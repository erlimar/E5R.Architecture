﻿using System;
using E5R.Architecture.Core.Abstractions;
using E5R.Architecture.Data;
using E5R.Architecture.Data.Abstractions;

namespace UsingData
{
    using static MemorySession;

    public class MemoryUnitOfWork : IUnitOfWork
    {
        private readonly IFileSystem _fs;
        private UnderlyingSession _session;
        private bool _disposed = false;

        public MemoryUnitOfWork(IFileSystem fs)
        {
            _fs = fs;
        }

        public void SaveWork()
        {
            SaveSession(Session, _fs);
            _session = null;
        }

        public UnderlyingSession Session
            => _session ?? (_session = CreateSession(_fs));

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _session = null;
            }

            _disposed = true;
        }
    }
}