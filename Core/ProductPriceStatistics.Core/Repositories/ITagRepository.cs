using ProductPriceStatistics.Core.Models;
using System.Collections.Generic;

namespace ProductPriceStatistics.Core.Repositories
{
    interface ITagRepository
    {
        void AddTag(Tag tag);

        IEnumerable<Tag> GetAllTags();
    }
}
