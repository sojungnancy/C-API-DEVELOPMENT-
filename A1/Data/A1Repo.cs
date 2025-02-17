using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Http;
using A1.Models;
using A1.Dtos;

namespace A1.Data
{
    public class A1Repo : IA1Repo
    {
      private readonly A1DbContext _dbContext;

      public A1Repo(A1DbContext dbContext)
        {
            // reference to the DB is stored in "_dbContext" - readonly
            _dbContext = dbContext;
        } 

      // Endpoint 1: Get the version of the Web API
      public string GetVersion(){
        string upi = "skim509";
        string version = string.Format("1.0.0 (Ngongotahā) by {0}", upi);
        return version;
      }

      // Endpoint 3: List the signs provided by the group
      public IEnumerable<Sign> AllSigns(){
        IEnumerable<Sign> signs = _dbContext.Signs.ToList();
        return signs;
      }

     // Endpoint 4: List  signs/sign provided by the group
      public IEnumerable<Sign> Signs(string term){
        return _dbContext.Signs.Where(e => e.Description.ToLower().Contains(term.ToLower())).ToList();
    
      }
    // Endpoint 6: Get a comment with a given ID
    public Comment? GetComment(int id){
      Comment? comment = _dbContext.Comments.FirstOrDefault(e => e.Id == id);
      return comment;
    }
    
    // Endpoint 7: Write comment
    public Comment WriteComment(Comment comment){
      EntityEntry<Comment> e = _dbContext.Comments.Add(comment);
      Comment c = e.Entity;
      _dbContext.SaveChanges();
      return c;
    }
    // Endpoint 8: Display comments
    public IEnumerable<Comment> Comments(int num) {
            IEnumerable<Comment> latestComments = _dbContext.Comments.OrderByDescending(e => e.Id).ToList<Comment>();
            // Total number of comments in the "Comments" table
            int commentsNum = latestComments.Count();
            // If total number of comments is less than or equal to the given number, then display all
            if (commentsNum <= num)
            {
                return latestComments;
            }
            else return latestComments.Take(num);
    }
    
    }
}
