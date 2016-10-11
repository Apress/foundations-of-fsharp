using System;
using System.Collections.Generic;
using System.Text;

using Strangelights;
using Microsoft.FSharp.Core;

namespace TestApp
{
    class Program
    {
        static void UseDemoClass()
        {
            DemoClass c = new DemoClass(3);
            FastFunc<int, int> ff = c.CurriedStyle(4);
            int result = ff.Invoke(5);
            Console.WriteLine("Curried Style Result {0}", result);
            result = c.TupleStyle(4, 5);
            Console.WriteLine("Tuple Style Result {0}", result);
        }
        static void Main(string[] args)
        {
            UseDemoClass();
        }
    }
    class Demoimplementation : IDemoInterface
    {
        public FastFunc<int, int> CurriedStyle(int x)
        {
            Converter<int, int> d =  
                delegate (int y) {return x + y;};
            return FuncConvert.ToFastFunc(d);
        }

        public int TupleStyle(Tuple<int, int> t)
        {
            return t.Item1 + t.Item2;
        }

        public int CSharpStyle(int x, int y)
        {
            return x + y;
        }

        public int CSharpNamedStyle(int x, int y)
        {
            return x + y;
        }
    }
}
