using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        var tasks = new List<Task<int>>();
        var source = new CancellationTokenSource();
        var token = source.Token;
        int completedInterations = 0;

        for (int n = 1; n <= 20; n++)
        {
            tasks.Add(Task.Run(() =>
            {
                int itegrations = 0;
                for (int ctr = 1; ctr <= 2_000_000; ctr++)
                {
                    token.ThrowIfCancellationRequested();
                    itegrations++;
                }
                Interlocked.Increment(ref completedInterations);
                if (completedInterations >= 10)
                {
                    source.Cancel();
                }
                return itegrations;
            }, token));
        }
        Console.WriteLine("Waiting for the first 10 tasks to complete .... \n");
        try
        {
            Task.WaitAll(tasks.ToArray());
        }
        catch (AggregateException)
        {
            Console.WriteLine("Status of tasks: \n");
            Console.WriteLine("{0,10} {1,20} {2,14:N0}", "Task Id", "Status", "Iterations");
            foreach (var t in tasks)
            {
                Console.WriteLine("{0,10} {1,20} {2,14}",
                    t.Id, t.Status, 
                    t.Status != TaskStatus.Canceled ? t.Result.ToString("N0") : "n/a");
            }
            Console.ReadLine();
        }
    }

}