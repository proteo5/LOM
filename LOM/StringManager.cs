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
                words = newString.Split(new string[] { "\r\n","\r", "\n", Environment.NewLine }, StringSplitOptions.None);
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
    }
}
