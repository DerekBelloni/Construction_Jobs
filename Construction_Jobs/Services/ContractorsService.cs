using System.Collections.Generic;
using Construction_Jobs.Models;
using Construction_Jobs.Repositories;

namespace Construction_Jobs.Services
{
  public class ContractorsService
  {
    private readonly ContractorsRepository _contractorsRepo;

    public ContractorsService(ContractorsRepository contractorsRepo)
    {
      _contractorsRepo = contractorsRepo;
    }

    internal List<Contractor> GetAll()
    {
      return _contractorsRepo.GetAll();
    }

    internal Contractor GetById(int id)
    {
      return _contractorsRepo.GetById(id);
    }

    internal Contractor Create(Contractor contractorData)
    {
      return _contractorsRepo.Create(contractorData);
    }


    internal Contractor Update(Contractor updatedContractor)
    {
      Contractor original = GetById(updatedContractor.Id);
      original.ContractorName = updatedContractor.ContractorName ?? original.ContractorName;
      original.ContractorLocation = updatedContractor.ContractorLocation ?? original.ContractorLocation;
      _contractorsRepo.Update(original);
      return original;
    }

    internal string Remove(int id, Account user)
    {
      Contractor contractor = GetById(id);
      if (contractor.CreatorId != user.Id)
      {
        throw new System.Exception("can not delete");
      }
      return _contractorsRepo.Remove(id);
    }


    internal List<ContractorCompanyViewModel> GetCompanies(int contractorId)
    {
      return _contractorsRepo.GetCompanies(contractorId);
    }


  }
}