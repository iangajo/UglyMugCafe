using System.Threading.Tasks;

namespace SignalRHub.Domains
{
    public interface IHubClient
    {
        Task BroadcastMessage(string type, string payload);
    }
}
