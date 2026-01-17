


#region Статическая загрузка сборок (модудей)

//using System.Reflection;
//using MathLib;


//AppDomain domain = AppDomain.CurrentDomain;
//Console.WriteLine($"{domain.FriendlyName}\t{domain.BaseDirectory}");

//foreach(Assembly a in domain.GetAssemblies())
//    Console.WriteLine($"{a.GetName().Name}\t{a.GetName().Version}");

//Calculator calc = new Calculator();
//Console.WriteLine(calc.Sum(3, 5));


#endregion

#region Динамическая подгрузка сборки

using System.Reflection;
using System.Runtime.Loader;

Console.WriteLine("==== BEFORE LOADING =======");
ShowAssemblies();

AssemblyLoadContext ctx = new AssemblyLoadContext("lib_ctx", true);
ctx.Unloading += c => Console.WriteLine(">>>>>>ASSEMBLY_CONTEXT UNLOADED<<<<<<<");

Assembly assembly = ctx.LoadFromAssemblyPath(Path.Combine(Directory.GetCurrentDirectory(), "MathLib.dll"));

Console.WriteLine("==== AFTER LOADING =======");
ShowAssemblies();

Type? type = assembly.GetType("MathLib.Calculator");

// === static call
//MethodInfo? method = type?.GetMethod("Factorial");
//int? result = (int?)method?.Invoke(assembly, new object[] { 5 });
//Console.WriteLine($"Factorial = {result}");

// === non static call
MethodInfo? method = type?.GetMethod("Sum");
object? obj = Activator.CreateInstance(type);
int? result = (int?)method.Invoke(obj, new object[] { 4, 5 });
Console.WriteLine($"Sum = {result}");

ctx.Unload();
GC.Collect();

Console.WriteLine("==== FTER LOADING =======");
ShowAssemblies();

void ShowAssemblies()
{
    AppDomain domain = AppDomain.CurrentDomain;
    
    foreach (Assembly a in domain.GetAssemblies())
        Console.WriteLine($"{a.GetName().Name}\t{a.GetName().Version}");
}
#endregion
