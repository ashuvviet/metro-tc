using System;
using System.Collections.Generic;
using MetroTransport.Application;
using MetroTransport.Application.Services;
using MetroTransport.Domain;
using MetroTransport.Infra;

namespace MetroTransport.UIConsole
{
  class Program
  {
    static void Main(string[] args)
    {
      StartUp.Initialize();

      Console.WriteLine("out of use case2 given in case study");

      var datetime = new DateTime(2021, 5, 24, 10, 0, 0, DateTimeKind.Local);
      var journeys = new List<Journey>()
      {
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime),
        new Journey(new Zone(1, "central"), new Zone(2, "concentric"),  datetime),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddHours(6)),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddHours(7)),

        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(1)),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(1)),
        new Journey(new Zone(1, "central"), new Zone(2, "concentric"),  datetime.AddDays(1)),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(1).AddHours(6)),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(1).AddHours(7)),

        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(2)),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(2)),
        new Journey(new Zone(1, "central"), new Zone(2, "concentric"),  datetime.AddDays(2)),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(2).AddHours(6)),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(2).AddHours(7)),

        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(3)),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(3)),
        new Journey(new Zone(1, "central"), new Zone(2, "concentric"),  datetime.AddDays(3)),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(3).AddHours(6)),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(3).AddHours(7)),

        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(4)),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(4)),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(4).AddHours(6)),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(4).AddHours(7)),

        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(5)),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(5)),
        new Journey(new Zone(1, "central"), new Zone(2, "concentric"),  datetime.AddDays(5)),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(5).AddHours(6)),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(5).AddHours(7)),

        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(6)),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(6)),
        new Journey(new Zone(1, "central"), new Zone(2, "concentric"),  datetime.AddDays(6)),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(6).AddHours(6)),
        new Journey(new Zone(2, "central"), new Zone(2, "concentric"),  datetime.AddDays(6).AddHours(7)),

        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(7)),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(7)),
        new Journey(new Zone(1, "central"), new Zone(2, "concentric"),  datetime.AddDays(7)),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddDays(7).AddHours(6)),
        new Journey(new Zone(2, "concentric"), new Zone(2, "concentric"),  datetime.AddDays(7).AddHours(7)),
      };

      var fare = Host.Instance.Get<IFareRuleService>().CalculateFare(journeys);

      Console.WriteLine($"Output:{fare}");

      Host.Instance.Get<IFareCapService>().GenerateReport();

      //// Enable to Do perform more tests

      //Console.WriteLine("enter more journeys to card to tests more cases");
      //var numberofJourneies = int.Parse(Console.ReadLine());
      //var journeys2 = new List<Journey>();
      //while (numberofJourneies > 0)
      //{
      //  Console.WriteLine("enter 'from' zone : ");
      //  var sourceZone = int.Parse(Console.ReadLine());

      //  Console.WriteLine("enter 'to' zone : ");
      //  var destinationZone = int.Parse(Console.ReadLine());


      //  Console.WriteLine("enter to punch time in YYYY-MM-DD HH:MM tt e.g.  2005-05-05 22:12 PM : ");
      //  var punchtime = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm tt", System.Globalization.CultureInfo.InvariantCulture);

      //  journeys2.Add(new Journey(new Zone(sourceZone), new Zone(destinationZone), punchtime));
      //  numberofJourneies--;
      //}

      //var fare2 = Host.Instance.Get<IFareRuleService>().CalculateFare(journeys2);
      //Console.WriteLine($"Output:{fare2}");
      //Host.Instance.Get<IFareCapService>().GenerateReport();

      StartUp.Dispose();

      Console.ReadKey();
    }
  }
}
