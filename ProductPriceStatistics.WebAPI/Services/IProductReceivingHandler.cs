using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStoreMicroservice.Services
{
    public interface IProductReceivingHandler<T>
    {
        void Handle(T product);
    }
}
