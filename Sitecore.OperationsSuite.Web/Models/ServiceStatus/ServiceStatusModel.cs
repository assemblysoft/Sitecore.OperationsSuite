using System;
using System.Collections.Generic;

namespace Sitecore.OperationsSuite.Web.Models.ServiceStatus
{

  public class StatusModel
  {
    public IEnumerable<ServiceStatusModel> ServicesStatus { get; set; }
  }

  public class ServiceStatusModel
  {
    public Int64 Id { get; set; }
    public String Name { get; set; }
    public String Status { get; set; }
    public String Message { get; set; }
    public String Color { get; set; }

    public IEnumerable<EndpointStatusModel> Services { get; set; }
  }

  public class EndpointStatusModel
  {
    public Int64 Id { get; set; }
    public String Name { get; set; }
    public String Status { get; set; }
    public String Message { get; set; }
    public String Color { get; set; }
  }
}