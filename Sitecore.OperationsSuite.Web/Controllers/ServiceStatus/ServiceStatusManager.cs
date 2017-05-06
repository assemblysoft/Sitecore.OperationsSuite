using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.OperationsSuite.Caching;
using Sitecore.OperationsSuite.Monitoring;
using Sitecore.OperationsSuite.Monitoring.Retrievers;
using Sitecore.Reflection;

namespace Sitecore.OperationsSuite.Controllers.ServiceStatus
{
  public class ServiceStatusManager
  {
    public static Status UpdateStatusCacheFromImplementation(Item item)
    {
      return UpdateStatusCacheFromImplementation(item, string.Empty);
    }

    public static Status UpdateStatusCacheFromImplementation(Item item, string extraParameters)
    {
      if (item == null || string.IsNullOrEmpty(item["Implementation"]))
      {
        return null;
      }

      IStatusRetriever statusRetriever = ReflectionUtil.CreateObject(Type.GetType(item["Implementation"])) as IStatusRetriever;
      if (statusRetriever != null)
      {
        // Get Parameters
        // NO IMPLEMENTATION NOW

        // Execute
        var result = statusRetriever.GetStatus();

        // add to cache
        StatusCacheManager.GetStatusCache().AddOrUpdate(item.ID.ToString(), result);

        return result;
      }

      return null;
    }
  }
}