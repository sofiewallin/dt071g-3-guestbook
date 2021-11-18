using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/*
* Code by Sofie Wallin (sowa2002), student at MIUN, 2021
*/

namespace GuestBook
{
    class GuestBook 
    {
        // PROPERTIES
        [JsonInclude]
        public string Name { get; private set; }
        [JsonInclude]
        public List<Entry> Entries { get; private set; }
        [JsonIgnore]
        public bool HasEntries {
            get {
                return Entries.Count > 0;
            }
        }

        // CONSTRUCTOR
        [JsonConstructor]
        public GuestBook(string name)
        {
            Name = name;
            Entries = new List<Entry>();
        }

        // METHODS

        // Add entry to guestbook
        public Entry AddEntry(Entry entry)
        {
            Entries.Add(entry);
            return entry;
        }

        // Remove entry from guest book
        public Entry RemoveEntry(int entryIndex)
        {
            Entry removedEntry = Entries[entryIndex];
            Entries.RemoveAt(entryIndex);
            return removedEntry;
        }

        // Print all entries
        public void PrintEntries() {
            foreach(Entry entry in Entries)
            {
                int entryIndex = Entries.IndexOf(entry);
                Console.WriteLine($"[{entryIndex}] {entry.AuthorName} – {entry.Message}");

            }            
        }
    }
}
