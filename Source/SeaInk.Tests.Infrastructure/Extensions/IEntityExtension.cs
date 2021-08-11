using System.Collections.Generic;
using SeaInk.Core.Entities;
using System.Linq;

namespace SeaInk.Tests.Infrastructure.Extensions
{
    public static class EntityExtension
    {
        public static IEnumerable<int> ToIds(this IEnumerable<IEntity> value)
            => value.Select(x => x.Id);
    }
}