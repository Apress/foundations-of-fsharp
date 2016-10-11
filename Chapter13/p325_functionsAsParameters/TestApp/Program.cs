using System;
using System.Collections.Generic;
using System.Text;

using Strangelights;
using Microsoft.FSharp.Core;

namespace TestApp
{
    class Program
    {
        static void MapOne()
        {
            List<string> l = new List<string>(
                new string[] { "Stefany", "Oussama", 
            "Sebastien", "Frederik" });
            Converter<string, bool> pred =
                delegate(string s) { return s.StartsWith("S"); };
            FastFunc<string, bool> ff =
                FuncConvert.ToFastFunc<string, bool>(pred);
            IEnumerable<string> ie =
                 DemoModule.filterStringList(ff, l);
            foreach (string s in ie)
            {
                Console.WriteLine(s);
            }
        }
        static void MapTwo()
        {
            List<string> l = new List<string>(
                new string[] { "Aurelie", "Fabrice", 
            "Ibrahima", "Lionel" });
            List<string> ie =
                DemoModule.filterStringListDelegate(
                    delegate(string s) { return s.StartsWith("A"); }, l);
            foreach (string s in ie)
            {
                Console.WriteLine(s);
            }
        }
        static void Main(string[] args)
        {
            MapOne();
            MapTwo();
        }
    }
}