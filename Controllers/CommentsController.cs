using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using DaVinki.Models;
using DaVinki.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaVinki.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CommentsController : ControllerBase
  {
    private readonly CommentsService _cs;

    public CommentsController(CommentsService cs)
    {
      _cs = cs;
    }

    [HttpGet("{id}")]
    public ActionResult<Comment> Get(int id)
    {
      try
      {
        Comment comment = _cs.Get(id);
        return Ok(comment);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Comment>> Create([FromBody] Comment commentData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        commentData.CreatorId = userInfo.Id;
        Comment newComment = _cs.Create(commentData);
        newComment.Creator = userInfo;
        return Ok(newComment);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<String>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _cs.Delete(id, userInfo.Id);
        return Ok("Deleted Comment");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}