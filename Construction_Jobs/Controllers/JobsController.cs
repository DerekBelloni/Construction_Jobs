using Construction_Jobs.Models;
using Construction_Jobs.Services;
using Microsoft.AspNetCore.Mvc;

namespace Construction_Jobs.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class JobsController : ControllerBase
  {
    private readonly JobsService _jobsService;

    public JobsController(JobsService jobsService)
    {
      _jobsService = jobsService;
    }
    [HttpPost]
    public ActionResult<Job> Create([FromBody] Job jobData)
    {
      try
      {
        Job job = _jobsService.Create(jobData);
        return Ok(job);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }

}


