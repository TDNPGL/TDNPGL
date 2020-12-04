using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;

namespace TDNPGL.Tests.Native
{
    public class DNNITests
    {
        public interface IKernel32
        {
            public IntPtr GetConsoleWindow();
        }
        [Test]
        public void Kernel32Test()
        {
            IKernel32 kernel = DNNI.DNNI.Create<IKernel32>("kernel32.dll");
            if (kernel == null)
                Assert.Fail("Instance is null");
            Type type = kernel.GetType();
            MethodInfo[] methods = type.GetMethods();
            for (int i = 0; i < methods.Length; i++)
            {
                Console.WriteLine(i + ". " + methods[i].Name);
            }
            MethodInfo info = type.GetMethods()[0];
            Console.WriteLine(info.Name);
            info.Invoke(kernel, new object[0] { });
            Assert.Pass();
        }
    }
}
