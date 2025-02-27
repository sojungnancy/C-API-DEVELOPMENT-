﻿using A2TEMPLATE.Models;
using Microsoft.EntityFrameworkCore;

namespace A2TEMPLATE.Data
{
    public class A2DbContext : DbContext{

        public A2DbContext(DbContextOptions<A2DbContext> options) : base(options) {}
        public DbSet<Sign> Signs { get; set;}
        public DbSet<User> Users { get; set;}
        public DbSet<Event> Events { get; set;}
        public DbSet<Organizer> Organizers { get; set;}
    }
}