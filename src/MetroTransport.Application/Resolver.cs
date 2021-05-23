using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using MetroTransport.Application.Services;
using MetroTransport.Domain;
using MetroTransport.Infra;
using MetroTransport.Infra.Attributes;


namespace MetroTransport.Application
{
  public class Resolver
  {
    [ImportMany(typeof(IFareBasedRule))]
    private readonly List<Lazy<IFareBasedRule>> _fareRulesImports = new List<Lazy<IFareBasedRule>>();

    [ImportMany(typeof(IFareCapRule))]
    private readonly List<Lazy<IFareCapRule>> _fareCapRulesImports = new List<Lazy<IFareCapRule>>();

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
        Compose(this);
        foreach (var rules in _fareRulesImports)
        {
           Host.Instance.Get<IFareRuleService>().AddFareRule(rules.Value);
        }

        foreach (var rules in _fareCapRulesImports.OrderBy(m => m.Value.GetType().GetCustomAttributes(true).OfType<CapRuleAttribute>().Single().Order))
        {
          Host.Instance.Get<IFareCapService>().AddCapFareRule(rules.Value);
        }
    }
  }
}
