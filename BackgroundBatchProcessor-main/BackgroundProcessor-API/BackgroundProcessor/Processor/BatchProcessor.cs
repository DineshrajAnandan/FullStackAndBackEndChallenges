using BackgroundProcessor.Contracts;
using BackgroundProcessor.Workers;
using System.Threading.Tasks;

namespace BackgroundProcessor.Processor
{
    public class BatchProcessor : IBatchProcessor
    {
        private readonly GeneratorManager _generatorManager;
        private readonly MultiplierManager _multiplierManager;
        private readonly IDbAccessService _dbAccessService;

        private static bool _processRunning = false;

        public static bool IsProcessRunning
        {
            get
            {
                return _processRunning;
            }
        }

        public BatchProcessor(GeneratorManager generatorManager,
            MultiplierManager multiplierManager,
            IDbAccessService dbAccessService)
        {
            _generatorManager = generatorManager;
            _multiplierManager = multiplierManager;
            _dbAccessService = dbAccessService;
        }

        public async Task StartProcess(int batchesCount, int numbersCount)
        {
            _processRunning = true;
            await _dbAccessService.CreateNewBatchGroup();
            int[] createdBatches = new int[batchesCount];
            for (int i = 0; i < batchesCount; i++)
            {
                createdBatches[i] = await _dbAccessService.AddBatch(numbersCount);
            }

            foreach (var batchId in createdBatches)
            {
                for (int j = 0; j < numbersCount; j++)
                {
                    var generatedNo = await _generatorManager.Generate();
                    var multipliedValue = await _multiplierManager.Multiply(generatedNo);
                    await _dbAccessService.UpdateBatchAfterMultiply(batchId, multipliedValue);
                }
            }

            await _dbAccessService.CompleteBatchGroup();
            _processRunning = false;
        }
    }
}
