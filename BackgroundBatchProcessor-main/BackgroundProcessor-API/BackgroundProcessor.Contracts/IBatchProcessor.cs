using System.Threading.Tasks;

namespace BackgroundProcessor.Contracts
{
    public interface IBatchProcessor
    {
        Task StartProcess(int batchesCount, int numbersCount);
    }
}
