using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Windows;

namespace Standard.Tool.Platform.Core
{
    public static class Assemblies
    {
        private static readonly ReadOnlyCollection<Assembly> _all = null;

        static Assemblies()
        {
            List<Assembly> all = new List<Assembly>();
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                //这里只加载了当前运行所需要的程序集，未必是全部。
                AddAssembly(all, a);
            }

            var dir = AppDomain.CurrentDomain.BaseDirectory;
            if (!string.IsNullOrEmpty(dir))
            {
                var dirInfo = new DirectoryInfo(dir);
                var files = dirInfo.GetFiles("*.dll", SearchOption.TopDirectoryOnly);
                foreach (var file in files)
                {
                    if (file.Name.StartsWith("Standard.Tool", StringComparison.OrdinalIgnoreCase))
                    {
                        AssemblyName name = AssemblyName.GetAssemblyName(file.Name);
                        Assembly a = Assembly.Load(name);
                        if (!all.Any(loaded =>
                            AssemblyName.ReferenceMatchesDefinition(loaded.GetName(), name)))
                        {
                            AddAssembly(all, a);
                        }
                    }
                }
            }

            _all = new ReadOnlyCollection<Assembly>(all);
        }

        private static void AddAssembly(List<Assembly> all, Assembly a)
        {
            if (a.FullName.StartsWith("Standard.Tool", StringComparison.OrdinalIgnoreCase))
            {
                all.Add(a);
            }
        }

        public static IEnumerable<Assembly> GetAssemblies()
        {
            return _all;
        }
    }
}
