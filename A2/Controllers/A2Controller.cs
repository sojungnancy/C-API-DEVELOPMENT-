using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using A2TEMPLATE.Data;
using A2TEMPLATE.Models;
using A2TEMPLATE.Dtos;
using System.Reflection.Metadata.Ecma335;

[Route("webapi")]
[ApiController]
public class A2Controller : Controller
{
    private readonly IA2Repo _repository;

    public A2Controller(IA2Repo a2Repo){
        _repository = a2Repo;
    }

    [HttpPost("Register")]
    public ActionResult<string> Register(User user){
        var added = _repository.Register(user);
        if (added) return Ok("User successfully registered.");
        return Ok($"UserName {user.UserName} is not available.");
    }

    [Authorize(AuthenticationSchemes = "A2Authentication", Policy = "UserPolicy")]
    [HttpGet("PurchaseSign/{id}")]
    public ActionResult<PurchaseOutput> PurchaseSign(string id){
        Claim claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
        var found = _repository.PurchaseSign(id);
        if (found) 
        {
            return Ok(new PurchaseOutput{
                UserName = claim.Value.ToString(),
                SignID = id
            });
        }
        else return BadRequest($"Sign {id} not found");
    }

    [Authorize(AuthenticationSchemes = "A2Authentication", Policy = "OrganizerPolicy")]
    [HttpPost("AddEvent")]
    public ActionResult<string> AddEvent(EventInput eventInput){
        string st = _repository.AddEvent(eventInput);
        if (st == "Success") return Ok(st);
        else return BadRequest(st);
    }

    [Authorize(AuthenticationSchemes = "A2Authentication", Policy = "AuthOnly")]
    [HttpGet("EventCount")]
    public ActionResult<int> EventCount(){
        return Ok(_repository.EventCount());
    }

    [Authorize(AuthenticationSchemes = "A2Authentication", Policy = "AuthOnly")]
    [HttpGet("Event/{id}")]
    public ActionResult Event(int id){
        Event e = _repository.GetEvent(id);
        if (e == null){
            return BadRequest($"Event {id} does not exist.");
        }
        Response.Headers.Add("Content-Type", "text/calendar");
        return Ok(e);
    }




}