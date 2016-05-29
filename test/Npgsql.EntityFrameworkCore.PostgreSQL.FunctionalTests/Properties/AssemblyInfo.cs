using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Npgsql.Logging;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

[assembly: AssemblyTitle("Npgsql.EntityFrameworkCore.PostgreSQL.FunctionalTests")]
[assembly: AssemblyDescription("Functional test suite for Npgsql PostgreSQL provider for Entity Framework Core")]

//[assembly: CollectionBehavior(MaxParallelThreads = 1)]
[assembly: CollectionBehavior(DisableTestParallelization = true)]

class FileLoggingProvider : INpgsqlLoggingProvider
{
    public static void Go()
    {
        NpgsqlLogManager.Provider = new FileLoggingProvider();
    }

    NpgsqlLogger INpgsqlLoggingProvider.CreateLogger(string name)
    {
        return new FileLogger(name);
    }
}

class FileLogger : NpgsqlLogger
{
    static StreamWriter _writer;

    internal FileLogger(string name)
    {
        _writer = new StreamWriter(new FileStream(@"c:\temp\crap.log", FileMode.CreateNew, FileAccess.ReadWrite));
    }

    public override bool IsEnabled(NpgsqlLogLevel level) => true;

    public override void Log(NpgsqlLogLevel level, int connectorId, string msg, Exception exception = null)
    {
        _writer.WriteLine($"{DateTime.Now} {connectorId} T{Thread.CurrentThread.ManagedThreadId} {msg}");
        _writer.Flush();
    }
}
