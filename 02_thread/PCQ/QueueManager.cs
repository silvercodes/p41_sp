using System;
using System.Collections.Generic;
using System.Text;

namespace PCQ;

public class QueueManager
{
    private Queue<IJob> jobs = new Queue<IJob>();
    private int workersCount;
    private List<Thread> threads = new List<Thread>();

    public QueueManager(int workersCount)
    {
        this.workersCount = workersCount;
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < workersCount; i++)
        {
            Thread t = new Thread(Handle)
            {
                Name = $"WORKER_{i}"
            };

            threads.Add(t);
            t.Start();
        }
    }

    public void EnqueueJob(IJob job)
    {
        lock (jobs)
            jobs.Enqueue(job);
    }

    private void Handle()
    {
        while(true)
        {

        }
    }



}
