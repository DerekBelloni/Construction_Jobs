using Microsoft.AspNetCore.Mvc;
using Construction_Jobs.Services;
using System.Collections.Generic;
using Construction_Jobs.Models;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;

namespace Construction_Jobs.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ContractorsController : ControllerBase
  {
    private readonly ContractorsService _contractorsService;

    public ContractorsController(ContractorsService contractorsService)
    {
      _contractorsService = contractorsService;
    }

    [HttpGet]
    public ActionResult<List<Contractor>> GetAll()
    {
      try
      {
        List<Contractor> contractors = _contractorsService.GetAll();
        return Ok(contractors);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Contractor> GetById(int id)
    {
      try
      {
        Contractor contractor = _contractorsService.GetById(id);
        return Ok(contractor);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/contractorCompanies")]
    public ActionResult<List<ContractorCompanyViewModel>> GetCompanies(int id)
    {
      try
      {
        List<ContractorCompanyViewModel> contractorCompanies = _contractorsService.GetCompanies(id);
        return Ok(contractorCompanies);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Contractor>> Create([FromBody] Contractor contractorData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        contractorData.CreatorId = userInfo.Id;
        Contractor contractor = _contractorsService.Create(contractorData);
        return Ok(contractor);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Contractor>> Update(int id, [FromBody] Contractor updatedContractor)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        updatedContractor.CreatorId = userInfo.Id;
        updatedContractor.Id = id;
        Contractor contractor = _contractorsService.Update(updatedContractor);
        return Ok(updatedContractor);
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
        string message = _contractorsService.Remove(id, userInfo);
        return Ok(message);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}