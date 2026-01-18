
#region Intro

//void RenderPlus()
//{
//    for (int i = 0; i < 1000; ++i)
//        Console.Write('+');
//}

//// Thread t = new Thread(new ThreadStart(RenderPlus));
//// >>> EQUALS <<<
//Thread t = new Thread(RenderPlus);

//t.Start();

//Console.WriteLine(t.IsAlive);

//for (int i = 0; i < 1000; ++i)
//    Console.Write('0');




//void Run()
//{
//    for (int i = 0; i < 5; ++i)
//        Console.WriteLine('0');
//}

//new Thread(Run).Start();
//Run();




//// НЕ потокобезопасно !!!
//bool done = false;

//void Run()
//{
//    if (! done)
//    {
//        Console.WriteLine('*');
//        done = true;
//        Console.WriteLine("DONE");
//    }
//}

//new Thread(Run).Start();
//Run();


// потокобезопасно
//bool done = false;
//object locker = new object();

//void Run()
//{
//    lock(locker)
//    {
//        if (!done)
//        {
//            Console.WriteLine('*');
//            done = true;
//            Console.WriteLine("DONE");
//        }
//    }
//}

//new Thread(Run).Start();
//Run();




//void Run()
//{
//    for (int i = 0; i < 1000; ++i)
//        Console.Write('*');
//    Console.WriteLine();
//}

//Console.WriteLine("Main started");
//Thread t = new Thread(Run);
//t.Start();
//// Thread.Sleep(1);
//t.Join();                       // Блокируем поток вызова и ждём завершения потока t
//Console.WriteLine("Main end");



#endregion


#region Create / Start

//void Run()
//{
//    Console.WriteLine("hello");
//}

//Thread t = new Thread(new ThreadStart(Run));
//t.Start();
//Run();



//Thread t = new Thread(() => Console.WriteLine("Hello Vasia"));
//t.Start();



//string email = "vasia@mail.com";
////Thread t = new Thread(new ThreadStart(() => Console.WriteLine(email)));
//// >>> EQUALS <<<
//Thread t = new Thread(() => Console.WriteLine(email));
//t.Start();



//void Calc(int a, int b)
//{
//    Console.WriteLine($"RESULT: {a + b}");
//}

//int a = 3;
//int b = 4;

// Способ 1
//void RunCalc(object? obj)
//{
//    if (obj is Parameters p)
//    {
//        Calc(p.A, p.B);
//    }
//}

//Thread t = new Thread(RunCalc);
//t.Start(new Parameters { A = a, B = b });

//class Parameters
//{
//    public int A { get; set; }
//    public int B { get; set; }
//}


// Способ 2
// Thread t = new Thread(() => Calc(a, b));






//void Render(string message, ConsoleColor color)
//{
//    Console.ForegroundColor = color;
//    Console.WriteLine(message);
//    Console.ResetColor();
//}

//string message = "Chack Norris";
//ConsoleColor color = ConsoleColor.Red;

//Thread t = new Thread(() => Render(message, color));
//t.Start();





// :-(((
//for (int i = 0; i < 10; ++i)
//    new Thread(() => Console.WriteLine(i)).Start();

// :-)))
//for (int i = 0; i < 10; ++i)
//{
//    int n = i;
//    new Thread(() => Console.WriteLine(n)).Start();
//}





//int i;

//List<Thread> threads = new List<Thread>();

//for (i = 0; i < 10; i++)
//{
//    threads.Add(new Thread(() => Console.WriteLine(i)));
//}

//threads.ForEach(t => t.Start());





//void Run()
//{
//    Console.WriteLine($"Message from {Thread.CurrentThread.Name}");
//}

//Thread.CurrentThread.Name = "MAIN";

//Thread t = new Thread(Run)
//{
//    Name = "WORKER"
//};

//t.Start();
//Run();





//Thread t = new Thread(() => Console.WriteLine("from thread"));

//if (args.Length > 0)
//    t.IsBackground = true;

//t.Start();


#endregion


#region try / catch

// :-(((
//void Run()
//{
//    throw new Exception("Test exception");
//}

//try
//{
//    new Thread(Run).Start();
//}
//catch (Exception ex)
//{
//    Console.WriteLine($"ERROR: {ex.Message}");
//}


// :-)))
//void Run()
//{
//	try
//	{
//		throw new Exception("Test exception");
//	}
//	catch (Exception ex)
//	{
//        Console.WriteLine($"ERROR: {ex.Message}");
//    }
//}

//new Thread(Run).Start();



#endregion


#region TPL (Task Parallel Library), Thread pool

// Task     Task<T>     ValueTask     ValueTask<T>      Parallel ........

//void Run()
//{
//    Console.WriteLine("Vasia");
//}

////Task t = Task.Factory.StartNew(() => Run());
//////
//////
//////
////t.Wait();       // BLOCKING!!!

//await Task.Factory.StartNew(() => Run());






// using System.Net;

//string DownlaodPageSrc(string url)
//{
//    WebClient client = new WebClient();

//    return client.DownloadString(url);
//}

//// Console.WriteLine(DownlaodPageSrc(@"https://habr.com/ru/articles/"));

//string url = @"https://habr.com/ru/articles/";

//Task<string> t = Task.Factory.StartNew(() => DownlaodPageSrc(url));
////
////
//Console.WriteLine("TEST");
////
////
//string content = t.Result;          // BLOCKING!!!
//Console.WriteLine(content);





// ThreadPool.SetMinThreads(100, 10);

ThreadPool.GetMinThreads(out int count, out int completion);
Console.WriteLine($"{count} {completion}");

#endregion