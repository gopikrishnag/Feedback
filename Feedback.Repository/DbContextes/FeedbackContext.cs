using Feedback.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Repository.DbContextes
{

    public class FeedbackContext : DbContext
    {
        public FeedbackContext() : base()
        { }
        public FeedbackContext(DbContextOptions<FeedbackContext> options) : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Feedback;Integrated Security=True;MultipleActiveResultSets=True");
        //}
        public virtual DbSet<FeedbackDto> FeedbackDtos { get; set; }
    }
}
