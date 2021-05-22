﻿using System;
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

      StartUp.Dispose();

      Console.ReadKey();
    }
  }
}
