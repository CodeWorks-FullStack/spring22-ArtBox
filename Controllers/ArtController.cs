using System;
using System.Collections.Generic;
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
  public class ArtController : ControllerBase
  {
    private readonly ArtService _artServ;

    public ArtController(ArtService artServ)
    {
      _artServ = artServ;
    }

    // GET
    [HttpGet]
    public ActionResult<List<Art>> Get()
    {
      try
      {
        List<Art> art = _artServ.Get();
        return Ok(art);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // GETBYID
    [HttpGet("{id}")]
    public ActionResult<Art> Get(int id)
    {
      try
      {
        Art art = _artServ.Get(id);
        return Ok(art);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // POST
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Art>> Create([FromBody] Art artData)
    {
      try
      {
        // NOTE attach creatorID
        // IF YOU DID NOT PUT [AUTHORIZE] userInfo could be null
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        artData.CreatorId = userInfo.Id;
        Art art = _artServ.Create(artData);
        // EASY WAY TO POPULATE ON CREATES
        art.Creator = userInfo;
        return Ok(art);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // PUT
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Art>> Edit(int id, [FromBody] Art artData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        artData.CreatorId = userInfo.Id;
        artData.Id = id;
        Art art = _artServ.Edit(artData);
        return Ok(art);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // DELETE
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<String>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _artServ.Delete(id, userInfo.Id);
        return Ok("Delorted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }





  }
}