using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using A1.Models;
using A1.Dtos;


namespace A1.Data
{
    public interface IA1Repo
    {
        // Endpoint 1: Get the version of the Web API
        string GetVersion();

        // Endpoint 3: List the signs provided by the group
        IEnumerable<Sign> AllSigns();
        // Endpoint 4: List  signs/sign provided by the group
        IEnumerable<Sign> Signs(string term);

        // Endpoint 6: Get a comment with a given ID
        Comment GetComment(int id);
    
        // Endpoint 7: Write comment
        Comment WriteComment(Comment comment);

        // Endpoint 8: Display comments
        IEnumerable<Comment> Comments(int num);
    }
}
