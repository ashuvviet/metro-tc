using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroTransport.Application.Services;
using MetroTransport.Infra;

namespace MetroTransport.Application
{
  public class StartUp
  {
    public static void Initialize()
    {
      Host.Instance.AddScope<IFareRuleService, FareRuleService>();
      new Resolver().Resolve();
    }
  }
}
