using System;
using System.Collections.Generic;
using System.Text;

namespace LOM
{
    public static class Extensions
    {
        public static IList<T> Safe<T>(this IList<T> list, Func<IList<T>, IList<T>> action)
        {
            list = action(list);
            list.RemoveAt(0);
            list.RemoveAt(0);
            list.Add(list[1]);
            list.Add(list[2]);
            Console.WriteLine("Print extension");
            foreach (var item in list)
            {
                Console.WriteLine($"Number: {item}");
            }
            Console.WriteLine("");
            return list; 
        }
    }
}
