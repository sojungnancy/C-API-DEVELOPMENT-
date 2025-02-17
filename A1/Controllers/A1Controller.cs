using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using A1.Data;
using A1.Models;
using A1.Dtos;

namespace A1.Controllers{
  [Route("webapi")]
  [ApiController]

     public class A1Controller: Controller
    {
        private readonly IA1Repo _repository;

        public A1Controller(IA1Repo repository)
        {
            _repository = repository;
        }

        // Endpoint 1: Get the version of the Web API
        // GET /webapi/GetVersion
        [HttpGet("GetVersion")]
        public ActionResult<string> GetVersion(){
          string version = _repository.GetVersion();
          return Ok(version);
        }

        // Endpoint 2: Get the logo of the company
        // GET /webapi/Logo
        [HttpGet("Logo")]
        public IActionResult GetLogo()
        {
            string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "Logos", "Logo.png");

            if (!System.IO.File.Exists(logoPath))
            {
                return NotFound("Logo image not found.");
            }

            return PhysicalFile(logoPath, "image/png");
        }

      // Endpoint 3: List the signs provided by the group
      // GET /webapi/AllSigns
      [HttpGet("AllSigns")]
      public ActionResult<IEnumerable<Sign>> AllSigns()
      {
        IEnumerable<Sign> signs = _repository.AllSigns();
        return Ok(signs);
      }

      // Endpoint 4: List  signs/sign provided by the group
      // GET /webapi/Signs/{term}
        [HttpGet("Signs/{term}")]
        public ActionResult<IEnumerable<Sign>> Signs(string term){
          IEnumerable<Sign> signs = _repository.Signs(term);
          return Ok(signs);
        }
        // Endpoint 5: Get the images of an item
        // The photos can be of type JPEG, PNG, GIF and SVG
        // GET /webapi/SignImage/{id}
        [HttpGet("SignImage/{id}")]
        public ActionResult SignImage(string id) {
          string path = Directory.GetCurrentDirectory();
          string imgDir = Path.Combine(path,"SignsImages");
          string fileJPEG = Path.Combine(imgDir, id + ".jpeg");
          string filePNG = Path.Combine(imgDir, id + ".png");
          string fileGIF = Path.Combine(imgDir, id + ".gif");
          string fileSVG = Path.Combine(imgDir, id + ".svg");
          string respHeader = "";
          string fileName = "";
          if (System.IO.File.Exists(filePNG)){
            respHeader ="image/png";
            fileName = filePNG;
          }
          else if (System.IO.File.Exists(fileJPEG)){
            respHeader ="image/jpeg";
            fileName = fileJPEG;
          }

          else if (System.IO.File.Exists(fileGIF)){
            respHeader ="image/gif";
            fileName = fileGIF;
          }
          else if (System.IO.File.Exists(fileSVG)){
            respHeader ="image/svg";
            fileName = fileSVG;
          }
          else{
            respHeader ="image/png";
            fileName = Path.Combine(imgDir, "default.png");
          }
          return PhysicalFile(fileName,respHeader);
        }


        // Endpoint 6: Get a comment with a given ID
        // GET /webapi/GetComment/{id}
        [HttpGet("GetComment/{id}")]
        public ActionResult<Comment> GetComment(int id){
          Comment comment = _repository.GetComment(id);
          if (comment == null) {
            string message = string.Format("Comment {0} does not exist.",id);
            return BadRequest(message);
          }
          else{
            return Ok(comment);
          }
        }

        // Endpoint 7: Write comment
        // POST /webapi/WriteComment
        [HttpPost("WriteComment")]
        public ActionResult<Comment> WriteComment(CommentInput comment){
          Comment c = new Comment {
            UserComment = comment.UserComment,
            Name = comment.Name,
            // server code obtains the user's IP address
            // from the Request.HttpContext.Connection.RemoteIpAddress property
            IP = Request.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "",
            // format of the time: "yyyyMMddTHHmmssZ"
            // "Z" indicates Coordinated Universal Time (UTC)
            Time = DateTime.UtcNow.ToString("yyyyMMddTHHmmssZ")
          };
            Comment addedComment = _repository.WriteComment(c);

            return CreatedAtAction(nameof(GetComment), new { id = addedComment.Id }, addedComment);
          }
        // Endpoint 8: Display comments
        // GET /webapi/Comments/{num}
        [HttpGet("Comments/{num}")]
        public ActionResult<IEnumerable<Comment>> Comments(int num = 5){
          // default value: 5
          IEnumerable<Comment> comments = _repository.Comments(num);
          return Ok(comments); 
        }
          

        
    }
}