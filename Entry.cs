using System.Text.Json.Serialization;

/*
* Code by Sofie Wallin (sowa2002), student at MIUN, 2021
*/

namespace GuestBook
{
    class Entry 
    {   
        // PROPERTIES
        [JsonInclude]
        public string AuthorName { get; private set; }    
        [JsonInclude] 
        public string Message { get; private set; }
        
        // CONSTRUCTOR
        [JsonConstructor]
        public Entry(string authorName, string message)
        {
            AuthorName = authorName;
            Message = message;
        }
    }
}