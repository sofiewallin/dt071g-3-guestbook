using System;
using System.IO;
using System.Text.Json;

namespace GuestBook
{
    class Program
    {         
        private static readonly string nl = Environment.NewLine;

        // Main
        public static void Main(string[] args)
        {
            Console.Clear();

            string file = "guestbook.json";
            string jsonString;
            GuestBook guestBook;

            if(File.Exists(file))
            {
                jsonString = File.ReadAllText(file);
                JsonSerializerOptions optionsDeserialize = new JsonSerializerOptions { IncludeFields = true };
                guestBook = JsonSerializer.Deserialize<GuestBook>(jsonString, optionsDeserialize);
                /*try
                {
                    jsonString = File.ReadAllText(file);
                    guestBook = JsonSerializer.Deserialize<GuestBook>(jsonString);
                }
                catch(JsonException)
                {
                    Console.WriteLine($"Försökte läsa \"guestbook.json\" men den innehöll ingen giltig data och skrevs över med en ny gästbok.{nl}");
                    guestBook = new GuestBook("SOFIES GÄSTBOK");  
                }*/
            }
            else
            {
                guestBook = new GuestBook("SOFIES GÄSTBOK");  
            }   

            Console.WriteLine($"{guestBook.Name}");

            Console.WriteLine($"{nl}Gästboken har {guestBook.Entries.Count} inlägg.");

            Console.WriteLine($"{nl}Jag lägger till fyra inlägg.");
            Entry entry1 = new Entry("Sofie", "Hejsan hoppsan!");
            Entry entry2 = new Entry("Robin", "Tjenixen!");
            Entry entry3 = new Entry("Benjamin", "Hello mon amor!");
            Entry entry4 = new Entry("Gabriel", "Hej, hej, hej!");
            guestBook.AddEntry(entry1);
            guestBook.AddEntry(entry2);
            guestBook.AddEntry(entry3);
            guestBook.AddEntry(entry4);
            
            Console.WriteLine($"{nl}Gästboken har {guestBook.Entries.Count} inlägg.{nl}");

            guestBook.PrintEntries();

            Console.WriteLine($"{nl}Jag tar bort ett inlägg");
            guestBook.RemoveEntry(0);

            Console.WriteLine($"{nl}Gästboken har {guestBook.Entries.Count} inlägg.{nl}");

            guestBook.PrintEntries();

            Console.WriteLine($"{nl}Jag sparar gästboken till fil.");
            JsonSerializerOptions optionsSerialize = new JsonSerializerOptions { WriteIndented = true };
            jsonString = JsonSerializer.Serialize<GuestBook>(guestBook, optionsSerialize);
            File.WriteAllText(file, jsonString);
        }
    }
}
