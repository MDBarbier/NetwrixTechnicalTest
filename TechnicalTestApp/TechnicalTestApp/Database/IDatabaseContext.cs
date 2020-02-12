using Microsoft.EntityFrameworkCore;
using System;

namespace TechnicalTestApp.Database
{
    public interface IDatabaseContext : IDisposable
    {
        DbContext Instance { get; }
    }
}
