using System;
using System.IO;
using System.Text.Json;

/*
* Code by Sofie Wallin (sowa2002), student at MIUN, 2021
*/

namespace GuestBook
{
    // Set message types
    enum MessageType {
        Success,
        Error
    }

    // PROGRAM
    class Program
    {         
        private static readonly string nl = Environment.NewLine;
        private static readonly string file = "guestbook.json";

        // MAIN
        public static void Main(string[] args)
        {
            // Initate guestbook from file if file exists or create new guestbook if it doesn't
            GuestBook guestBook = InitiateGuestBook();

            // Loop program until exit
            do
            {          
                Console.Clear();
                Console.WriteLine(guestBook.Name);

                // Print menu
                Console.WriteLine($"{nl}{nl}1. Skriv ett inlägg");

                // Only print this option if there are entries
                if(guestBook.HasEntries)
                {
                    Console.WriteLine("2. Ta bort inlägg");
                }

                Console.WriteLine($"{nl}x. Avsluta{nl}{nl}");

                // Print entries if there are any or else print message
                if(guestBook.HasEntries)
                {
                    guestBook.PrintEntries();

                    // Get input from user
                    Console.WriteLine($"{nl}{nl}Vad vill du göra? Tryck 1, 2 eller x följt av <Enter>:");
                }
                else
                {
                    Console.WriteLine("Det finns inga inlägg ännu.");

                    // Get input from user
                    Console.WriteLine($"{nl}{nl}Vad vill du göra? Tryck 1 eller x följt av <Enter>:");
                }  

                // Store input
                string input = Console.ReadLine();

                switch(input)
                {
                    // Add entry if input is 1
                    case "1":
                        AddEntry(guestBook);
                        break;
                    // Remove entry if input is 2
                    case "2":
                        // Only go to remove entry if there are any
                        if(guestBook.HasEntries)
                        {
                            RemoveEntry(guestBook);
                            break;
                        }
                        else continue;
                    // Exit program if entry is x
                    case "x":
                    case "X":
                        ExitProgram();
                        break;
                    // Continue if input is anything else
                    default:
                        continue;
                }
            } while(true);
        }

        // INITIATE GUESTBOOK
        public static GuestBook InitiateGuestBook()
        {
            GuestBook guestBook;

            // Check if the file set for the program exists
            if(File.Exists(file))
            {
                // Read Json-file and deserialize
                string jsonString = File.ReadAllText(file);
                JsonSerializerOptions optionsDeserialize = new JsonSerializerOptions { IncludeFields = true };
                // Set deserialized data as guest book
                guestBook = JsonSerializer.Deserialize<GuestBook>(jsonString, optionsDeserialize);
            }
            else
            {
                // Instantiate new guest book
                guestBook = new GuestBook("S O F I E S   G Ä S T B O K");  
            }
            return guestBook;
        }

        // ADD ENTRY
        public static void AddEntry(GuestBook guestBook)
        {
            Console.Clear();
            Console.WriteLine(guestBook.Name);

            // Print cancel option
            Console.WriteLine($"{nl}{nl}x. Avbryt");

            // Get name input from user
            Console.WriteLine($"{nl}{nl}Ange ditt namn:");

            // Loop until action is finished or user cancels
            do
            {
                // Store input
                string nameInput = Console.ReadLine();

                // Cancel back to main view if input is x
                if(nameInput == "x" || nameInput == "X") break;

                // Loop error message and input field until name input is a valid name
                while(nameInput.Length < 2)
                {
                    WriteMessage(MessageType.Error, $"{nl}Du måste ange ett namn som är minst 2 tecken.");
                    nameInput = Console.ReadLine();
                }

                // Get message input from user
                Console.WriteLine($"{nl}Skriv ett meddelande:");

                // Store input
                string messageInput = Console.ReadLine();

                // Cancel back to main view if input is x
                if(messageInput == "x" || messageInput == "X") break;

                // Loop error message and input field until message input is a valid message
                while(messageInput.Length < 2)
                {
                    WriteMessage(MessageType.Error, $"{nl}Du måste ange ett meddelande som är minst 2 tecken.");
                    messageInput = Console.ReadLine();
                }

                // Create new entry and add it to guest book
                Entry newEntry = new Entry(nameInput, messageInput);
                newEntry = guestBook.AddEntry(newEntry);

                // Save guest book to file
                SaveGuestBook(guestBook);

                // Write success message
                WriteMessage(MessageType.Success, $"{nl}{nl}Ditt inlägg har lagts till:{nl}{nl}{newEntry.AuthorName} – {newEntry.Message}");
                
                // Ask user to enter any key to continue
                Console.WriteLine($"{nl}{nl}Tryck på vilken tangent som helst för att fortsätta.");
                Console.ReadKey(true);
                break;
            } while(true);
        }

        // REMOVE ENTRY
        public static void RemoveEntry(GuestBook guestBook)
        {
            Console.Clear();
            Console.WriteLine(guestBook.Name);

            // Print cancel option
            Console.WriteLine($"{nl}{nl}x. Avbryt{nl}{nl}");

            // Print entries
            guestBook.PrintEntries();

            // Get user input
            Console.WriteLine($"{nl}{nl}Ange index på det inlägg du vill ta bort, ex. 1:");
            
            // Loop until action is finished or user cancels
            do
            {
                // Store input
                string input = Console.ReadLine();

                // Cancel back to main view if input is x
                if(input == "x" || input == "X") break;

                // Perform action if string input can be parsed as int or else print error message
                if(int.TryParse(input, out int inputInt))
                {
                    try
                    {
                        // remove entry from guest book
                        Entry removedEntry = guestBook.RemoveEntry(inputInt);

                        // Save guest book to file
                        SaveGuestBook(guestBook);

                        // Write success message
                        WriteMessage(MessageType.Success, $"{nl}{nl}Detta inlägg har tagits bort:{nl}{nl}{removedEntry.AuthorName} – {removedEntry.Message}");
                        
                        // Ask user to enter any key to continue
                        Console.WriteLine($"{nl}{nl}Tryck på vilken tangent som helst för att fortsätta.");
                        Console.ReadKey(true);
                        break;
                    }
                    catch(ArgumentOutOfRangeException)
                    {
                        // Write error message if int input is not an index in the entries list
                        WriteMessage(MessageType.Error, $"{nl}Du måste ange ett giligt index. Se listan ovan för befintliga inlägg.");
                    }
                }
                else
                {
                    WriteMessage(MessageType.Error, $"{nl}Du måste ange ett index i form av ett heltal för det inlägg du vill ta bort.");
                }
            } while(true);
        }

        // WRITE MESSAGE in different color depending on type
        public static void WriteMessage(MessageType type, string message)
        {
            // If error message set color to red else set color to green
            if(type == MessageType.Error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            // Write message
            Console.WriteLine(message);

            // Reset color
            Console.ResetColor();
        }

        // SAVE GUEST BOOK TO FILE
        public static void SaveGuestBook(GuestBook guestBook)
        {
            // Serialize guest book and write it to file
            JsonSerializerOptions optionsSerialize = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize<GuestBook>(guestBook, optionsSerialize);
            File.WriteAllText(file, jsonString);
        }

        // EXIT PROGRAM
        public static void ExitProgram()
        {
            Environment.Exit(0);
        }
    }
}
