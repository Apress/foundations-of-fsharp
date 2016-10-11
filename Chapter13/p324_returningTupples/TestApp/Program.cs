using System;
using System.Collections.Generic;
using System.Text;

using Strangelights;
using Microsoft.FSharp.Core;

namespace TestApp
{
    class Program
    {
        static void HourMinute()
        {
            Tuple<int, int> t = DemoModule.hourAndMinute(DateTime.Now);
            Console.WriteLine("Hour {0} Minute {1}", t.Item1, t.Item2);
        }

        static void Main(string[] args)
        {
            HourMinute();
        }
    }
}