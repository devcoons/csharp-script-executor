using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.Reflection;
using Microsoft.CSharp;

namespace ScriptExecutor
{
    public static class ScriptHelper
    {
        public static Assembly CompileAssembly(string[] sourceFiles,string[] assembliesFiles)
        {
            var codeProvider = new CSharpCodeProvider();
  
            var compilerParameters = new CompilerParameters
            {
                GenerateExecutable = false,
                GenerateInMemory = true,
                IncludeDebugInformation = false,
            };

            compilerParameters.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);

            string[] assemblies;

            foreach (string assemblyFile in assembliesFiles)
            {

                assemblies =  File.ReadAllLines(assembliesFiles[0]);
                foreach(string assembly in assemblies)
                {
                    if(assembly.Trim()!="")
                    compilerParameters.ReferencedAssemblies.Add(assembly.Trim());
                }
            }
            
            
            var result = codeProvider.CompileAssemblyFromFile(compilerParameters, sourceFiles);

            if (result.Errors.HasErrors) throw new Exception("Assembly compilation failed.");

            return result.CompiledAssembly;
        }

        public static List<Type> GetTypesImplementingInterface(Assembly assembly, Type interfaceType)
        {
            if (!interfaceType.IsInterface) throw new ArgumentException("Not an interface.", "interfaceType");

            return assembly.GetTypes()
                           .Where(t => interfaceType.IsAssignableFrom(t))
                           .ToList();
        }

    }
}
