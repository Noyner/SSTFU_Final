using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface IDetectorConveyor
    {
        public Task HandleQueue();
    }
}