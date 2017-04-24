using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.OperationsSuite.Monitoring.Retrievers.Settings;

namespace Sitecore.OperationsSuite.Monitoring.Retrievers
{
  public interface IStatusRetriever
  {
    Status GetStatus();
    Status GetStatus(IStatusRetrieverSettings settings);
  }
}
