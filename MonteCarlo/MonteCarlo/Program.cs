using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = BLL.Models.Task;

namespace MonteCarlo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter tasks in the following format: c1,c2,...  where X is cost");
                Console.WriteLine("Type END to finish entering tasks");

                var counter = 0;
                Plan plan = new Plan();

                while (true)
                {
                    Console.Write($"Task #{++counter}: ");
                    String input = Console.ReadLine();
                    plan.AddTask(new Task(input));
                    if (input.Trim().ToLower() == "end")
                        break;


                }

                Bucket bucket = plan.Simulate();
                var calculatedTime = bucket.CalculateTime();
                output(plan, bucket, calculatedTime);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().Message);
            }
        }

        private static void output(Plan plan, Bucket bucket, (int, int, int) calculatedTime)
        {
            Console.WriteLine($"After probing {plan._iterations} random plans, the results are:");
            Console.WriteLine($"Minimum = {calculatedTime.Item1} days");
            Console.WriteLine($"Maximum = {calculatedTime.Item2} days");
            Console.WriteLine($"Average = {calculatedTime.Item3} days");

            Console.WriteLine("Probability of finishing the plan in:\n" + bucket);

            bucket.ToAccumulated();

            Console.WriteLine("Accumulated probability of finishing the plan in or before:\n" + bucket);
        }
    }
    }

