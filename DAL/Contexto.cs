using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Parcial2.Entidades;

namespace Parcial2.DAL
{
    public class Contexto :DbContext
    {
        public DbSet<LLamada> Llamadas { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.EnableSensitiveDataLogging().UseSqlite(@"Data Source =DATA/Llamadas.db");
        }
    }
}
