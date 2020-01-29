using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Plan
    {
        private List<Task> _tasks = new List<Task>();

        public int _iterations { get; set; } = 10000;

        public void AddTask(Task task)
        {
            _tasks.Add(task);
        }

        public (int, int, int) EstimateTime()
        {
            var min = 0;
            var max = 0;
            var avg = 0;

            foreach (Task task in _tasks)
            {

                avg += task.AverageCase;
                min += task.BestCase;
                max += task.WorstCase;
            }

            return (min, max, avg);
        }

        public Bucket Simulate()
        {
            int totalCostOfRandomPlans = 0;
            var values = EstimateTime();
            int dif = (values.Item2 - values.Item1);
            int div = (dif / 10);
            if (dif == 0 && div == 0) dif = div = 1;
            var bucket = new Bucket(dif / (div != 0 ? div : 1), values.Item1, values.Item2, this._iterations);

            for (int i = 0; i < _iterations; i++)
            {
                int randomPlanCost = 0;
                foreach (Task task in _tasks)
                    randomPlanCost += task.GetRandomEstimate();
                bucket.AddValueToBucket(randomPlanCost);

                totalCostOfRandomPlans += randomPlanCost;
            }

            return bucket;
        }
    }
}
