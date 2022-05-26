using System;
using System.Threading.Tasks;
using DaVinki.Models;
using DaVinki.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DaVinki.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly PurchasesService _ps;

    public AccountController(AccountService accountService, PurchasesService ps)
    {
      _accountService = accountService;
      _ps = ps;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpGet("bought")]
    [Authorize]
    public async Task<ActionResult<List<ArtPurchaseViewModel>>> GetBought()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<ArtPurchaseViewModel> art = _ps.GetAccountArtPurchase(userInfo.Id);
        return Ok(art);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }


}