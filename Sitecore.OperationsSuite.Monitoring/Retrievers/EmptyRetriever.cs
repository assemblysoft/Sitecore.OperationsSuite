using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.OperationsSuite.Monitoring.Retrievers.Settings;

namespace Sitecore.OperationsSuite.Monitoring.Retrievers
{
  public class EmptyRetriever : IStatusRetriever
  {
    public Status GetStatus()
    {
      return new Status()
      {
        Message = "Automatic status",
        Severity = StatusSeverity.Information
      };
    }

    public Status GetStatus(IStatusRetrieverSettings settings)
    {
      return this.GetStatus();
    }
  }
}
