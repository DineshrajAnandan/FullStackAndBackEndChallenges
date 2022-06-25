using System;
using System.Threading.Tasks;

namespace BackgroundProcessor.Workers
{
    public class MultiplierManager
    {
        private readonly Random _random = new Random();

        public async Task<int> Multiply(int no)
        {
            await Task.Delay(GetRandom(5, 10) * 1000);
            var val = no * GetRandom(2, 4);
            return val;
        }

        private int GetRandom(int min, int max) => _random.Next(min, max + 1);
    }
}
