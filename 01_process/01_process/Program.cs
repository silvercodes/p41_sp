
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

//Process[] existsProcesses = Process.GetProcesses();

//var processes = existsProcesses.OrderBy(p => p.Id);

//foreach(Process process in processes)
//    Console.WriteLine($"pid: {process.Id} {process.ProcessName}");



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
    }
}




#endregion



