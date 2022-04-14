using Construction_Jobs.Models;
using Construction_Jobs.Repositories;

namespace Construction_Jobs.Services
{
  public class JobsService
  {
    private readonly JobsRepository _jRepo;
    private readonly ContractorsService _conService;
    private readonly CompaniesService _compService;
    public JobsService(JobsRepository jRepo, ContractorsService conService, CompaniesService compService)
    {
      _jRepo = jRepo;
      _conService = conService;
      _compService = compService;
    }

    internal Job Create(Job jobData)
    {
      return _jRepo.Create(jobData);
    }
  }


}