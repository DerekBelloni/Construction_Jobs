namespace Construction_Jobs.Models
{
  public class Contractor
  {
    public int Id { get; set; }
    public string ContractorName { get; set; }
    public string ContractorLocation { get; set; }

    public string CreatorId { get; set; }


  }

  public class ContractorCompanyViewModel : Contractor
  {
    public int ContractorCompanyId { get; set; }

    public string CompanyName { get; set; }

    public string CompanyLocation { get; set; }





  }
}