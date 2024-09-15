using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ModularMonolith.Shared.CQRS.Queries
{
    public static class QueryableExtensions
    {
        public static async Task<List<TSource>> ToListAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken = default)
        {
            List<TSource> list = [];
            await foreach (TSource item in source.AsAsyncEnumerable().WithCancellation(cancellationToken))
            {
                list.Add(item);
            }

            return list;
        }

        public static IAsyncEnumerable<TSource> AsAsyncEnumerable<TSource>(this IQueryable<TSource> source)
        {
            if (source is IAsyncEnumerable<TSource> asyncEnumerable)
            {
                return asyncEnumerable;
            }

            throw new InvalidOperationException();
        }

        public static async Task<TSource[]> ToArrayAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken = default)
            => [.. (await source.ToListAsync(cancellationToken).ConfigureAwait(false))];
    }
}
