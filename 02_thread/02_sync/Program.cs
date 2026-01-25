
// Инструменты синхронизации

// 1. Простые методы блокировки (Thread.Sleep(), Thread.Join(), Task.Wait()....)

// 2. Контроль критических секций (lock, Monitor(20нс), Mutex(1000нс), SpinLock, Semaphore(1000нс), SemaphoreSlim.....)

// 3. Инструменты сигнализации (Monitor.Pulse(), Monitor.PulseAll(), .Wait(), AutoResetEvent, ManualResetEvent, CountdownResetEvent....)

// 4. Неблокирующие инструменты (Thread.MemoryBarrier, Interlocked, Thread.VolitileRead....)


#region lock / Monitor (эксклюзивная блокировка)

// Разблокировка
// 1. Выполнение условий блокировки
// 2. Таймаут
// 3. Thread.Interrupt()
// 4. Thread.Abort()


//new Thread(ThreadUnsafe.Run).Start();
//ThreadUnsafe.Run();

//class ThreadUnsafe
//{
//    static int a = 10;
//    static int b = 20;

//    public static void Run()
//    {
//        int c = 0;

//        if (b!= 0)
//        {
//            c = a / b;
//        }

//        b = 0;
//    }
//}


//class ThreadSafe
//{
//    static int a = 10;
//    static int b = 20;
//    static object locker = new object();

//    public static void Run()
//    {
//        int c = 0;

//        // FIFO
//        lock(locker)
//        {
//            if (b != 0)
//            {
//                c = a / b;
//            }

//            b = 0;
//        }
//    }
//}




//class ThreadSafe
//{
//    static int a = 10;
//    static int b = 20;
//    static object locker = new object();

//    public static void Run()
//    {
//        int c = 0;
//        bool flag = false;

//        try
//        {
//            // FIFO
//            Monitor.Enter(locker, ref flag);    // Попытка взятия блокировки

//            if (b != 0)
//            {
//                c = a / b;
//            }

//            b = 0;
//        }
//        catch(Exception ex)
//        {
//            Console.WriteLine($"ERROR: {ex.Message}");
//        }
//        finally
//        {
//            if (flag)
//                Monitor.Exit(locker);           // Освобождение блокировки
//        }
//    }
//}



//object locker = new object();
//int val = 0;
//void Run()
//{
//    bool flag = false;

//    try
//    {
//        flag = Monitor.TryEnter(locker, 1200);
//        if (flag)
//        {
//            for (int i = 0; i < 10; ++i)
//            {
//                Console.WriteLine($"{Thread.CurrentThread.Name}: {val++}");
//                Thread.Sleep(100);
//            }
//        }

//        Console.WriteLine($"{Thread.CurrentThread.Name} is looser");

//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"ERROR: {ex.Message}");
//    }
//    finally
//    {
//        if (flag)
//            Monitor.Exit(locker);           // Освобождение блокировки
//    }
//}

//for (int i = 0; i < 3; ++i)
//{
//    Thread t = new Thread(Run)
//    {
//        Name = $"Thread_{i}"
//    };
//    t.Start();
//}

#endregion


#region Mutex

//int count = 0;
//Mutex mutex = new Mutex();

//void UseResource()
//{
//    if (mutex.WaitOne(500))         // Попытка взятия блокировки
//    {
//        Console.WriteLine($"{Thread.CurrentThread.Name} takes the mutex");

//        Thread.Sleep(200);
//        count++;

//        Console.WriteLine($"{Thread.CurrentThread.Name} DONE");
//        Console.WriteLine($"{Thread.CurrentThread.Name} release mutex");

//        mutex.ReleaseMutex();       // Освобождение мьютекса
//    }
//    else
//    {
//        Console.WriteLine($"{Thread.CurrentThread.Name} is looser");
//    }

//}

//void StartThreads()
//{
//    for (int i = 0; i < 5; ++i)
//    {
//        Thread t = new Thread(UseResource)
//        {
//            Name = $"THREAD_{i}"
//        };
//        t.Start();
//    }
//}
//StartThreads();

#endregion


#region Semaphore  (НЕ эксклюзивная блокировка)

//Semaphore semaphore = new Semaphore(0, 3);
//object locker = new object();
//int executionTime = 0;

//void Run(int id)
//{
//    Console.WriteLine($"THREAD_{id} started");

//    if (semaphore.WaitOne(2500))                        // Попытка взять блокировку
//    {
//        Console.WriteLine($"THREAD_{id} passed semaphore");

//        int time;

//        lock (locker)
//        {
//            executionTime += 200;
//            time = executionTime;
//        }

//        Thread.Sleep(time + 2000);

