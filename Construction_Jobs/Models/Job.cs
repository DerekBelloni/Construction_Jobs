using System;

namespace Construction_Jobs.Models
{


  public class Job
  {
    public int Id { get; set; }

    public int ContractorId { get; set; }

    public int CompanyId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }


  }
}