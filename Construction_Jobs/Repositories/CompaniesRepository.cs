using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Construction_Jobs.Models;
using Dapper;

namespace Construction_Jobs.Repositories
{
  public class CompaniesRepository
  {
    private readonly IDbConnection _db;

    public CompaniesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Company> GetAll()
    {
      string sql = @"
      SELECT * FROM companies";
      return _db.Query<Company>(sql).ToList();
    }

    internal Company Create(Company companyData)
    {
      string sql = @"
      INSERT INTO companies
      (companyName, companyLocation, creatorId)
      VALUES
      (@CompanyName, @CompanyLocation, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, companyData);
      companyData.Id = id;
      return companyData;
    }

    internal Company GetById(int id)
    {
      string sql = @"
      SELECT
      c.*
      FROM companies c
      WHERE c.id = @id;
      ";
      return _db.Query<Company>(sql, new { id }).FirstOrDefault();
    }

    internal void Update(Company original)
    {
      string sql = @"
      UPDATE companies
      SET
        companyName = @CompanyName,
        companyLocation = @CompanyLocation
        WHERE id = @Id;";

      _db.Execute(sql, original);

    }


    internal string Remove(int id)
    {
      string sql = @"
      DELETE FROM companies WHERE id = @id LIMIT 1;
      ";
      int rowsAffected = _db.Execute(sql, new { id });
      if (rowsAffected > 0)
      {
        return "deleted";
      }
      throw new Exception("could not delete");
    }
  }
}