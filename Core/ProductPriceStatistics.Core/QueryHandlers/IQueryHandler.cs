namespace ProductPriceStatistics.Core.QueryHandlers
{
    public interface IQueryHandler<out TResult>
    {
        TResult Ask();
    }

    public interface IQueryHandler<in TQuery, out TResult>
    {
        TResult Ask(TQuery query);
    }
}
