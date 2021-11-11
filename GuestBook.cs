using System;
using System.Collections.Generic;
using System.IO;

namespace guestbook
{
    class GuestBook 
    {
        // Fields
        private string name;
        private string file;
        private List<Entry> entrys;

        // Constructor
        public GuestBook(string name, string file)
        {
            this.name = name;
            this.file = file;
            this.entrys = new List<Entry>();
        }

        // Methods
        public override string ToString()
        {
            return $"{this.name}";
        }

        public void ListEntries() 
        {
            if (File.Exists(this.file))
            {

            }
        }

        public void AddEntry()
        {
            if (File.Exists(this.file))
            {

            }
        }

        public void DeleteEntry()
        {
            if (File.Exists(this.file))
            {

            }
        }


    }
}