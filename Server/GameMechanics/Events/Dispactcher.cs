using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servidor.GameMechanics.Events
{
    public class Dispatcher
    {
        private readonly BlockingCollection<Tuple<Delegate, object[]>> runQueue = new BlockingCollection<Tuple<Delegate, object[]>>();
        private readonly BlockingCollection<object> resultQueue = new BlockingCollection<object>();
        private readonly CancellationTokenSource source = new CancellationTokenSource();
        private readonly Task task;
        Dispatcher(String serverName)
        {
            Task.Run(() =>
            {
                using (source)
                using (runQueue)
                using (resultQueue)
                {
                    Console.WriteLine(serverName + " iniciado con {0} hilos", Thread.CurrentThread.ManagedThreadId);
                    while (!source.IsCancellationRequested)
                    {
                        var run = runQueue.Take(source.Token);
                        resultQueue.Add(run.Item1.DynamicInvoke(run.Item2));
                    }
                    Console.WriteLine(serverName + " finalizado.");
                }
            });
        }

        public void Stop()
        {
            source.Cancel();
        }
        public object Invoke(Delegate @delegate, params object[] @params)
        {
            runQueue.Add(new Tuple<Delegate, object[]>(@delegate, @params));
            return resultQueue.Take(source.Token);
        }
    }

}
