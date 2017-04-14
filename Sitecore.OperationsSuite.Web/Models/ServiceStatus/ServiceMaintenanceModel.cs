using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Sitecore.Configuration;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Presentation;
using Sitecore.Shell.Applications.ContentEditor;
using DateTime = System.DateTime;

namespace Sitecore.OperationsSuite.Models.ServiceStatus
{
  public class ServiceMaintenanceModel
  {
    public string Title { get; set; }

    public IEnumerable<ServiceMaintenanceEntryModel> Entries { get; set; }
    
  }

  public class ServiceMaintenanceEntryModelResultItem : SearchResultItem
  {
    [IndexField("name")]
    public string NameField { get; set; }

    [IndexField("description")]
    public string Description { get; set; }

    [IndexField("start_date")]
    public DateTime StartDate { get; set; }

    [IndexField("end_date")]
    public DateTime EndDate { get; set; }

    [IndexField("in_progress")]
    public bool InProgress { get; set; }

    [IndexField("resolved")]
    public bool Resolved { get; set; }
  }

  public class ServiceMaintenanceEntryModel : IRenderingModel
  {
    public ServiceMaintenanceEntryModel()
    {
      
    }

    public ServiceMaintenanceEntryModel(ServiceMaintenanceEntryModelResultItem searchResult)
    {
      var item = searchResult;

      this.Name = item.NameField;
      this.Description = item.Description;

      this.StartDate = item.StartDate;
      this.EndDate = item.EndDate;

      this.InProgress = item.InProgress;
      this.Resolved = item.Resolved;
    }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool InProgress { get; set; }

    public bool Resolved { get; set; }

    public void Initialize(Rendering rendering)
    {
      this.Name = rendering.Item["Name"];
      this.Description = rendering.Item["Description"];

      DateTime startDate;
      this.StartDate = DateTime.TryParse(rendering.Item["Start Date"], out startDate) ? startDate : DateTime.MinValue;

      DateTime endDate;
      this.EndDate = DateTime.TryParse(rendering.Item["End Date"], out endDate) ? endDate : DateTime.MaxValue;

      this.InProgress = rendering.Item["In Progress"] == "1";
      this.Resolved = rendering.Item["Resolved"] == "1";
    }
  }
}