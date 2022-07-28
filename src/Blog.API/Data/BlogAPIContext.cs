using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Blog.API.Entities;

    public class BlogAPIContext : DbContext
    {
        public BlogAPIContext (DbContextOptions<BlogAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Subject> Subject { get; set; } = default!;
        public DbSet<Post> Post { get; set; } = default!;
}
