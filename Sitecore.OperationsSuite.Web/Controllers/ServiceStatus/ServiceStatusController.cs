using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.OperationsSuite.Models.ServiceStatus;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Globalization;
using Sitecore.Mvc.Extensions;
using Sitecore.Mvc.Presentation;
using Version = Sitecore.Data.Version;

namespace Sitecore.OperationsSuite.Controllers.ServiceStatus
{
  public class ServiceStatusController : Controller
  {
    public static ID MetricTemplateId = ID.Parse("{795EA735-F4CE-4D43-87CD-17D5A0078EFF}");
    public static ID MetricGroupTemplateId = ID.Parse("{3E7DBE8A-3688-426F-8F4A-28F02764EFDE}");

    public Item GetDatasourceItem()
    {
      var datasourceId = RenderingContext.Current.Rendering.DataSource;
      return ID.IsID(datasourceId) ? Context.Database.GetItem(datasourceId) : null;
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
          var staticColor = "white";
          var staticStatusName = "None";
          var defaultStaticMessage = string.Empty;

          ID statusItem;
          if (ID.TryParse(item["Status"], out statusItem))
          {
            var statusItemResolved = Context.Database.GetItem(statusItem);
            if (statusItemResolved != null)
            {
              staticColor = statusItemResolved["Color"];
              staticStatusName = statusItemResolved["Name"];
              defaultStaticMessage = statusItemResolved["Default Message"];
            }
          }

          return new ServiceStatusModel()
          {
            Color = staticColor,
            Message = item["Message"] ?? defaultStaticMessage,
            Name = item["Name"],
            Status = staticStatusName
          };
        }
        else
        {
          return new ServiceStatusModel()
          {
            Color = "green",
            Message = "Service is operating normally def",
            Name = item["Name"],
            Status = "Running"
          };
        }
      }

      return null;
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