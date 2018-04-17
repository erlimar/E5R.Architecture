﻿using System;
using System.Collections.Generic;
using System.Linq;
using E5R.Architecture.Core;
using E5R.Architecture.Data.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    public class StorageReader<TModel> : IStorageReader<StorageReader<TModel>, TModel>
        where TModel : DataModel<TModel>
    {
        protected IQueryable<TModel> Reader { get; private set; }

        public StorageReader<TModel> Configure(UnderlyingSession session)
        {
            Checker.NotNullArgument(session, nameof(session));

            var context = session.Get<DbContext>();

            if (context == null)
            {
                // TODO: Implementar internacionalização
                throw new NullReferenceException(
                    $"The context is null. The session has not been configured.");
            }

            Reader = context.Set<TModel>().AsNoTracking();

            return this;
        }

        public TModel Find(TModel data)
        {
            Checker.NotNullArgument(data, nameof(data));

            return Reader.SingleOrDefault(data.GetIdenifierCriteria());
        }


        public DataLimiterResult<TModel> Get(DataLimiter<TModel> limiter)
        {
            Checker.NotNullArgument(limiter, nameof(limiter));

            return QueryLimitResult(limiter, Reader);
        }

        public IEnumerable<TModel> Search(DataReducer<TModel> reducer)
        {
            Checker.NotNullArgument(reducer, nameof(reducer));

            return QuerySearch(reducer);
        }

        public DataLimiterResult<TModel> LimitedSearch(DataReducer<TModel> reducer,
            DataLimiter<TModel> limiter)
        {
            Checker.NotNullArgument(reducer, nameof(reducer));
            Checker.NotNullArgument(limiter, nameof(limiter));

            var result = QuerySearch(reducer);

            return QueryLimitResult(limiter, result);
        }

        #region Auxiliary methods

        private IQueryable<TModel> QuerySearch(DataReducer<TModel> reducer)
        {
            Checker.NotNullArgument(reducer, nameof(reducer));

            var reducerList = reducer.GetReducer();

            Checker.NotNullObject(reducerList, $"reducer.{nameof(reducer.GetReducer)}()");

            return reducerList.Aggregate(Reader, (q, w) => q.Where(w));
        }

        private DataLimiterResult<TModel> QueryLimitResult(DataLimiter<TModel> limiter,
            IQueryable<TModel> origin)
        {
            Checker.NotNullArgument(limiter, nameof(limiter));

            // Ensures offset range
            if (0 > limiter.OffsetBegin)
            {
                throw new ArgumentOutOfRangeException($"limiter.{limiter.OffsetBegin}");
            }

            if (0 > limiter.OffsetEnd)
            {
                throw new ArgumentOutOfRangeException($"limiter.{limiter.OffsetEnd}");
            }

            if (0 > limiter.OffsetEnd - limiter.OffsetBegin)
            {
                throw new ArgumentOutOfRangeException(
                    $"limiter.{limiter.OffsetEnd} - limiter.{limiter.OffsetBegin}");
            }

            var sorter = limiter.GetSorter();

            Checker.NotNullObject(sorter, $"limiter.{nameof(limiter.GetSorter)}()");

            var ordered = !limiter.Descending
                ? origin.OrderBy(sorter)
                : origin.OrderByDescending(sorter);

            var count = ordered.Count();
            var result = ordered
                .Skip(limiter.OffsetBegin)
                .Take(limiter.OffsetEnd - limiter.OffsetBegin);

            return new DataLimiterResult<TModel>(result, count);
        }

        #endregion
    }
}
