namespace SignalRExample.Business.Interface
{
    public interface IMyBusiness
    {
        Task SendMessageAsync(string message, string userName);
    }
}
