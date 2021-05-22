namespace MetroTransport.Domain
{
  public interface IFareCapRule
  {
    bool CanExecute(int from, int to, BaseCap cap);

    double Execute(int from, int to, double currentFare, BaseCap cap);
  }
}
