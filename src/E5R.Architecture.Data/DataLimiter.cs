﻿namespace E5R.Architecture.Data
{
    /// <summary>
    /// Is a #skip-take
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    /// <typeparam name="TIdentifier">Identifier type of model</typeparam>
    public class DataLimiter<TModel, TIdentifier> : DataSorter<TModel, TIdentifier>
        where TModel : DataModel<TIdentifier>
        where TIdentifier : struct
    {
    }
}