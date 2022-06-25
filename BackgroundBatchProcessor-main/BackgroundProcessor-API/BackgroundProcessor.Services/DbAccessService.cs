using BackgroundProcessor.Contracts;
using BackgroundProcessor.DB;
using BackgroundProcessor.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundProcessor.Services
{
    public class DbAccessService: IDbAccessService
    {
        private readonly ProcessContext _processContext;

        public DbAccessService(ProcessContext processContext)
        {
            _processContext = processContext;
        }

        public async Task CreateNewBatchGroup()
        {
            _processContext.BatchesGroup.Add(new BatchGroup());
            await _processContext.SaveChangesAsync();
        }

        public async Task CompleteBatchGroup()
        {
            var batchGroup = _processContext.BatchesGroup.LastOrDefault();
            batchGroup.IsComplete = true;
            await _processContext.SaveChangesAsync();
        }

        public async Task<int> AddBatch(int numbersCount)
        {
            var batchGroup = _processContext.BatchesGroup.LastOrDefault();
            if (batchGroup == null) throw new Exception("BatchGroup not found");
            var newBatch = new Batch
            {
                TotalNumbers = numbersCount,
                BatchGroupId = batchGroup.GroupId
            };
            _processContext.Batch.Add(newBatch);
            await _processContext.SaveChangesAsync();
            return newBatch.BatchId;
        }

        public async Task UpdateBatchAfterMultiply(int batchNo, int number)
        {
            var batch = _processContext.Batch.Where(b => b.BatchId == batchNo).FirstOrDefault();
            batch.ProcessedNumbers += 1;
            batch.BatchTotal += number;
            await _processContext.SaveChangesAsync();
        }

        public List<Batch> GetCurrentBatches()
        {
            var batchGroup = _processContext.BatchesGroup.LastOrDefault();
            var batchesList = _processContext.Batch.Where(b => b.BatchGroupId == batchGroup.GroupId).ToList();
            return batchesList;
        }

        public List<Batch> GetPreviousBatches()
        {
            var batchGroups = _processContext.BatchesGroup
                        .OrderByDescending(bg=>bg.GroupId)
                        .Take(2).ToList();
            batchGroups.Reverse();

            if (batchGroups != null && batchGroups.Count() >0)
            {
                var batchGroup = batchGroups.First();
                var batchesList = _processContext.Batch.Where(b => b.BatchGroupId == batchGroup.GroupId).ToList();
                return batchesList;
            }
            return null;
        }
    }
}
