using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using MetroTransport.Application.Services;
using MetroTransport.Domain.Contracts;
using MetroTransport.Infra;


namespace MetroTransport.Application
{
  public class Resolver
  {
    private static volatile bool isloaded;

    [ImportMany(typeof(IFareBasedRule))]
    private readonly List<Lazy<IFareBasedRule>> rulesImports = new List<Lazy<IFareBasedRule>>();

    private void Compose<T>(T target)
    {
      var catalog = new AggregateCatalog();
      foreach (var assembly in GetAssemblies())
      {
        catalog.Catalogs.Add(new AssemblyCatalog(assembly));
      }

      var container = new CompositionContainer(catalog);
      container.ComposeParts(target);
    }

    private IEnumerable<Assembly> GetAssemblies()
    {
      var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

      if (string.IsNullOrWhiteSpace(path) == false)
      {
        foreach (var dll in Directory.GetFiles(path, "*.dll"))
        {
          yield return Assembly.LoadFile(dll);
        }
      }
    }

    public void Resolve()
    {
      if (!isloaded)
      {
        Compose(this);
        foreach (var rules in rulesImports)
        {
           Host.Instance.Get<IFareRuleService>().AddFareRule(rules.Value);
        }

        isloaded = true;
      }
    }
  }
}
