using ScriptExecutor.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace ScriptExecutor
{
    class Program
    {
        private const string ScriptsDirectory = @"";

        static void Main(string[] args)
        {
            var compiledAssemblyPath = Path.Combine(Environment.CurrentDirectory, ScriptsDirectory);
            var sourceFiles = Directory.EnumerateFiles(Environment.CurrentDirectory, "*.source", SearchOption.AllDirectories).ToArray();
            var assembliesFiles = Directory.EnumerateFiles(Environment.CurrentDirectory, "*.assemblies", SearchOption.AllDirectories).ToArray();
            var scriptAssembly = ScriptHelper.CompileAssembly(sourceFiles,assembliesFiles);
            var scriptTypes = ScriptHelper.GetTypesImplementingInterface(scriptAssembly, typeof(IScript));
            foreach (var scriptType in scriptTypes)
            {
                 var script = (IScript)Activator.CreateInstance(scriptType);
               Console.WriteLine( script.Run());
            }

            Console.ReadKey(true);
        }
    }
}
