using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;

namespace WPFPWZTGBOT
{
    internal class DbConnect : DbContext
    {
        public DbSet<Object_Orders> History_orders { get; set; }
        public DbSet<Object_tg_chat> Tg_bot { get; set; }
        public DbConnect()
        {
            Database.EnsureCreated();
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Object_tg_chat>().HasNoKey(); // Вызываем HasNoKey для указания, что сущность не имеет первичного ключа
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user=root;password=Aa_111111;database=db",
                new MySqlServerVersion(new Version(8, 0, 36)));
        }
    } 
}
