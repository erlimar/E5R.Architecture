﻿// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace E5R.Architecture.Data.EntityFrameworkCore
{
    using Abstractions;
    using  Infrastructure;

    public class Storage<TDataModel> : IStorage<TDataModel>
        where TDataModel : class, IDataModel
    {
        private readonly FullStorage<TDataModel> _base = new FullStorage<TDataModel>();

        protected DbSet<TDataModel> Set => _base.Set;
        protected IQueryable<TDataModel> Query => _base.Query;
        protected WriterDelegate Write => _base.Write;

        public void Configure(UnderlyingSession session) => _base.Configure(session);

        public TDataModel Find(TDataModel data) => _base.Find(data);

        public DataLimiterResult<TDataModel> Get(DataLimiter<TDataModel> limiter) => _base.Get(limiter);

        public IEnumerable<TDataModel> Search(DataReducer<TDataModel> reducer) => _base.Search(reducer);

        public DataLimiterResult<TDataModel> LimitedSearch(DataReducer<TDataModel> reducer,
            DataLimiter<TDataModel> limiter) => _base.LimitedSearch(reducer, limiter);

        public TDataModel Create(TDataModel data) => _base.Create(data);

        public TDataModel Replace(TDataModel data) => _base.Replace(data);

        public void Remove(TDataModel data) => _base.Remove(data);

        public IEnumerable<TDataModel> BulkCreate(IEnumerable<TDataModel> data) => _base.BulkCreate(data);

        public IEnumerable<TDataModel> BulkReplace(IEnumerable<TDataModel> data) => _base.BulkReplace(data);

        public void BulkRemove(IEnumerable<TDataModel> data) => _base.BulkRemove(data);

        public void BulkRemoveFromSearch(DataReducer<TDataModel> reducer) =>
            _base.BulkRemoveFromSearch(reducer);
    }
}
