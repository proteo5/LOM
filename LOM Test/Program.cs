using LOM;
using System;
using System.Collections.Generic;

namespace Lomvm_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            LOMList<int> list = new LOMList<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            print($"Print List 1", list);

            LOMList<int> list2 = list.Clone();

            list2.Add(10);
            list2.Add(11);
            list2.Add(12);

            print($"Print List 1", list);
            print($"Print List 2", list2);

            list[2] = 22;
            list[9] = 99;
            list2[4] = 444;
            list2[7] = 777;
            list.Insert(9, 9999);
            list2.Insert(8, 8888);
            list.RemoveAt(10);
            list2.RemoveAt(9);
            list.Remove(3);
            list2.Remove(12);
            print($"Print List 1", list);
            print($"Print List 2", list2);

            Console.WriteLine("List 1 Contains 0: {0}",list.Contains(0) ? "Yes":"No");
            Console.WriteLine("List 2 Contains 100: {0}", list.Contains(100) ? "Yes" : "No");

            Console.WriteLine("List 1 Index of 0: {0}", list.IndexOf(0));
            Console.WriteLine("List 2 index of 100: {0}", list.IndexOf(100));

            Console.WriteLine("");
            Console.WriteLine("Print Sublist");
            int[] sublist = new int[list.Count+10];
            list.CopyTo(sublist);
            list.CopyTo(sublist, 11);

            for (int i = 0; i < sublist.Length; i++)
            {
                Console.WriteLine("{0}", sublist[i]);
            }


            //list.Safe<int>((listIn) => {
            //    listIn.Remove(0);
            //    listIn.Remove(0);
            //    listIn.Add(10);
            //    listIn.Add(11);
            //    print($"Print List Action", listIn);
            //    return listIn;
            //});

            //print($"Print List Out", list);

        }

        static void print(string message, LOMList<int> list ) {
            Console.WriteLine(message);
            foreach (var item in list)
            {
                Console.WriteLine($"Number: {item}");
            }
            Console.WriteLine("");
        }

       
    }
}
