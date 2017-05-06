using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Sitecore.OperationsSuite.Models.ServiceStatus;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Globalization;
using Sitecore.Mvc.Extensions;
using Sitecore.Mvc.Presentation;
using Sitecore.OperationsSuite.Caching;
using Sitecore.OperationsSuite.Monitoring;
using Sitecore.OperationsSuite.Monitoring.Retrievers;
using Sitecore.Reflection;
using Version = Sitecore.Data.Version;

namespace Sitecore.OperationsSuite.Controllers.ServiceStatus
{
  public class ServiceStatusController : Controller
  {
    public static ID MetricTemplateId = ID.Parse("{795EA735-F4CE-4D43-87CD-17D5A0078EFF}");
    public static ID MetricGroupTemplateId = ID.Parse("{3E7DBE8A-3688-426F-8F4A-28F02764EFDE}");

    public static ID StatusDefinitions = ID.Parse("{AAB0A90D-029C-46D4-AAE5-14547DECF9A6}");

    public Item GetDatasourceItem()
    {
      var datasourceId = RenderingContext.Current.Rendering.DataSource;
      return ID.IsID(datasourceId) ? Context.Database.GetItem(datasourceId) : null;
    }

    protected ID GetStatusItemIdByStatusSeverity(StatusSeverity statusSeverity)
    {
      var statuses = Context.Database.GetItem(StatusDefinitions);

      foreach (Item item in statuses.GetChildren())
      {
        if (int.Parse(item["Severity"]) == (int)statusSeverity)
        {
          return item.ID;
        }
      }

      return null;
    }

    protected ServiceStatusModel GetDefaultStatusValues(ID statusItemId)
    {
      var statusItemResolved = ID.IsNullOrEmpty(statusItemId) ? null : Context.Database.GetItem(statusItemId);
      if (statusItemResolved != null)
      {
        return new ServiceStatusModel()
        {
          Color = statusItemResolved["Color"],
          Status = statusItemResolved["Name"],
          Message = statusItemResolved["Default Message"]
        };
      }

      return new ServiceStatusModel()
      {
        Color = "white",
        Message = "No information",
        Status = "Undefined"
      };
    }

    protected Status GetStatusFromCache(Item item)
    {
      Status statusCached = StatusCacheManager.GetStatusCache().Get(item.ID.ToString());
      if (statusCached == null)
      {
        // update cache
        new Task(() => ServiceStatusManager.UpdateStatusCacheFromImplementation(item)).Start();

        return new Status()
        {
          Message = "Retrieving status info, please reload the page",
          Severity = StatusSeverity.Running
        };
      }

      return statusCached;
    }

    protected ServiceStatusModel ItemDefinitionToViewModel(Item item)
    {
      if (item.TemplateID == MetricGroupTemplateId)
      {
        return new ServiceStatusModel()
        {
          Color = "gray",
          Message = "See more",
          Name = item["Name"],
          Status = "Undefined"
        };
      }

      if (item.TemplateID == MetricTemplateId)
      {
        if (item["Activate Static Mode"] == "1")
        {
          ID statusItem;
          var overrideDefaults = (ID.TryParse(item["Status"], out statusItem)) ? this.GetDefaultStatusValues(statusItem) : new ServiceStatusModel();
          
          return new ServiceStatusModel()
          {
            Color = overrideDefaults.Color,
            Message = item["Message"] ?? overrideDefaults.Message,
            Name = item["Name"],
            Status = overrideDefaults.Status
          };
        }
        else if (!string.IsNullOrEmpty(item["Implementation"]))
        {
          Status result = this.GetStatusFromCache(item);

          ID statusItemId = this.GetStatusItemIdByStatusSeverity(result.Severity);
          var statusDefaults = this.GetDefaultStatusValues(statusItemId);

          return new ServiceStatusModel()
          {
            Color = statusDefaults.Color,
            Message = result.Message ?? statusDefaults.Message,
            Name = item["Name"],
            Status = statusDefaults.Status
          };
        }
      }

      // if fail to recognize - everything is OK :)
      return new ServiceStatusModel()
      {
        Color = "green",
        Message = "Service is operating normally",
        Name = item["Name"],
        Status = "Running"
      };
    }

    // GET: ServiceStatus
    public ActionResult ServiceStatus()
    {
      var metrics = this.GetDatasourceItem();

      var model = metrics.GetChildren().Select(this.ItemDefinitionToViewModel).Where(i => i != null);

      return View(model);
    }
  }
}