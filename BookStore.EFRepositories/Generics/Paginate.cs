using BookStore.Repositories.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.EFRepositories.Generics
{
    public class Paginate<T> : IPaginate<T>
    {
        internal Paginate(IEnumerable<T> source, int index, int size, int from)
        {
            var enumerable = source as T[] ?? source.ToArray();

            if (from > index)
                throw new ArgumentException($"indexFrom: {from} > pageIndex: {index}, must indexFrom <= pageIndex");

            if (source is IQueryable<T> querable)
            {
                Index = index;
                Size = size;
                From = from;
                Count = querable.Count();
                Pages = (int)Math.Ceiling(Count / (double)Size);

                Items = querable.Skip((Index - From) * Size).Take(Size).ToList();
            }
            else
            {
                Index = index;
                Size = size;
                From = from;

                Count = enumerable.Count();
                Pages = (int)Math.Ceiling(Count / (double)Size);

                Items = enumerable.Skip((Index - From) * Size).Take(Size).ToList();
            }
        }

        internal Paginate()
        {
            Items = new T[0];
        }

        public int From { get; set; }
        public int Index { get; set; }
        public int Size { get; set; }
        public int Count { get; set; }
        public int Pages { get; set; }
        public IList<T> Items { get; set; }
        public bool HasPrevious => Index - From > 0;
        public bool HasNext => Index - From + 1 < Pages;
    }

    internal class Paginate<TSource, TResult> : IPaginate<TResult>
    {
        public Paginate(IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter,
            int index, int size, int from)
        {
            var enumerable = source as TSource[] ?? source.ToArray();

            if (from > index) throw new ArgumentException($"From: {from} > Index: {index}, must From <= Index");

            if (source is IQueryable<TSource> queryable)
            {
                Index = index;
                Size = size;
                From = from;
                Count = queryable.Count();
                Pages = (int)Math.Ceiling(Count / (double)Size);

                var items = queryable.Skip((Index - From) * Size).Take(Size).ToArray();

                Items = new List<TResult>(converter(items));
            }
            else
            {
                Index = index;
                Size = size;
                From = from;
                Count = enumerable.Count();
                Pages = (int)Math.Ceiling(Count / (double)Size);

                var items = enumerable.Skip((Index - From) * Size).Take(Size).ToArray();

                Items = new List<TResult>(converter(items));
            }
        }

        public Paginate(IPaginate<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
        {
            Index = source.Index;
            Size = source.Size;
            From = source.From;
            Count = source.Count;
            Pages = source.Pages;

            Items = new List<TResult>(converter(source.Items));
        }

        public int Index { get; }

        public int Size { get; }

        public int Count { get; }

        public int Pages { get; }

        public int From { get; }

        public IList<TResult> Items { get; }

        public bool HasPrevious => Index - From > 0;

        public bool HasNext => Index - From + 1 < Pages;
    }

    public static class Paginate
    {
        public static IPaginate<T> Empty<T>()
        {
            return new Paginate<T>();
        }

        public static IPaginate<TResult> From<TResult, TSource>(IPaginate<TSource> source,
            Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
        {
            return new Paginate<TSource, TResult>(source, converter);
        }
    }

    public static class PaginateExtensions
    {
        public static IPaginate<T> ToPaginate<T>(this IEnumerable<T> source, int index, int size, int from = 0)
        {
            return new Paginate<T>(source, index, size, from);
        }

        public static IPaginate<TResult> ToPaginate<TSource, TResult>(this IEnumerable<TSource> source,
            Func<IEnumerable<TSource>, IEnumerable<TResult>> converter, int index, int size, int from = 0)
        {
            return new Paginate<TSource, TResult>(source, converter, index, size, from);
        }
    }

    public static class IQueryablePaginateExtensions
    {
        public static async Task<IPaginate<T>> ToPaginateAsync<T>(this IQueryable<T> source, int index, int size,
            int from = 0, CancellationToken cancellationToken = default)
        {
            if (from > index) throw new ArgumentException($"From: {from} > Index: {index}, must From <= Index");

            var count = await source.CountAsync(cancellationToken).ConfigureAwait(false);
            var items = await source.Skip((index - from) * size)
                .Take(size).ToListAsync(cancellationToken).ConfigureAwait(false);

            var list = new Paginate<T>
            {
                Index = index,
                Size = size,
                From = from,
                Count = count,
                Items = items,
                Pages = (int)Math.Ceiling(count / (double)size)
            };

            return list;
        }
    }
}