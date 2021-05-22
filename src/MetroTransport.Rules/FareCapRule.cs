using System;
using System.ComponentModel.Composition;
using System.Reflection.Metadata.Ecma335;
using MetroTransport.Domain;
using MetroTransport.Domain.Contracts;
using MetroTransport.Infra;


namespace MetroTransport.Rules
{
  [Export(typeof(IFareCapRule))]
  public class FareCapRule : IFareCapRule
  {
    public bool CanExecute(Zone source, Zone destination) => true;

    public (int DailyCap, int WeeklyCap) Execute(Zone source, Zone destination)
    {
      switch (source.Id, destination.Id)
      {
        case (1, 1):
          return (100, 500);
        case (1, 2):
        case (2, 1):
          return (120, 600);
        case (2, 2):
          return (80, 400);
      }

      return (default, default);
    }
  }
}
