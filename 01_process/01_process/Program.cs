
// 1. Процесc (Process)

// 2. Поток (Thread)

// 3. Адрессное пространство (Memory scope)

// 4. Приложение (Application)

// 5. Сборка (Assembly)

// 6. Модуль (Module)

// 7. Системные ресурсы



#region Process

// 1. Memory scope
// 2. Исполняемый код
// 3. Системные дискрипторы
// 4. Контекст безопасности
// 5. Идентификатор PID
// 6. Переменные окружения
// 7. Приоритет
// 8. Как минимум одним потоком


using System.Diagnostics;

void Run()
{
    string? input;
    while (true)
    {
        Console.WriteLine("1. Show all processes");
        Console.WriteLine("2. Get process by PID");
        Console.WriteLine("3. Show threads");
        Console.WriteLine("4. Show modules");
        Console.WriteLine("5. Start process");
        Console.WriteLine("6. Kill process");

        input = Console.ReadLine();

        switch(input)
        {
            case "1":
                ShowAllProcesses();
                break;
            case "2":
                GetProcessById();
                break;
            case "3":
                ShowThreads();
                break;
            case "4":
                ShowModules();
                break;
            case "5":
                StartProcess();
                break;
            case "6":
                KillProcess();
                break;
        }

    }
}

Run();

void ShowAllProcesses()
{
    Process[] existsProcesses = Process.GetProcesses();

    var processes = existsProcesses.OrderBy(p => p.Id);

    foreach (Process process in processes)
        Console.WriteLine($"pid: {process.Id} {process.ProcessName}");
}

void GetProcessById()
{
    try
    {
        Process p = GetProcessFromInput();

        Console.WriteLine($"{p.Id}\t{p.ProcessName}\t{p.BasePriority}\t{p.StartTime}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"ERROR: {ex.Message}");
    }

}

void ShowThreads()
{
    try
    {
        Process p = GetProcessFromInput();

        var threads = p.Threads;
        Console.WriteLine("Threads list:");
        foreach(ProcessThread t in threads)
            Console.WriteLine($"{t.Id}\t{t.StartTime.ToShortTimeString()}\t{t.PriorityLevel}");

    }
    catch (Exception ex)
    {
        Console.WriteLine($"ERROR: {ex.Message}");
    }
}

void ShowModules()
{
    try
    {
        Process p = GetProcessFromInput();

        ProcessModuleCollection modules = p.Modules;
        foreach(ProcessModule m in modules)
            Console.WriteLine($"{m.ModuleName}\t{m.ModuleMemorySize}");

    }
    catch (Exception ex)
    {
        Console.WriteLine($"ERROR: {ex.Message}");
    }
}

void StartProcess()
{
    // Process.Start("notepad");
    // Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome.exe", "https://wikipedia.org --incognito");
    Process.Start(@"C:\Users\ThinkPad\Desktop\cpp_pro.exe");
}

void KillProcess()
{
    try
    {
        Process p = GetProcessFromInput();

        p.Kill();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"ERROR: {ex.Message}");
    }
}

Process GetProcessFromInput()
{
    Console.Write("Enter PID: ");
    string? input = Console.ReadLine();

    int pid = int.Parse(input);

    return Process.GetProcessById(pid);
}





#endregion



