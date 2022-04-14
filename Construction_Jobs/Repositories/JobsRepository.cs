using System.Data;
using Construction_Jobs.Models;
using Dapper;

namespace Construction_Jobs.Repositories
{
  public class JobsRepository
  {

    private readonly IDbConnection _db;

    public JobsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal Job Create(Job jobData)
    {
      string sql = @"
    INSERT INTO jobs
    (companyId, contractorId)
    VALUES
    (@CompanyId, @contractorId);
    SELECT LAST_INSERT_ID();
   ";
      int id = _db.ExecuteScalar<int>(sql, jobData);
      jobData.Id = id;
      return jobData;
    }
  }
}