using BackgroundProcessor.DB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackgroundProcessor.Contracts
{
    public interface IDbAccessService
    {
        Task CreateNewBatchGroup();
        Task CompleteBatchGroup();
        Task<int> AddBatch(int numbersCount);
        Task UpdateBatchAfterMultiply(int batchNo, int number);
        List<Batch> GetCurrentBatches();
        List<Batch> GetPreviousBatches();
    }
}
