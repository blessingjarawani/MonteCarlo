using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Task
    {

        public int WorstCase { get; }
        public int AverageCase { get; }
        public int BestCase { get; }
        private Random random = new Random();
        private List<int> _estimates = new List<int>();

        public Task(int bestCase, int averageCase, int worstCase)
        {
            this.BestCase = bestCase;
            this.AverageCase = averageCase;
            this.WorstCase = worstCase;
        }

        public Task(String input)
        {
            input = input.Trim();

            _estimates = input.Split(',').Select(Int32.Parse).ToList();

            if (_estimates.Count != _estimates.Distinct().ToList().Count)
                throw new Exception("Estimations could not be repeated.");

            this.BestCase = _estimates.Min();
            this.AverageCase = (int)_estimates.Average();
            this.WorstCase = _estimates.Max();
        }

        public int GetRandomEstimate()
        {
            return _estimates[random.Next(0, _estimates.Count)];
        }
    }
}


