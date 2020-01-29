using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Bucket
    {
        public Dictionary<int, int> bucketsDictionary = new Dictionary<int, int>();
        private List<int> numbers = new List<int>();
        private int count;
        private int rangeLow;
        private int rangeHigh;
        private int stepSize;
        private int numberOfIterations;

        public Bucket(int bucketCount, int rangeLow, int rangeHigh, int numberOfIterations)
        {
            this.count = bucketCount;
            this.rangeLow = rangeLow;
            this.rangeHigh = rangeHigh;
            this.stepSize = (Math.Abs(rangeHigh) - Math.Abs(rangeLow)) / bucketCount;
            this.numberOfIterations = numberOfIterations;

            this.InitializeBuckets();
        }

        public void InitializeBuckets()
        {
            for (int counter = 0; counter < count; counter ++)
                this.bucketsDictionary.Add(this.rangeLow + (this.stepSize * counter), 0);
        }

        public int GetBacketValue(int number)
        {
            var idx = 0;
            while ((idx < this.count - 1) && number > this.rangeLow + this.stepSize * (idx + 1))
                idx++;

            return idx;
        }
        public void ToAccumulated()
        {
            for (int i = 1; i < bucketsDictionary.Count; i++)
                bucketsDictionary[bucketsDictionary.ElementAt(i).Key] = bucketsDictionary.ElementAt(i - 1).Value + bucketsDictionary.ElementAt(i).Value;
        }
        public void AddValueToBucket(int number)
        {
            var bucketidxvalue = GetBacketValue(number);
            bucketsDictionary[bucketsDictionary.ElementAt(bucketidxvalue).Key]++;
            numbers.Add(number);
        }

        public (int, int, int) CalculateTime()
        {
            return (numbers.Min(), numbers.Max(), (int)numbers.Average());
        }     
     
    }
}

