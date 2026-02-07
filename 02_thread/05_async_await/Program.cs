// async await

// 1. async

// 2. return
//      Task
//      Task<T>
//      void        :-(((
//      ValueTask  
//      ValueTask<T>
//      IAsyncEnumerable<T>
//      IAsyncEnumerator<T>
//  ...

// 3. await ........

// 4. ...Async



//async Task MethodAsync()
//{
//    Console.WriteLine("Start");

//    Task t = new Task(() => Thread.Sleep(1000));
//    t.Start();

//    Console.WriteLine("ONE");

//    // t.Wait();
//    await t;

//    Console.WriteLine("End");
//}

//_ = MethodAsync();

//Console.WriteLine("Main");

//Console.ReadLine();




// --- cancel

//async Task DownloadAsync(string url, CancellationToken token)
//{
//    //
//    //
//    HttpClient client = new HttpClient();

//    //Task t = client.GetStringAsync(url, token);
//    //t.Wait();

//    string content = await client.GetStringAsync(url, token);
//    //
//    //
//    Console.WriteLine(content);
//}

//using var cts = new CancellationTokenSource();

//_ = DownloadAsync("https://habr.com/ru/articles/", cts.Token);

//// cts.Cancel();

////
////
//Console.ReadLine();




#region EXAMPLE_1

//async Task<string> FetchDataAsync(string url)
//{
//    using var httpClient = new HttpClient();

//	try
//	{
//		Task<string> responseTask = httpClient.GetStringAsync(url);

//		// return responseTask.Result;		// BLOCKING

//		// responseTask.Wait();				// BLOCKING
//		// return responseTask.Result;

//		return await responseTask;
//	}
//	catch (Exception ex)
//	{
//        Console.WriteLine($"ERROR: {ex.Message}");
//		return string.Empty;
//	}
//}

//string content = await FetchDataAsync("https://habr.com/ru/articles/");
//Console.WriteLine(content);

#endregion



#region EXAMPLE_2
async Task<string> FetchDataAsync(string url, CancellationToken token)
{
    using var httpClient = new HttpClient();

    try
    {
        Task<string> responseTask = httpClient.GetStringAsync(url, token);

        return await responseTask;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"ERROR: {ex.Message}");
        return string.Empty;
    }
}

async Task<IDictionary<string, string>> FetchMultipleDataAsync(IEnumerable<string> ulrs, CancellationToken token)
{
    var tasks = new Dictionary<string, Task<string>>();

    foreach (string url in ulrs)
        tasks.Add(url, FetchDataAsync(url, token));

    // Task.WaitAll(tasks.Values);			// BLOCKING

    await Task.WhenAll(tasks.Values);           // <---------

    return tasks.ToDictionary(
        pair => pair.Key,
        pair => pair.Value.Result
    );
}


using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

try
{
    IDictionary<string, string> results = await FetchMultipleDataAsync(new[]
    {
        "https://google.com",
        "https://habr.com/ru/articles/"
    }, cts.Token);

    foreach (KeyValuePair<string, string> item in results)
    {
        Console.WriteLine($"{item.Key}: {item.Value.Length}");

        string domain = new Uri(item.Key).DnsSafeHost;

        using FileStream fs = File.OpenWrite($"{domain}.html");
        using StreamWriter sw = new StreamWriter(fs);

        await sw.WriteAsync(item.Value);
    }
}
catch (TaskCanceledException ex)
{
    Console.WriteLine("Операция прервана по таймауту");
}


#endregion









