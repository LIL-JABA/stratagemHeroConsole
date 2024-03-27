using stratagemHeroConsole;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace StratagemHero
{
    class Program
    {
        static int index;
        static int maxIndex;
        private static readonly Random random = new Random();
        private static Stratagem curStratagem;
        private static Countdown countdown;
        private static Score score;
        private static bool isTimeOut = false;
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            score = new Score();
            await Stratagems.loadStratagems();
            await chooseNextStratagem();

            var keyPressNotifier = new KeyPressNotifier();
            keyPressNotifier.KeyPressed += async (sender, e) =>
            {
                char key = $"{e.KeyInfo.Key}".ToArray()[0];
                await handleKey(e.KeyInfo.Key);
            };

            countdown = new Countdown(3m);
            countdown.Start();
            countdown.OnCount += async (sender, e) =>
            {
                Console.Title = $"{e.Value.ToString("0.00")} | Score: {score.points} | Errors: {score.errors}";
            };
            countdown.CountdownReachedZero += async () =>
            {
                Console.Clear();
                await keyPressNotifier.StopListening();
                isTimeOut = true;
                Console.WriteLine("Time out!");
            };

            await keyPressNotifier.StartListening();
            Task listeningTask2 = Task.Run(async () => await countdown.WaitForCompletionAsync());
            await listeningTask2;
        }
        static async Task chooseNextStratagem()
        {
            Console.Clear();
            int randomIndex = random.Next(Stratagems.stratagems.Count);
            curStratagem = Stratagems.stratagems[randomIndex];
            index = 0;
            maxIndex = curStratagem.keys.Length;
            Console.WriteLine(curStratagem.name);
            Console.WriteLine(string.Join(' ', curStratagem.symbols));
        }
        static async Task handleKey(ConsoleKey key)
        {
            if (isTimeOut) return;
            if((char)key == curStratagem.keys[index])
            {
                Console.Write(curStratagem.symbols[index++]);
                Console.Write(' ');
            }
            else
            {
                score.IncErrors();
                index = 0;
                Console.Clear();
                Console.WriteLine(curStratagem.name);
                Console.WriteLine(string.Join(' ', curStratagem.symbols));
            }
            if (index == maxIndex)
            {
                score.IncPoints();
                await chooseNextStratagem();
                countdown.Add(1m);
            }
        }
    }
}
