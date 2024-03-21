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
        private static Stratagem curStratagem;
        private static readonly Random random = new Random();
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            await Stratagems.loadStratagems();
            //Countdown countdown = new Countdown(3m);
            //countdown.Start();
            //countdown.CountdownReachedZero += () =>
            //{
            //    Console.WriteLine("Countdown reached zero!");
            //};
            //await countdown.WaitForCompletionAsync();
            var keyPressNotifier = new KeyPressNotifier();
            keyPressNotifier.KeyPressed += async (sender, e) =>
            {
                char key = $"{e.KeyInfo.Key}".ToArray()[0];
                //Console.WriteLine($"Key pressed: {e.KeyInfo.Key}");
                await handleKey(e.KeyInfo.Key);
            };
            Task listeningTask = Task.Run(async () => await keyPressNotifier.StartListening());
            await chooseNextStratagem();

            await listeningTask;
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
            if((char)key == curStratagem.keys[index])
            {
                Console.Write(curStratagem.symbols[index++]);
                Console.Write(' ');
            }
            else
            {
                index = 0;
                Console.Clear();
                Console.WriteLine(curStratagem.name);
                Console.WriteLine(string.Join(' ', curStratagem.symbols));
            }
            if (index == maxIndex) await chooseNextStratagem();
        }
    }
}
