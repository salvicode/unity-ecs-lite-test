namespace Server
{
    public interface ITimeServiceProvider
    {
        TimeService TimeService { get; }
    }
}