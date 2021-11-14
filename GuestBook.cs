using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GuestBook
{
    class GuestBook 
    {
        // Constructor
        [JsonConstructor]
        public GuestBook(string name)
        {
            Name = name;
            Entries = new List<Entry>();
        }

        // Properties
        [JsonInclude]
        public string Name { get; private set; }
        [JsonInclude]
        public List<Entry> Entries { get; private set; }

        // Methods
        public Entry AddEntry(Entry entry)
        {
            Entries.Add(entry);
            return entry;
        }

        public Entry RemoveEntry(int entryIndex)
        {
            Entries.RemoveAt(entryIndex);
            return Entries[entryIndex];
        }

        public void PrintEntries() {
            foreach(Entry entry in Entries)
            {
                int entryIndex = Entries.IndexOf(entry);
                Console.WriteLine($"[{entryIndex}] {entry.AuthorName} – {entry.Message}");

            }            
        }
    }
}
