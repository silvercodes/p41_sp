using System;
using System.Collections.Generic;
using System.Text;
using PCQ;

namespace _03_producer_concumer_queue.Jobs;

internal class SendEmailJob : IJob
{
    public Random random;
    public required string Email { get; set; }
    public SendEmailJob()
    {
        random = new Random();
    }

    public void Execute()
    {
        Thread.Sleep(random.Next(50, 200));
        Console.WriteLine($"Email to {Email} was sent...");
    }

    public string GetInfo()
    {
        return $"Email = {Email}";
    }
}
