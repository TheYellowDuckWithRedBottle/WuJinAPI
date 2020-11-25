using WuJinAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace WuJinAPI.Context
{
    public class BuildingsContext: DbContext
    {
        
        public BuildingsContext(DbContextOptions<BuildingsContext> options):base(options)
        {
           
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        //{
        //    base.OnConfiguring(dbContextOptionsBuilder);
        //}
        public virtual DbSet<Building> Buildings { get; set; }

    }
}
