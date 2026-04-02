using DataStax.AstraDB.DataApi.Tables;
using Newtonsoft.Json;
using System;
using System.Globalization;

namespace kv_be_csharp_dataapi_table.Models;

public class Comment
{
    [ColumnPrimaryKey(1)]
    [ColumnName("videoid")]
    public Guid videoid { get; set; } = Guid.Empty;

    [ColumnPrimaryKey(2)]
    [ColumnName("commentid")]
    public Guid commentid { get; set; } = GenerateTimeBasedGuid(DateTimeOffset.UtcNow);
    public string comment { get; set; } = string.Empty;
    public Guid userid { get; set; } = Guid.Empty;
    
    [ColumnName("sentiment_score")]
    [JsonProperty("sentiment_score")]
    public float sentimentScore { get; set; } = 0.0F;

    private static readonly DateTimeOffset GregorianCalendarTime = new DateTimeOffset(1582, 10, 15, 0, 0, 0, TimeSpan.Zero);
    private static readonly Random RandomGenerator = new Random();

    // https://github.com/datastax/csharp-driver/blob/master/src/Cassandra/TimeUuid.cs
    public static Guid GenerateTimeBasedGuid(DateTimeOffset dateTime)  
    {
        byte[] clockSequenceBytes = BitConverter.GetBytes(Convert.ToInt16(Environment.TickCount % Int16.MaxValue));
        byte[] nodeId = new byte[6];
        byte[] clockId = new byte[2];
        RandomGenerator.NextBytes(nodeId);
        RandomGenerator.NextBytes(clockId);

        var timeBytes = BitConverter.GetBytes((dateTime - GregorianCalendarTime).Ticks);

        if (!BitConverter.IsLittleEndian)
        {
            Array.Reverse(timeBytes);
        }

        byte[] timeuuid = new byte[16];

        //Positions 0-7 Timestamp
        Buffer.BlockCopy(timeBytes, 0, timeuuid, 0, 8);
        //Position 8-9 Clock
        Buffer.BlockCopy(clockId, 0, timeuuid, 8, 2);
        //Positions 10-15 Node
        Buffer.BlockCopy(nodeId, 0, timeuuid, 10, 6);

        //Version Byte: Time based
        //0001xxxx
        //turn off first 4 bits
        timeuuid[7] &= 0x0f; //00001111
        //turn on fifth bit
        timeuuid[7] |= 0x10; //00010000

        //Variant Byte: 1.0.x
        //10xxxxxx
        //turn off first 2 bits
        timeuuid[8] &= 0x3f; //00111111
        //turn on first bit
        timeuuid[8] |= 0x80; //10000000

        return new Guid(timeuuid);
    }

    // pull date from TimeUUID
    public DateTimeOffset GetDate(Guid timeuuid)
    {
        var bytes = timeuuid.ToByteArray();
        //Remove version bit
        bytes[7] &= 0x0f; //00001111
        //Remove variant
        bytes[8] &= 0x3f; //00111111
        if (!BitConverter.IsLittleEndian)
        {
            Array.Reverse(bytes);
        }
        var timestamp = BitConverter.ToInt64(bytes, 0);
        var ticks = timestamp + GregorianCalendarTime.Ticks;

        return new DateTimeOffset(ticks, TimeSpan.Zero);
    }
}