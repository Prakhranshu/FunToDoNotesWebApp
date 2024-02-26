using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{
    public class FunNoteContext:DbContext
    {
        public FunNoteContext(DbContextOptions options) : base(options) 
        { }

        DbSet<DemoEntity> DemoTable { get; set; }
        public DbSet<UserEntity> UserTable { get; set; }   
    }
}
