using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Configuration;

namespace Sitecore.OperationsSuite.Caching
{
  public class StatusCacheManager
  {
    private static StatusCache statusCache;

    public static StatusCache GetStatusCache()
    {
      if (statusCache == null)
      {
        var size = StringUtil.ParseSizeString(Settings.GetSetting("Caching.StatusCacheSize", "500KB"));
        statusCache = new StatusCache("StatusCache", size);
      }

      return statusCache;
    }
  }
}