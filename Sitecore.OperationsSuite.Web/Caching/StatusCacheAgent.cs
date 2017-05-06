using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.OperationsSuite.Controllers.ServiceStatus;
using Sitecore.OperationsSuite.Monitoring;
using Sitecore.OperationsSuite.Monitoring.Retrievers;
using Sitecore.Reflection;
using Sitecore.Web;

namespace Sitecore.OperationsSuite.Caching
{
  public class StatusCacheAgent
  {
    // Fields
    private readonly string _path;

    // Methods
    public StatusCacheAgent(string path)
    {
      Assert.ArgumentNotNull(path, "path");
      this._path = path;
    }
    
    public void ScanTree(Item item)
    {
      foreach (Item itemChild in item.Children)
      {
        ServiceStatusManager.UpdateStatusCacheFromImplementation(itemChild);

        if (this.IncludeSubItems && itemChild.HasChildren)
        {
          this.ScanTree(itemChild);
        }
      }
    }

    public void Run()
    {
      try
      {
        Item item = Factory.GetDatabase("web").GetItem(this._path);
        this.ScanTree(item);
      }
      catch (Exception exception)
      {
        Log.Error("Exception in StatusCacheAgent (path: " + this._path + ")", exception, this);
      }
    }

    // Properties
    public bool IncludeSubItems { get; set; }
  }
}