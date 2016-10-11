using System;
using System.Collections.Generic;
using System.Text;

using Strangelights;
using Microsoft.FSharp.Core;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Microsoft.FSharp.Collections.List<int> l = DemoModule.getList();
            foreach (int i in l)
            {
                Console.WriteLine(i);
            }
        }
    }
}