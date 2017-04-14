using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Sitecore.OperationsSuite.Models.ServiceStatus;
using Sitecore.Configuration;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Presentation;

namespace Sitecore.OperationsSuite.Controllers.ServiceStatus
{
  public class ServiceMaintenanceController : Controller
  {
    protected IEnumerable<ServiceMaintenanceEntryModel> GetMaintenances(Item root, Expression<System.Func<ServiceMaintenanceEntryModelResultItem, bool>> expression)
    {
      Assert.ArgumentNotNull(root, "root");

      ID rID = root.ID;
      int maintenancesLimit = Settings.GetIntSetting("OpsSuite.StatusPage.Maintenances", 5);

      using (var context = ContentSearchManager.GetIndex("sitecore_ops_maintenances_index").CreateSearchContext())
      {
        IQueryable<ServiceMaintenanceEntryModelResultItem> searchQuery
          = context.GetQueryable<ServiceMaintenanceEntryModelResultItem>()
            .Where(item => item.Paths.Contains(rID) && item.ItemId != rID)
            .Where(expression)
            .OrderByDescending(item => item.StartDate)
            .Take(maintenancesLimit);

        return searchQuery.GetResults().Select(sq => new ServiceMaintenanceEntryModel(sq.Document))
          .ToList(); // ugly workaround due to LINQ nature :( - https://sitecorebasics.wordpress.com/2014/03/19/cannot-access-a-disposed-object-object-name-lucenesearchcontext/
      }
    }

    protected Item GetDatasourceItem()
    {
      var datasourceId = RenderingContext.Current.Rendering.DataSource;
      return ID.IsID(datasourceId) ? Context.Database.GetItem(datasourceId) : null;
    }

    protected ActionResult MapMaintenances(string title, bool? inProgress, bool? resolved)
    {
      // Expanded implementation because original one did not work via LINQ compilation
      // Original: item => (!inProgress.HasValue || item.InProgress == inProgress.Value) && (!resolved.HasValue || item.Resolved == resolved.Value)

      Expression<System.Func<ServiceMaintenanceEntryModelResultItem, bool>> expression = i => true;

      if (inProgress.HasValue)
      {
        bool inProgressValue = inProgress.Value;
        expression = expression.And(item => item.InProgress == inProgressValue);
      }

      if (resolved.HasValue)
      {
        bool resolvedValue = resolved.Value;
        expression = expression.And(item => item.Resolved == resolvedValue);
      }

      return View("ServiceMaintenance", new ServiceMaintenanceModel()
      {
        Title = title,
        Entries = this.GetMaintenances(this.GetDatasourceItem(), expression)
      });
    }

    public ActionResult AllMaintenances()
    {
      return MapMaintenances("All Maintenances", null, null);
    }

    public ActionResult PastMaintenances()
    {
      return MapMaintenances("Past Maintenances", null, true);
    }

    public ActionResult ActiveMaintenances()
    {
      return MapMaintenances("Maintenances In Progress", true, false);
    }

    public ActionResult ScheduledMaintenances()
    {
      return MapMaintenances("Scheduled Maintenances", false, false);
    }
  }
}