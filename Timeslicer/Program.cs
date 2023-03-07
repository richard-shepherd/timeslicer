using System.Diagnostics;

namespace Timeslicer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var t1 = new Thread(threadStart_LowPriority);
            t1.Start();
            var t2 = new Thread(threadStart_Traceability);
            t2.Start();

            var total = 0.0;
            var count = 0;
            var stopwatch = new Stopwatch();
            for(; ;)
            {
                //burnCPU(5);

                stopwatch.Restart();
                var ms = stopwatch.ElapsedMilliseconds;
                if(ms > 1)
                {
                    total += ms;
                    count++;
                    Console.WriteLine($"{ms}ms, avg={total/count}");
                }

                //Thread.Sleep(1);
            }
        }

        private static void burnCPU(int ms)
        {
            var offset = 0.1 * m_rnd.NextDouble() * ms;
            var msToBurn = ms + offset;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var total = 1.0;
            while(stopwatch.ElapsedMilliseconds < msToBurn)
            {
                total *= 1.000001;
                if(total > 1000000.0)
                {
                    total = 1.0;
                }
            }
            if (total < 1.00001)
            {
                Console.WriteLine($"Total={total}");
            }
        }

        private static void threadStart_LowPriority(object? obj)
        {
            var rnd = new Random();
            for(; ;)
            {
                var total = 0.0;
                for(var i = 0; i < 10; i++)
                {
                    total += rnd.NextDouble();
                }
                if(total < 0.01)
                {
                    Console.WriteLine(total);
                }
                Thread.Sleep(1);
            }
        }

        private static void threadStart_Traceability(object? obj)
        {
            for (; ; )
            {
                var rnd = new Random();
                for (; ; )
                {
                    var total = 0.0;
                    for (var i = 0; i < 10; i++)
                    {
                        total += rnd.NextDouble();
                    }
                    if (total < 0.01)
                    {
                        Console.WriteLine(total);
                    }
                    Thread.Sleep(1);
                }
            }
        }

        private static Random m_rnd = new();
    }
}