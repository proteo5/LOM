using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LOM
{
    internal static class ListManager
    {
        private static readonly Dictionary<Guid, object> vault;
        private static readonly Dictionary<Guid, List<Guid>> lists;

        static ListManager()
        {
            vault = new Dictionary<Guid, object>();
            lists = new Dictionary<Guid, List<Guid>>();
        }

        internal static void Add(Guid listID, object item)
        {
            //Save the value at the valult;
            Guid vaultIndex = Guid.NewGuid();
            vault.Add(vaultIndex, item);

            //Save the objects index
            //Check if list exist
            List<Guid> list;
            if (!lists.TryGetValue(listID, out list)) list = new List<Guid>();
            //add index to the list
            list.Add(vaultIndex);
            lists[listID] = list;
        }

        internal static object GetAtIndex(Guid listID, int index)
        {
            var internallist = lists[listID];
            var valueIndex = internallist[index];
            var value = vault[valueIndex];
            return value;
        }

        internal static void SetAtIndex(Guid listID, int index, object value)
        {
            var internallist = lists[listID];
            internallist.RemoveAt(index);
            Guid newVaultIndex = Guid.NewGuid();
            internallist.Insert(index, newVaultIndex);
            vault[newVaultIndex] = value;
        }

        internal static void Insert(Guid listID, int index, object value)
        {
            var internallist = lists[listID];
            Guid newVaultIndex = Guid.NewGuid();
            internallist.Insert(index, newVaultIndex);
            vault[newVaultIndex] = value;
        }

        internal static bool Remove(Guid listID, object value)
        {
            var internallist = lists[listID];

            var key = vault.FirstOrDefault(x => x.Value.Equals(value)).Key;
            var result = internallist.Remove(key);
            return result;
        }

        internal static void RemoveAt(Guid listID, int index)
        {
            var internallist = lists[listID];
            internallist.RemoveAt(index);
        }

        internal static bool Contains(Guid listID, object value)
        {
            var internallist = lists[listID];

            return vault.Any(x => x.Value.Equals(value));
        }
        internal static int IndexOf(Guid listID, object value)
        {
            var internallist = lists[listID];
            var key = vault.FirstOrDefault(x => x.Value.Equals(value)).Key;
            return internallist.IndexOf(key);
        }
        internal static void Clear(Guid listID)
        {
            var internallist = lists[listID];
            internallist.Clear();
        }

        internal static object[] CopyTo(Guid listID)
        {
            var internallist = lists[listID];
            int howMany = internallist.Count ;
            object[] array = new object[howMany];
            Guid[] childArray = new Guid[howMany];
            internallist.CopyTo(childArray);

            for (int i = 0; i < childArray.Length; i++)
            {
                array[i] = vault[childArray[i]];
            }
            return array;
        }

        internal static int Count(Guid objectID) => lists[objectID].Count;

        internal static Guid Clone(Guid objectID)
        {
            Guid newObjectID = Guid.NewGuid();
            List<Guid> newList = new List<Guid>();

            foreach (var item in lists[objectID])
            {
                newList.Add(item);
            }

            lists[newObjectID] = newList;
            return newObjectID;
        }

    }

}
