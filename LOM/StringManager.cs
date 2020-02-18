using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LOM
{

    internal static class StringManager
    {
        private static readonly Dictionary<uint, string> vault;
        private static readonly Dictionary<Guid, List<uint>> strings;
        private static uint vaultIndex = 0;

        static StringManager()
        {
            vault = new Dictionary<uint, string>();
            strings = new Dictionary<Guid, List<uint>>();
        }

        internal static void AddNewString(Guid stringID, string newString, string separator)
        {
            strings.Add(stringID, new List<uint>());
            string[] words;
            if (string.IsNullOrEmpty(separator))
            {
                words = newString.Split(new string[] { "\r\n", "\r", "\n", Environment.NewLine }, StringSplitOptions.None);
            }
            else
            {
                words = newString.Split(separator);
            }

            for (int i = 0; i < words.Length; i++)
            {
                var exist = vault.Where(f => f.Value == words[i]);
                uint stringIndex = vaultIndex;
                if (!exist.Any())
                {
                    vault.Add(vaultIndex, words[i]);
                    vaultIndex++;
                }
                else
                {
                    stringIndex = exist.FirstOrDefault().Key;
                }
                strings[stringID].Add(stringIndex);
            }
        }

        internal static string GetAtIndex(Guid stringID, int index)
        {
            var internalString = strings[stringID];
            var valueIndex = internalString[index];
            var value = vault[valueIndex];
            return value;
        }

        internal static void SetAtIndex(Guid stringID, int index, string value)
        {
            var internalString = strings[stringID];
            internalString.RemoveAt(index);
            var newVaultIndex = vaultIndex;
            vaultIndex++;
            internalString.Insert(index, newVaultIndex);
            vault[newVaultIndex] = value;
        }

        internal static string ToString(Guid stringID, string separator)
        {
            var stringToReturn = new StringBuilder();
            var theString = strings[stringID];
            if (string.IsNullOrEmpty(separator))
                separator = Environment.NewLine;

            foreach (var item in theString)
            {
                stringToReturn.Append(vault[item]);
                stringToReturn.Append(separator);
            }

            return stringToReturn.ToString().TrimEnd();
        }

        internal static Guid Clone(Guid stringID)
        {
            Guid newStringID = Guid.NewGuid();
            List<uint> newString = new List<uint>();

            foreach (var item in strings[stringID])
            {
                newString.Add(item);
            }

            strings[newStringID] = newString;

            return newStringID;
        }

        internal static void Add(Guid stringID, string value)
        {
            //Save the value at the valult;
            var newVaultIndex = vaultIndex;
            vaultIndex++;
            vault.Add(newVaultIndex, value);

            //Save the objects index
            //Check if list exist
            if (!strings.TryGetValue(stringID, out List<uint> stringList)) stringList = new List<uint>();
            //add index to the list
            stringList.Add(newVaultIndex);
            strings[stringID] = stringList;
        }

        internal static void Insert(Guid stringID, int index, string value)
        {
            var internalstring = strings[stringID];
            var newVaultIndex = vaultIndex;
            vaultIndex++;
            internalstring.Insert(index, newVaultIndex);
            vault[newVaultIndex] = value;
        }

        internal static bool Remove(Guid stringID, object value)
        {
            var internalstring = strings[stringID];

            var key = vault.FirstOrDefault(x => x.Value.Equals(value)).Key;
            var result = internalstring.Remove(key);
            return result;
        }

        internal static void RemoveAt(Guid stringID, int index)
        {
            var internalstring = strings[stringID];
            internalstring.RemoveAt(index);
        }

        internal static int IndexOf(Guid stringID, string value)
        {
            var internalstring = strings[stringID];
            var key = vault.FirstOrDefault(x => x.Value == value && internalstring.Contains(x.Key)).Key;
            return internalstring.IndexOf(key);
        }

        internal static bool Contains(Guid stringID, string value)
        {
            var internalstring = strings[stringID];

            return vault.Any(x => x.Value.Contains(value) && internalstring.Contains(x.Key));
        }

        internal static int Count(Guid stringID) => strings[stringID].Count;

        internal static void Clear(Guid stringID)
        {
            var internalstring = strings[stringID];
            internalstring.Clear();
        }

        internal static string[] CopyTo(Guid stringID)
        {
            var internalstring = strings[stringID];
            int howMany = internalstring.Count;
            string[] array = new string[howMany];
            uint[] childArray = new uint[howMany];
            internalstring.CopyTo(childArray);

            for (int i = 0; i < childArray.Length; i++)
            {
                array[i] = vault[childArray[i]];
            }
            return array;
        }

        internal static void Dispose(Guid stringID)
        {
            
        }
    }
}
