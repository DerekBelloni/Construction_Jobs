using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Construction_Jobs.Models;
using Dapper;

namespace Construction_Jobs.Repositories
{
  public class ContractorsRepository
  {
    private readonly IDbConnection _db;

    public ContractorsRepository(IDbConnection db)
    {
      _db = db;
    }




    internal List<Contractor> GetAll()
    {
      string sql = @"
     SELECT * FROM contractors;
     ";
      return _db.Query<Contractor>(sql).ToList();
    }







    internal Contractor GetById(int id)
    {
      string sql = @"
     SELECT
     c.*
     FROM contractors c
     WHERE c.id = @id;
     ";
      return _db.Query<Contractor>(sql, new { id }).FirstOrDefault();
    }

    internal Contractor Create(Contractor contractorData)
    {
      string sql = @"
      INSERT INTO contractors
      (contractorName, contractorLocation, creatorId)
      VALUES
      (@ContractorName, @ContractorLocation, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, contractorData);
      contractorData.Id = id;
      return contractorData;
    }


    internal void Update(Contractor original)
    {
      string sql = @"
      UPDATE contractors
      SET
        contractorName = @ContractorName,
        contractorLocation = @ContractorLocation
        WHERE id = @Id;";
      _db.Execute(sql, original);
    }
    internal string Remove(int id)
    {
      string sql = @"
      DELETE FROM contractors WHERE id = @id LIMIT 1;
      ";
      int rowsAffected = _db.Execute(sql, new { id });
      if (rowsAffected > 0)
      {
        return "deleted";
      }
      throw new Exception("could not delete");
    }

    internal List<ContractorCompanyViewModel> GetCompanies(int contractorId)
    {
      string sql = @"
      SELECT
      j.*,
      cont.*,
      comp.*
      FROM jobs j
      JOIN contractors cont ON cont.id = j.contractorId
      JOIN companies comp ON comp.id = j.companyId
      WHERE j.contractorId = @contractorId;
      ";
      return _db.Query<Job, ContractorCompanyViewModel, Company, ContractorCompanyViewModel>(sql, (j, ccvm, comp) =>
      {
        ccvm.ContractorCompanyId = j.Id;
        ccvm.CompanyLocation = comp.CompanyLocation;
        ccvm.CompanyName = comp.CompanyName;
        return ccvm;
      }, new { contractorId }).ToList();


    }
  }
}