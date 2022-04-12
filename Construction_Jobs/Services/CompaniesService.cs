using System.Collections.Generic;
using Construction_Jobs.Models;
using Construction_Jobs.Repositories;

namespace Construction_Jobs.Services
{
  public class CompaniesService
  {

    private readonly CompaniesRepository _companiesRepo;

    public CompaniesService(CompaniesRepository companiesRepo)
    {
      _companiesRepo = companiesRepo;
    }
    internal List<Company> GetAll()
    {
      return _companiesRepo.GetAll();
    }

    internal Company Create(Company companyData)
    {
      return _companiesRepo.Create(companyData);
    }

    internal Company GetById(int id)
    {
      return _companiesRepo.GetById(id);
    }

    internal Company Update(Company updatedCompany)
    {
      Company original = GetById(updatedCompany.Id);
      original.CompanyName = updatedCompany.CompanyName ?? original.CompanyName;
      original.CompanyLocation = updatedCompany.CompanyLocation ?? original.CompanyLocation;
      _companiesRepo.Update(original);
      return original;
    }

    internal string Remove(int id, Account user)
    {
      Company company = GetById(id);
      if (company.CreatorId != user.Id) 
      {
        throw new System.Exception("can not delete");
      }
      return _companiesRepo.Remove(id);
    }
  }
}