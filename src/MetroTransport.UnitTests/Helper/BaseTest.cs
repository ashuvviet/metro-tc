using System;
using MetroTransport.Domain;
using MetroTransport.Infra;
using MetroTransport.Infra.Services;
using NSubstitute;
using Unity;

namespace MetroTransport.UnitTests.Helper
{
  public abstract class BaseTest : IDisposable
  {
    protected IUnityContainer mockContainer;

    protected BaseTest()
    {
      this.mockContainer = Substitute.For<IUnityContainer>();
      Host.Instance.Initialize(mockContainer);

      mockContainer.Resolve<ILogService>().Returns(Substitute.For<LogService>());
    }

    protected Journey GetNewJourney()
    {
      var datetime = new DateTime(2021, 5, 24, 10, 0, 0, DateTimeKind.Local);
      return new Journey(new Zone(1, "central"), new Zone(1, "concentric"), datetime);
    }

    protected Journey GetNewJourney(int sourcezone, int destinationzone)
    {
      var datetime = new DateTime(2021, 5, 24, 10, 0, 0, DateTimeKind.Local);
      return new Journey(new Zone(sourcezone, "central"), new Zone(destinationzone, "concentric"), datetime);
    }

    public virtual void Dispose()
    {
      this.mockContainer?.Dispose();
    }
  }
}
