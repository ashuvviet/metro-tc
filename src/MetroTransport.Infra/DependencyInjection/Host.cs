using System;
using Unity;

namespace MetroTransport.Infra
{
  public class Host
  {
    private static readonly Lazy<Host> LazyObject = new Lazy<Host>(() => new Host());

    public static Host Instance => LazyObject.Value;

    /// <summary>
    /// Initializes the specified unity container.
    /// </summary>
    public void Initialize(IUnityContainer c)
    {
      container = c;
    }

    /// <summary>
    /// Access to Container property
    /// </summary>
    private IUnityContainer container;

    public void AddScope<TFrom, TTo>() where TTo : TFrom => container.RegisterType<TFrom, TTo>(TypeLifetime.Scoped);
    public void AddSingleton<TFrom, TTo>() where TTo : TFrom => container.RegisterType<TFrom, TTo>(TypeLifetime.Singleton);
    public T Get<T>() => container.Resolve<T>();
    public void Dispose()
    {
      container?.Dispose();
    }
  }
}