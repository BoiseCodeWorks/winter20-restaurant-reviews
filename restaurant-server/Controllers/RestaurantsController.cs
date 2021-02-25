using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using restaurant_server.Models;
using restaurant_server.Services;

namespace restaurant_server.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class RestaurantsController : ControllerBase
  {
    private readonly RestaurantsService _rs;
    private readonly ReviewsService _rvs;

    public RestaurantsController(RestaurantsService rs, ReviewsService rvs)
    {
      _rs = rs;
      _rvs = rvs;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Restaurant>> Get()
    {
      try
      {
        return Ok(_rs.GetAll());
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }

    [HttpGet("{id}")]
    public ActionResult<IEnumerable<Restaurant>> Get(int id)
    {
      try
      {
        return Ok(_rs.Get(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Restaurant>> Post([FromBody] Restaurant newRestaurant)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newRestaurant.OwnerId = userInfo.Id;
        Restaurant created = _rs.Create(newRestaurant);
        created.Owner = userInfo;
        return Ok(created);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Restaurant>> Edit(int id, [FromBody] Restaurant editData)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        editData.Id = id;
        editData.Owner = userInfo;
        return Ok(_rs.Edit(editData, userInfo.Id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_rs.Delete(id, userInfo.Id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/reviews")]
    public ActionResult<IEnumerable<Restaurant>> GetReviewsByRestaurantId(int id)
    {
      try
      {
        return Ok(_rvs.GetByRestaurantId(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }

  }
}