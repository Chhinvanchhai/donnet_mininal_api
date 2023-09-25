using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace c_minial_api.Models
{

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string? name { get; set; }
    }


    class ShopDb : DbContext
    {
        public DbSet<Blog> Blogs { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;

        public ShopDb(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost:5432;Database=shop;Username=postgres;Password=123456");
 
    }
}