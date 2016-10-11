using System;
using System.Collections.Generic;
using System.Text;

using Strangelights;
using Microsoft.FSharp.Core;

namespace TestApp
{
    class Program
    {
        static void GetQuantityZero()
        {
            DemoModule.Quantity d = DemoModule.Quantity.Discrete(12);
            DemoModule.Quantity c = DemoModule.Quantity.Continuous(12.0);
        }
        static void GetQuantityOne()
        {
            DemoModule.Quantity q = DemoModule.getRandomQuantity();
            switch (q.Tag)
            {
                case DemoModule.Quantity.tag_Discrete:
                    Console.WriteLine("Discrete value: {0}", q.Discrete1);
                    break;
                case DemoModule.Quantity.tag_Continuous:
                    Console.WriteLine("Continuous value: {0}", q.Continuous1);
                    break;
            }

        }
        static void GetQuantityTwo()
        {
            DemoModule.Quantity q = DemoModule.getRandomQuantity();
            if (q.IsDiscrete())
            {
                Console.WriteLine("Discrete value: {0}", q.Discrete1);
            }
            else if (q.IsContinuous())
            {
                Console.WriteLine("Continuous value: {0}", q.Continuous1);
            }
        }
        static void GetQuantityThree()
        {
            DemoModule.EasyQuantity q = DemoModule.getRandomEasyQuantity();
            Console.WriteLine("Value as a float: {0}", q.ToFloat());
        }
        static void Main(string[] args)
        {
            GetQuantityZero();
            GetQuantityOne();
            GetQuantityTwo();
            GetQuantityThree();
        }
    }
}
