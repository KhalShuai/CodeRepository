using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Reflection.Emit;
using Microsoft.SqlServer.Server;


namespace Dynamic.UnitTest
{
    [TestFixture]
    public class DynamicMethodTester
    {
        /// <summary>
        /// 创建个动态方法，输出Hello World
        /// </summary>
        public static void UnitTest1()
        {
            AssemblyName aName = new AssemblyName("DynamicAssemblyExample");
            AssemblyBuilder ab = AppDomain.CurrentDomain.DefineDynamicAssembly(aName, AssemblyBuilderAccess.Run);
            ModuleBuilder mb = ab.DefineDynamicModule(aName.Name, aName.Name + ".dll");
            TypeBuilder tb = mb.DefineType("MyDynamicType", TypeAttributes.Public);

        }

        /// <summary>
        /// 输出Hello World
        /// </summary>
        [Test]
        public void UnitTest_DynamicMethod1()
        {
            DynamicMethod Hello = new DynamicMethod("Hello", null, null);

            var il = Hello.GetILGenerator();
            il.Emit(OpCodes.Ldstr, "Hello World");
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
            il.Emit(OpCodes.Ret);

            Hello.Invoke(null, BindingFlags.ExactBinding, null, null, null);
            // or
            ((Action)Hello.CreateDelegate(typeof(Action)))();

            ShowMethodInfo(Hello);
        }


        /// <summary>
        /// 接受string参数，输出字符
        /// </summary>
        [Test]
        public void UnitTest_DynamicMethod2()
        {
            //DynamicMethod Hello = new DynamicMethod("Hello", null, new Type[] { typeof(string) });
            //DynamicMethod Hello = new DynamicMethod("Hello", null, new Type[] { typeof(string) }, typeof(EmitUnitTest).Module);
            DynamicMethod Hello = new DynamicMethod("Hello", null, new Type[] { typeof(string) }, typeof(string).Module);

            var il = Hello.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
            il.Emit(OpCodes.Ret);

            Hello.Invoke(null, BindingFlags.ExactBinding, null, new object[] { "Hello DynamicMethod1" }, null);
            // or
            ((Action<string>)Hello.CreateDelegate(typeof(Action<string>)))("Hello DynamicMethod2");

            ShowMethodInfo(Hello);
        }

        public void UnitTest_DynamicMethod3_Example(int i)
        {
            int i1 = 10;
            Console.WriteLine(string.Format("{0}+{1}:{2}", i1, i, i1 + i));
            Console.WriteLine(string.Format("{0}-{1}:{2}", i1, i, i1 - i));
            Console.WriteLine(string.Format("{0}*{1}:{2}", i1, i, i1 * i));
            Console.WriteLine(string.Format("{0}/{1}:{2}", i1, i, i1 / i));
        }

        [Test]
        public void UnitTest_DynamicMethod3()
        {
            DynamicMethod Hello = new DynamicMethod("Hello", null, new Type[] { typeof(int) });

            var il = Hello.GetILGenerator(149);

            var WriteLine = typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) });
            //注意调用Format方法的参数重载，string,object,object,object
            var Format = typeof(String).GetMethod("Format", new Type[] { typeof(string), typeof(object), typeof(object), typeof(object) });

            #region Add
            il.DeclareLocal(typeof(Int32));
            il.Emit(OpCodes.Ldc_I4_S, 10);
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Ldstr, "{0}+{1}:{2}");
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Box, typeof(Int32));
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Box, typeof(Int32));
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Add);
            il.Emit(OpCodes.Box, typeof(Int32));
            il.Emit(OpCodes.Call, Format);
            il.Emit(OpCodes.Call, WriteLine);
            #endregion

            #region Sub
            il.Emit(OpCodes.Ldstr, "{0}-{1}:{2}");
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Box, typeof(Int32));
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Box, typeof(Int32));
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Sub);
            il.Emit(OpCodes.Box, typeof(Int32));
            il.Emit(OpCodes.Call, Format);
            il.Emit(OpCodes.Call, WriteLine);
            #endregion

            #region Mul
            il.Emit(OpCodes.Ldstr, "{0}*{1}:{2}");
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Box, typeof(Int32));
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Box, typeof(Int32));
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Mul);
            il.Emit(OpCodes.Box, typeof(Int32));
            il.Emit(OpCodes.Call, Format);
            il.Emit(OpCodes.Call, WriteLine);
            #endregion

            #region Div
            il.Emit(OpCodes.Ldstr, "{0}/{1}:{2}");
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Box, typeof(Int32));
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Box, typeof(Int32));
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Div);
            il.Emit(OpCodes.Box, typeof(Int32));
            il.Emit(OpCodes.Call, Format);
            il.Emit(OpCodes.Call, WriteLine);
            #endregion

            il.Emit(OpCodes.Ret);

            Hello.Invoke(null, BindingFlags.ExactBinding, null, new object[] { 9 }, null);
            // or
            ((Action<int>)Hello.CreateDelegate(typeof(Action<int>)))(9);
        }

        private void ShowMethodInfo(DynamicMethod method)
        {
            Console.WriteLine("Name:" + method.Name);
            Console.WriteLine("IsStatic:" + method.IsStatic);
            Console.WriteLine("ReflectedType:" + method.ReflectedType);
            Console.WriteLine("DeclaringType:" + method.DeclaringType);
            Console.WriteLine("Module Name:" + method.Module.FullyQualifiedName);
            Console.WriteLine("Assembly Name:" + method.Module.Assembly.FullName);
        }
    }
}
