
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



string email = "vasia@mail.com";
//Thread t = new Thread(new ThreadStart(() => Console.WriteLine(email)));
// >>> EQUALS <<<
Thread t = new Thread(() => Console.WriteLine(email));
t.Start();


#endregion


