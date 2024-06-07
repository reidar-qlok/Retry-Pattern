namespace Retry_Pattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Definiera maximalt antal försök och fördröjning mellan försök
            int maxRetries = 3;
            int retryDelayMilliseconds = 2000;

            // Loop för att tillåta användaren att köra om operationen genom att trycka på Enter
            do
            {
                // Försök att utföra operationen med retry-logik
                bool success = RetryOperation(maxRetries, retryDelayMilliseconds, PerformOperation);

                // Ge feedback till användaren baserat på resultatet av operationen
                if (success)
                {
                    Console.WriteLine("Operationen lyckades.");
                }
                else
                {
                    Console.WriteLine("Operationen misslyckades efter maximalt antal försök.");
                }

                Console.WriteLine("Tryck på Enter för att försöka igen eller någon annan tangent för att avsluta.");
                Console.WriteLine();
            }
            while (Console.ReadKey().Key == ConsoleKey.Enter);
        }

        // Metod som hanterar retry-logik för en given operation
        public static bool RetryOperation(int maxRetries, int retryDelayMilliseconds, Action operation)
        {
            int retryCount = 0; // Initialisera antalet försök

            // Loop för att försöka utföra operationen flera gånger
            while (retryCount < maxRetries)
            {
                try
                {
                    operation(); // Försök att utföra operationen
                    return true; // Om operationen lyckas, returnera true
                }
                catch (Exception ex)
                {
                    // Om ett fel inträffar, visa felmeddelande och försökets nummer
                    Console.WriteLine($"Fel inträffade: {ex.Message}");
                    Console.WriteLine($"Försök {retryCount + 1}/{maxRetries}");

                    // Fördröjning innan nästa försök
                    Thread.Sleep(retryDelayMilliseconds);

                    retryCount++; // Öka antalet försök
                }
            }

            return false; // Returnera false om operationen misslyckades efter maximalt antal försök
        }

        // Simulerad operation som slumpmässigt lyckas eller misslyckas
        public static void PerformOperation()
        {
            Random random = new Random();
            int result = random.Next(0, 5); // Generera ett slumpmässigt nummer mellan 0 och 4

            if (result == 0)
            {
                // Simulera misslyckande
                Console.WriteLine("Operationen misslyckades.");
                throw new Exception("Operationen misslyckades.");
            }
            else
            {
                // Simulera framgång
                Console.WriteLine("Operationen lyckades.");
            }
        }
    }
}
