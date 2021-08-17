using System.Threading.Tasks;
using App.Docker.Domain.Messages;

namespace App.Docker.Domain.Communication
{
    public interface IMediatorHandler
    {
        Task<bool> SendCommand<T>(T command) where T : Command;

        Task PublishNotification<T>(T notification) where T : DomainNotification;
    }
}
