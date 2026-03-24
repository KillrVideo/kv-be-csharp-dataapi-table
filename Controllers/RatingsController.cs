using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using kv_be_csharp_dataapi_table.Repositories;
using kv_be_csharp_dataapi_table.Models;
using System.Security.Claims;

namespace kv_be_csharp_dataapi_table.Controllers;

[ApiController]
[Route("/api/v1/videos")]
[Produces("application/json")]
public class RatingsController : Controller
{
    private readonly IRatingDAL _ratingDAL;

    public RatingsController(IRatingDAL ratingDAL)
    {
        _ratingDAL = ratingDAL;
    }

    [HttpPost("{videoid}/ratings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<ActionResult> SubmitRating(Guid videoid, [FromBody] RatingRequest ratingRequest)
    {
        var userId = getUserIdFromAuth(HttpContext.User);

        // check for existing rating
        var existingRatingByUser = await _ratingDAL.FindByVideoIdAndUserId(videoid, userId);
        var existingRating = await _ratingDAL.FindByVideoId(videoid);

        if (existingRatingByUser is not null)
        {
            int existingRatingScore = existingRating.ratingTotal;
            // remove old score, add new
            int updatedScore = existingRatingScore - existingRatingByUser.rating + ratingRequest.rating;

            // update existing rating
            existingRatingByUser.rating = ratingRequest.rating;
            existingRatingByUser.ratingDate = DateTimeOffset.Now;
            existingRating.ratingTotal = updatedScore;

            _ratingDAL.UpdateRatingByUser(existingRatingByUser);
            _ratingDAL.UpdateRating(existingRating);
        }
        else
        {
            Rating rating = new Rating();
            rating.videoid = videoid;
            rating.userid = userId;
            rating.rating = ratingRequest.rating;

            RatingDB ratingDB = new RatingDB();
            ratingDB.videoid = videoid;
            ratingDB.ratingCounter = 1;
            ratingDB.ratingTotal = ratingRequest.rating;

            await _ratingDAL.SaveRatingByUser(rating);
            await _ratingDAL.SaveRating(ratingDB);
        }

        return Ok();
    }

    [HttpGet("{videoid}/ratings")]
    [ProducesResponseType(typeof(RatingResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RatingResponse>> GetVideoRating(Guid videoid)
    {
        var ratings = await _ratingDAL.FindByVideoId(videoid);
        RatingResponse response = new RatingResponse();

        if (ratings is not null)
        {
            response = new RatingResponse(ratings);
        }

        return Ok(response);
    }

    [HttpGet("id/{videoid}/rating")]
    [ProducesResponseType(typeof(RatingSummaryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RatingSummaryResponse>> GetAggregateVideoRating(Guid videoid)
    {
        RatingDB? rating = await _ratingDAL.FindByVideoId(videoid);
        RatingSummary summary = new();
        summary.videoid = videoid;

        if (rating is null)
        {
            summary.averageRating = "0.0";
        }
        else
        {
            int ratingSum = rating.ratingCounter;
            int ratingCount = rating.ratingTotal;
            summary.averageRating = (ratingSum / ratingCount).ToString("0.0");
            summary.ratingCount = ratingCount;
        }

        return Ok(new RatingSummaryResponse(summary));
    }

    [HttpGet("{videoid}/ratings/user/{userid}")]
    [ProducesResponseType(typeof(RatingSummaryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RatingSummaryResponse>> GetUserRating(Guid videoid, Guid userid)
    {
        var userRating = await _ratingDAL.FindByVideoIdAndUserId(videoid, userid);

        RatingSummary summary = new();
        summary.videoid = videoid;

        if (userRating is not null)
        {
            summary.ratingCount = 1;
            summary.averageRating = userRating.rating.ToString() + ".0";
            summary.currentUserRating = userRating.rating;
        }
        else
        {
            summary.averageRating = "0.0";
        }

        return Ok(new RatingSummaryResponse(summary));
    }

    private Guid getUserIdFromAuth(ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim is not null)
        {
            return Guid.Parse(userIdClaim.Value);
        }

        return Guid.Empty;
    }
}