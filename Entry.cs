using System;

namespace guestbook
{
    class Entry 
    {
        // Fields
        private string authorName;        
        private string message;

        // Constructor
        public Entry(string authorName, string message)
        {
            this.authorName = authorName;
            this.message = message;
        }

        // Methods
        public override string ToString()
        {
            return $"{authorName} – {message}";
        }
    }
}