﻿// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;

namespace E5R.Architecture.Infrastructure.Exceptions
{
    using Core;
    using static Core.ArchitectureLayerDefaults;

    public class InfrastructureLayerException : ArchitectureLayerException
    {
        public InfrastructureLayerException(string message) : base(InfrastructureLayer, message)
        {
        }

        public InfrastructureLayerException(string message, Exception innerException) : base(
            InfrastructureLayer, message, innerException)
        {
        }

        public InfrastructureLayerException(Exception exception) : base(InfrastructureLayer,
            exception)
        {
        }
    }
}
