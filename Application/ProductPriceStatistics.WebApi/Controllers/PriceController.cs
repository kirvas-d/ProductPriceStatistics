using Microsoft.AspNetCore.Mvc;
using ProductPriceStatistics.Core.DTOs;
using ProductPriceStatistics.Core.Queries;
using ProductPriceStatistics.Core.QueryHandlers;
using System;
using System.Collections.Generic;

namespace ProductPriceStatistics.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly IQueryHandler<GetPricesOfProductQuery, IEnumerable<PriceDto>> _getPricesOfProductQueryHandler;

        public PriceController(IQueryHandler<GetPricesOfProductQuery, IEnumerable<PriceDto>> getPricesOfProductQueryHandler) 
        {
            _getPricesOfProductQueryHandler = getPricesOfProductQueryHandler;
        }

        // GET: api/<PriceController>
        [HttpGet]
        public IEnumerable<PriceDto> Get([FromQuery] Guid? productId, [FromQuery] DateTime? startDateTime, [FromQuery] DateTime? finishDateTime)
        {
            if (productId.HasValue)
            {
                return _getPricesOfProductQueryHandler.Ask(new GetPricesOfProductQuery(productId.Value, startDateTime, finishDateTime));
            }
            else 
            {
                return null;
            }
        }
    }
}
