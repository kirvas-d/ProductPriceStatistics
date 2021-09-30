namespace ProductPriceStatistics.Core.CommandHandlers
{
    public interface ICommandHandler<in TCommand>
    {
        public void Handle(TCommand command);
    }

    public interface ICommandHandler<in TCommand, out TResult>
    {
        public TResult Handle(TCommand command);
    }
}
