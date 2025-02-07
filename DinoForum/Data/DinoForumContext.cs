using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DinoForum.Models;

namespace DinoForum.Data
{
    public class DinoForumContext : DbContext
    {
        public DinoForumContext (DbContextOptions<DinoForumContext> options)
            : base(options)
        {
        }

        public DbSet<DinoForum.Models.Discussion> Discussion { get; set; } = default!;
        public DbSet<DinoForum.Models.Comment> Comment { get; set; } = default!;
    }
}
