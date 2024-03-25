using Microsoft.EntityFrameworkCore;
using Squares.Api.Models;

namespace Squares.Api.Data
{
    public class SquaresDbContext : DbContext
    {
        public SquaresDbContext(DbContextOptions<SquaresDbContext> options) : base(options)
        {
        }
        public DbSet<PointsCollection> PointsCollection { get; set; }
        public DbSet<Point> Points { get; set; }
    }
}
