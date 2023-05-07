//using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Core.DataContext
{
    public class AppContext : DbContext
    {
        public AppContext() { }
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }
    }
}
