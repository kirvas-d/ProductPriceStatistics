using ProductPriceStatistics.Domain.Entities;
using System.Collections.Generic;

namespace ProductPriceStatistics.Domain.Repositories
{
    interface ITagRepository
    {
        void AddTag(Tag tag);

        IEnumerable<Tag> GetAllTags();
    }
}
