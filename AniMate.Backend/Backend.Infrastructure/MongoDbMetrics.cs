using Prometheus;

namespace Backend.Infrastructure;

public class MongoDbMetrics
{
    public static readonly Histogram Query = Metrics
        .CreateHistogram("db_query_duration_seconds", "The duration of mongodb queries",
            new HistogramConfiguration
            {
                SuppressInitialValue = true,
                Buckets = Histogram.ExponentialBuckets(0.02, 5, 5),
            });

    public static readonly Histogram Command = Metrics
        .CreateHistogram("db_command_duration_seconds", "The duration of mongodb commands",
            new HistogramConfiguration
            {
                SuppressInitialValue = true,
                Buckets = Histogram.ExponentialBuckets(0.02, 5, 5),
            });
}