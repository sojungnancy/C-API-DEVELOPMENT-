using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using A1.Models;
using A1.Dtos;

namespace A1.Data
{
    public class A1DbContext: DbContext
    {
        public A1DbContext(DbContextOptions<A1DbContext> options) : base(options) { }
        // "Comments" represents the table Products
        public DbSet<Comment> Comments {get;set;}
        // "Sign" represents the table Products
        public DbSet<Sign> Signs {get;set;}
    }
}