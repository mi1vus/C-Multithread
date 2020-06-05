using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading
{
    1
    2 мастер
    class Program
    {
        static List<int> input = new List<int> { 5, 2, 3, 7, 95, 4, 85, 4, 7, 45, 4, 84, 58, 41, 45, 4, 58, 1, 45, 47, 5, 4, 58, 1, 564, 5, 4, 5, 4, 5, 45, 5, 5, 12, 52, 14, 5, 14, 51, 54, 453 };

        static int result = 0;

        static object lockObj = new object();

        public class Parameters {
            public int begin { get; set; }
            public int end { get; set; }
        }

        static void func(object par) {
            var parameters = par as Parameters;
            if (parameters == null)
                return;

            int summ = 0;

            for (int i = parameters.begin; i < parameters.end; ++i)
            {
                summ += input[i];
            }

            lock (lockObj)
            {
                result += summ;
            }
        }

        static void Main(string[] args)
        {
            const int THREADS = 5;
            var size = input.Count;

            List<System.Threading.Thread> threads = new List<System.Threading.Thread>();

            for (int i = 0; i < THREADS; ++i)
            {
                var par = new Parameters();
                double delta = (input.Count * 1.0)/THREADS;
                par.begin = Convert.ToInt32(i * delta);
                par.end = Convert.ToInt32((i + 1) * delta);

                var thread = new System.Threading.Thread(func);
                threads.Add(thread);
                thread.Start(par);
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine("Result:{0}", result);
            int res = input.Sum(it=>it);

            Console.WriteLine("Result2:{0}", res);

            Console.ReadLine();
        }
    }
}
