using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace TDNPGL.DNNI
{
    public static class DNNI
    {
        private static AssemblyBuilder assembly;
        private static ModuleBuilder module;
        private static ConstructorInfo dllImportConstr;
        private static Type dllImport = typeof(DllImportAttribute);
        public static T Create<T>(string file)
        {
            Type type = typeof(T);
            if (!typeof(T).IsInterface)
                throw new TypeInitializationException(type.FullName, new Exception(string.Format("Given type {0} not interface",type.Name)));

            TypeBuilder builder = module.DefineType("DNNIDynamicAssembly.Native" + type.Name,
                              TypeAttributes.Public |
                              TypeAttributes.Class);
            builder.AddInterfaceImplementation(type);

            FieldBuilder fieldBuilder = builder.DefineField("MethodsCount", typeof(int), FieldAttributes.Public);
            fieldBuilder.SetConstant(type.GetMethods().Length);
            
            ConstructorBuilder ciBuilder=builder.DefineConstructor(MethodAttributes.Public, CallingConventions.Any, Type.EmptyTypes);
            ILGenerator ctorILGen = ciBuilder.GetILGenerator();

            ctorILGen.Emit(OpCodes.Ldarg_0);
            ctorILGen.Emit(OpCodes.Call,
                typeof(object).GetConstructor(Type.EmptyTypes));
            ctorILGen.Emit(OpCodes.Ret);

            foreach (MethodInfo info in type.GetMethods())
            {
                Type[] args = info.GetParameters()
                                     .ToList()
                                     .Select((ParameterInfo i) =>
                                        i.ParameterType)
                                     .ToArray();
                MethodBuilder m = builder.DefineMethod(info.Name,
                                     MethodAttributes.Public | MethodAttributes.Virtual
                                     );
                ILGenerator iLGen = m.GetILGenerator();

                iLGen.Emit(OpCodes.Ldarg_0);
                iLGen.EmitCall(OpCodes.Call, typeof(Console).GetMethod("WriteLine", Type.EmptyTypes), null);
                iLGen.Emit(OpCodes.Ret);

                builder.DefineMethodOverride(m, info);

                m.SetParameters(args);
                m.SetReturnType(info.ReturnType);
            }
            Console.WriteLine(builder.Name);
            Type type1 = builder.CreateTypeInfo().BaseType;
            if (!builder.IsCreated())
                throw new TypeInitializationException(builder.FullName,new Exception("Type can't be created!"));
               Console.WriteLine(type1.Name);
            T item = (T)Activator.CreateInstance(type1);

            return item;
        }
        static DNNI()
        {
            ReloadAssemblyBuilder();
        }
        public static void ReloadAssemblyBuilder()
        {
            AssemblyName name = new AssemblyName("DNNIDynamicAssembly");
            AssemblyBuilderAccess access = AssemblyBuilderAccess.Run;
            assembly = AssemblyBuilder.DefineDynamicAssembly(name, access);
            module = assembly.DefineDynamicModule("DNNIDynamicModule");
            dllImportConstr = dllImport.GetConstructors()[0];
        }
    }
}
