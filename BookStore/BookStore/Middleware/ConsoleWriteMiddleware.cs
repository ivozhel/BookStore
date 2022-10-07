using System.Drawing;

namespace BookStore.Middleware
{
    public class ConsoleWriteMiddleware
    {
        private readonly RequestDelegate _next;
        public ConsoleWriteMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {

            for (int i = 0; i < 1; i++)
            {
                
                for (int j = 0; j < 30; j++)
                {
                    Console.Clear();

                    // steam
                    Console.Write("       . . . . o o o o o o", Color.LightGray);
                    for (int s = 0; s < j / 2; s++)
                    {
                        Console.Write(" o", Color.LightGray);
                    }
                    Console.WriteLine();

                    var margin = "".PadLeft(j);
                    Console.WriteLine(margin + "                _____      o", Color.LightGray);
                    Console.WriteLine(margin + "       ____====  ]OO|_n_n__][.", Color.DeepSkyBlue);
                    Console.WriteLine(margin + "      [________]_|__|________)< ", Color.DeepSkyBlue);
                    Console.WriteLine(margin + "       oo    oo  'oo OOOO-| oo\\_", Color.Blue);
                    Console.WriteLine("   +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+", Color.Silver);
                    Console.WriteLine("Checking if you want to Get or not ...");

                    Thread.Sleep(200);
                }
            }

            Console.WriteLine("\rDone!          ");
            if (context.Request.Method == "GET")
            {
                Console.WriteLine("Yes you do");
            }
            else
            {
                Console.WriteLine("You dont");
            }
        
            
            await _next(context);
        }
    }
}
