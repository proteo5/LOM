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
            string[] words = newString.Split(separator);
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

        internal static string ToString(Guid stringID, string separator)
        {
            var stringToReturn = new StringBuilder();
            var theString = strings[stringID];

            foreach (var item in theString)
            {
                stringToReturn.Append(vault[item] + " ");
                
            }

            return stringToReturn.ToString().TrimEnd();
        }
    }
}
