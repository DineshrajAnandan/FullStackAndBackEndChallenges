using System;
using System.Threading.Tasks;

namespace BackgroundProcessor.Workers
{
    public class GeneratorManager
    {
        private readonly Random _random = new Random();

        public async Task<int> Generate()
        {
            await Task.Delay(GetRandom(5, 10) * 1000);
            var no = GetRandom(1, 100);
            return no;
        }

        private int GetRandom(int min, int max) => _random.Next(min, max + 1);
    }
}
