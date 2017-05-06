using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Caching.Generics;
using Sitecore.Data;
using Sitecore.OperationsSuite.Monitoring;

namespace Sitecore.OperationsSuite.Caching
{
  public class StatusCache : CustomCache<string>
  {
    public StatusCache(string name, long maxSize) : base(name, maxSize)
    {
    }

    public DateTime LastFullUpdate { get; set; }

    public void AddOrUpdate(string key, Status status)
    {
      if (base.GetObject(key) == null)
      {
        base.Remove(key);
      }

      base.SetObject(key, status);
    }

    public Status Get(string key)
    {
      return base.GetObject(key) as Status;
    }
  }
}