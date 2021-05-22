using System;
using System.ComponentModel.Composition;

namespace MetroTransport.Infra.Attributes
{
  public class CapRuleAttribute : ExportAttribute
  {
    public int Order { get; }

    public CapRuleAttribute(int order, Type contracType) : base(contracType)
    {
      Order = order;
    }
  }
}
