using System.Threading.Tasks;

namespace App.Docker.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
