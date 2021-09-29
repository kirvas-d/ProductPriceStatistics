using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductPriceStatistics.Core.Commands
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
