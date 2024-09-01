﻿namespace Library.Api.Infrastructure.MongoDb;

internal sealed class MongoOptions
{
    public string ConnectionString { get; set; } = null!;
    public string Database { get; set; } = null!;
}