//        Console.WriteLine($"THREAD_{id} relesed semaphore");
//        semaphore.Release();                        // Освободить 1 место
//    }
//    else
//    {
//        Console.WriteLine($"THREAD_{id} is looser");
//    }


//}

//for (int i = 1; i <= 5; ++i)
//{
//    int x = i;
//    Thread t = new Thread(() => Run(x));
//    t.Start();
//}

//Thread.Sleep(1000);
//semaphore.Release(3);

#endregion


#region Signaling

//object locker = new object();

//void First()
//{
//	try
//	{
//		Monitor.Enter(locker);

//		for (int i = 1; i <= 10; i += 2)
//		{
//			Thread.Sleep(200);
//			Console.Write($"{i} ");
//			Monitor.Pulse(locker);		// Перевод locker в сигнальное состояние
//            if (i < 10)
//                Monitor.Wait(locker);		// Ожидание следующего сигнального состояния
//		}
//	}
//	finally
//	{
//		Monitor.Exit(locker);
//	}
//}

//void Second()
//{
//    try
//    {
//        Monitor.Enter(locker);

//        for (int i = 0; i <= 10; i += 2)
//        {
//            Thread.Sleep(200);
//            Console.Write($"{i} ");
//            Monitor.Pulse(locker);      // Перевод locker в сигнальное состояние
//            if (i < 10)
//                Monitor.Wait(locker);       // Ожидание следующего сигнального состояния
//        }
//    }
//    finally
//    {
//        Monitor.Exit(locker);
//    }
//}

//Thread t1 = new Thread(First);
//Thread t2 = new Thread(Second);

//t2.Start();
//Thread.Sleep(3000);
//t1.Start();






//SimpleWaitHandle.Run();
//static class SimpleWaitHandle
//{
//    static EventWaitHandle wh = new AutoResetEvent(false);

//    public static void Run()
//    {
//        new Thread(Work).Start();
//        Thread.Sleep(3000);
//        wh.Set();                       // Перевод в сигнальное состояние (с автоматическим закрытием)

//    }

//    public static void Work()
//    {
//        Console.WriteLine("Work()");
//        wh.WaitOne();                   // Ожидание сигнального состояния
//        Console.WriteLine("Working.....");
//    }
//}





//AutoResetEvent are = new AutoResetEvent(false);


//for (int i = 0; i < 5; ++i)
//{
//    Thread t = new Thread(Render)
//    {
//        Name = $"THREAD_{i}",
//    };
//    t.Start();
//}
//Thread.Sleep(3000);
//are.Set();

//void Render()
//{
//    are.WaitOne();
//    for (int i = 0; i < 10; i++)
//    {
//        Console.WriteLine($"{Thread.CurrentThread.Name}: {i}");
//        Thread.Sleep(200);
//    }
//    are.Set();
//}






//ManualResetEvent mre = new ManualResetEvent(false);

//UserThread ut1 = new UserThread("first", mre);
//Console.WriteLine("Waiting");
//mre.WaitOne();
//Console.WriteLine("first DONE");
//mre.Reset();                    // Перевод в несигнальное состояние

//UserThread ut2 = new UserThread("second", mre);
//mre.WaitOne();
//Console.WriteLine("second DONE");
//mre.Reset();

//class UserThread
//{
//    private ManualResetEvent mre;
//    public Thread Thread { get; set; }

//    public UserThread(string name, ManualResetEvent mre)
//    {
//        this.mre = mre;
//        Thread = new Thread(Run)
//        {
//            Name = name,
//        };
//        Thread.Start();
//    }
//    private void Run()
//    {
//        Console.WriteLine($"{Thread.Name} started...");

//        for (int i = 0; i < 5; ++i)
//        {
//            Console.WriteLine($"{Thread.Name}: {i}");
//            Thread.Sleep(200);
//        }
//        mre.Set();
//    }
//}



#endregion


#region Interlocked

Semaphore semaphore = new Semaphore(0, 3);
int executionTime = 0;

void Run(int id)
{
    Console.WriteLine($"Thread {id} started...");

    if (semaphore.WaitOne(2000))                        // Попытка взять блокировку
    {
        Console.WriteLine($"THREAD_{id} passed semaphore");

        //int time;

        //lock (locker)
        //{
        //    executionTime += 200;
        //    time = executionTime;
        //}

        int time = Interlocked.Add(ref executionTime, 200);

        Thread.Sleep(time + 2000);

        Console.WriteLine($"THREAD_{id} relesed semaphore");
        semaphore.Release();                        // Освободить 1 место
    }
    else
    {
        Console.WriteLine($"THREAD_{id} is looser");
    }
}

for (int i = 1; i <= 5; ++i)
{
    int x = i;
    Thread t = new Thread(() => Run(x));
    t.Start();
}

Thread.Sleep(1000);
semaphore.Release(3);

#endregion





