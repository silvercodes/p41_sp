

using _03_producer_concumer_queue.Jobs;
using PCQ;

QueueManager qm = new QueueManager(100);

for (int i = 0; i < 100000; ++i)
{
    qm.EnqueueJob(new SendEmailJob() { Email = $"user_{i}@mail.com" });
}

for (int i = 0; i < 200; ++i)
{
    Thread.Sleep(100);
    Console.WriteLine($"Main: {i}");
}