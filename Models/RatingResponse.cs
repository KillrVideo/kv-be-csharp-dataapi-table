namespace kv_be_csharp_dataapi_table.Models;

public class RatingResponse
{
    public List<RatingConversion> data { get; set; } = new();
    public string averageRating { get; set; } = string.Empty;

    public RatingResponse()
    {
        this.averageRating = "0.0";
    }

    public RatingResponse(RatingDB rating)
    {
        List<RatingConversion> dataResponse = new();

        RatingConversion localRating = new RatingConversion(rating);
        dataResponse.Add(localRating);


        float averageRatingFlt = localRating.averageRating;
        this.averageRating = averageRatingFlt.ToString("0.0");
        this.data = dataResponse;
    }
}

public class RatingConversion
{
    public Guid videoid { get; set; } = Guid.Empty;
    public float averageRating { get; set; } = 0f;
    public int ratingCount { get; set; } = 0;

    public RatingConversion(RatingDB rating)
    {
        this.videoid = rating.videoid;
        this.averageRating = rating.ratingTotal / rating.ratingCounter;
        this.ratingCount = rating.ratingCounter;
    }
}