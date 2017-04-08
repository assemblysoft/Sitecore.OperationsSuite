using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.OperationsSuite.Web.Models.ServiceStatus;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Globalization;
using Sitecore.Mvc.Extensions;
using Sitecore.Mvc.Presentation;
using Version = Sitecore.Data.Version;

namespace Sitecore.OperationsSuite.Web.Controllers.ServiceStatus
{
  public class ServiceStatusController : Controller
  {
    public Item GetDatasourceItem()
    {
      var datasourceId = RenderingContext.Current.Rendering.DataSource;
      return ID.IsID(datasourceId) ? Context.Database.GetItem(datasourceId) : null;
    }

    // GET: ServiceStatus
    public ActionResult ServiceStatus()
    {
      var metrics = this.GetDatasourceItem();

      var model = metrics.GetChildren().Select(
        i => new ServiceStatusModel()
        {
          Color = "green",
          Message = "Service is operating normally",
          Name = i["Name"],
          Status = "Running"
        });

      return View(model);
    }
  }
}