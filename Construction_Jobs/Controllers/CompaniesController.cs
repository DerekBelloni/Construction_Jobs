using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Construction_Jobs.Models;
using Construction_Jobs.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Construction_Jobs.Controllers
{
  [ApiController]
  [Route("api/[controller]")]

  public class CompaniesController : ControllerBase
  {
    private readonly CompaniesService _companiesService;

    public CompaniesController(CompaniesService companiesService)
    {
      _companiesService = companiesService;
    }

    [HttpGet]
    public ActionResult<List<Company>> GetAll()
    {
      try
      {
        List<Company> company = _companiesService.GetAll();
        return Ok(company);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Company> GetById(int id)
    {
      try
      {
        Company company = _companiesService.GetById(id);
        return Ok(company);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Company>> Create([FromBody] Company companyData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        companyData.CreatorId = userInfo.Id;
        Company company = _companiesService.Create(companyData);
        return Ok(company);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Company>> Update(int id, [FromBody] Company updatedCompany)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        updatedCompany.CreatorId = userInfo.Id;
        updatedCompany.Id = id;
        Company company = _companiesService.Update(updatedCompany);
        return Ok(company);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Remove(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        string message = _companiesService.Remove(id, userInfo);
        return Ok(message);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}