using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Security;
using System.Web.SessionState;
using Sitecore.Diagnostics;

namespace Sitecore.OperationsSuite.Web
{
  public class Global : Sitecore.Web.Application
  {
    public override void Init()
    {
      base.Init();
      BundleConfig.RegisterBundles(BundleTable.Bundles);
    }
  }
}