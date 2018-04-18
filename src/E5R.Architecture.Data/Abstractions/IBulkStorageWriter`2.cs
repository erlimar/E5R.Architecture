﻿using System.Collections.Generic;

namespace E5R.Architecture.Data.Abstractions
{
    public interface IBulkStorageWriter<out TImpl, TModel> : IStorageSignature,
        ITradableObject<TImpl>
        where TImpl : class
        where TModel : DataModel<TModel>
    {
        IEnumerable<TModel> BulkCreate(IEnumerable<TModel> data);
        IEnumerable<TModel> BulkReplace(IEnumerable<TModel> data);
        void BulkRemove(IEnumerable<TModel> data);
        void BulkRemoveFromSearch(DataReducer<TModel> reducer);
    }
}