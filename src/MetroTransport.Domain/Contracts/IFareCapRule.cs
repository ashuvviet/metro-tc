using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTransport.Domain.Contracts
{
  public interface IFareCapRule
  {
    bool CanExecute(Zone source, Zone destination);

    (int DailyCap, int WeeklyCap) Execute(Zone source, Zone destination);
  }
}
