using System;
using System.Collections.Generic;
using System.Text;

using Strangelights;
using Microsoft.FSharp.Core;

namespace TestApp
{
    class Program
    {
        static void UseTheClass()
        {
            List<TheClass> l = new List<TheClass>();
            l.Add(new TheClass(5));
            l.Add(new TheClass(6));
            l.Add(new TheClass(7));
            TheModule.incList(l);
            foreach (TheClass c in l)
            {
                Console.WriteLine(c.TheField);
            }
        }
        static void Main(string[] args)
        {
            UseTheClass();
        }
    }
}