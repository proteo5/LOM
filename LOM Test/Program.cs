using LOM;
using System;
using System.Collections.Generic;

namespace Lomvm_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestList();
            TestString();

        }

        static void print(string message, LOMList<int> list)
        {
            Console.WriteLine(message);
            foreach (var item in list)
            {
                Console.WriteLine($"Number: {item}");
            }
            Console.WriteLine("");
        }

        static void TestList()
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

            Console.WriteLine("List 1 Contains 0: {0}", list.Contains(0) ? "Yes" : "No");
            Console.WriteLine("List 2 Contains 100: {0}", list.Contains(100) ? "Yes" : "No");

            Console.WriteLine("List 1 Index of 0: {0}", list.IndexOf(0));
            Console.WriteLine("List 2 index of 100: {0}", list.IndexOf(100));

            Console.WriteLine("");
            Console.WriteLine("Print Sublist");
            int[] sublist = new int[list.Count + 10];
            list.CopyTo(sublist);
            list.CopyTo(sublist, 11);

            for (int i = 0; i < sublist.Length; i++)
            {
                Console.WriteLine("{0}", sublist[i]);
            }

            Console.WriteLine();
            Console.WriteLine("List one destroyed");
            list.Destroy();
            print($"Print List 2", list2);


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

        static void TestString()
        {
            string testString = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque sed mauris ut ipsum porttitor dapibus vitae eget urna. Donec nisl quam, ultrices id euismod et, congue et lacus. Mauris interdum faucibus libero ac dignissim. Curabitur eget blandit leo. " + Environment.NewLine
                              + "Pellentesque dictum nec justo ac scelerisque. Sed eu augue feugiat, sodales turpis vel, luctus metus. Nam tempus vitae nunc nec tempor. Ut faucibus urna quis tempor ullamcorper. Etiam ac faucibus ipsum." + Environment.NewLine
                              + "Mauris ornare ante non massa mollis, nec venenatis purus imperdiet.Donec consectetur purus ut nibh fermentum semper.Suspendisse mattis est nulla, nec ornare dui pulvinar eu. Pellentesque dolor ligula, lacinia ut.";
            Console.WriteLine("Original String");
            Console.WriteLine(testString);

            var lomString = new LOMString(testString);
            Console.WriteLine();
            Console.WriteLine("String from LomObject");
            string testString2 = lomString.ToString();
            Console.WriteLine(testString2);

            var lomStringClone = lomString.Clone();
            Console.WriteLine();
            Console.WriteLine("String from Clone");
            string testStringClone = lomStringClone.ToString();
            Console.WriteLine(testStringClone);
           
            //modify clone LOMString
            string paragraph = lomStringClone[1];
            lomStringClone[1] = paragraph.Replace("nec", "necModified");

            Console.WriteLine();
            Console.WriteLine("Original LOMString after modified clone");
            testString2 = lomString.ToString();
            Console.WriteLine(testString2);
            
            Console.WriteLine();
            Console.WriteLine("Cloned LOMString Modified after being modified");
            testStringClone = lomStringClone.ToString();
            Console.WriteLine(testStringClone);

            Console.WriteLine();
            Console.WriteLine("Test add, insert, remove and removeat");
            lomStringClone.Insert(1, "This is a inserted string");
            testStringClone = lomStringClone.ToString();
            Console.WriteLine(testStringClone);
            Console.WriteLine();
            lomStringClone.RemoveAt(1);
            testStringClone = lomStringClone.ToString();
            Console.WriteLine(testStringClone);
            Console.WriteLine();
            var holdstring = lomStringClone[0];
            lomStringClone.Remove(lomStringClone[0]);
            testStringClone = lomStringClone.ToString();
            Console.WriteLine(testStringClone);
            Console.WriteLine();
            lomStringClone.Add(holdstring);
            testStringClone = lomStringClone.ToString();
            Console.WriteLine(testStringClone);
        }


    }
}
