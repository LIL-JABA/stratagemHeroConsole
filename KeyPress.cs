using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StratagemHero
{
    internal class KeyPressEventArgs : EventArgs
    {
        public ConsoleKeyInfo KeyInfo { get; }

        public KeyPressEventArgs(ConsoleKeyInfo keyInfo)
        {
            KeyInfo = keyInfo;
        }
    }

    internal class KeyPressNotifier
    {
        public delegate void KeyPressEventHandler(object sender, KeyPressEventArgs e);

        public event KeyPressEventHandler KeyPressed;
        private bool isWorking = false;

        public async Task StartListening()
        {
            isWorking = true;
            while (true)
            {
                if (!isWorking) break;
                await Task.Run(() =>
                {
                    var keyInfo = Console.ReadKey(true);
                    OnKeyPressed(new KeyPressEventArgs(keyInfo));
                });
            }
        }

        public async Task StopListening() => isWorking = false;

        protected void OnKeyPressed(KeyPressEventArgs e)
        {
            KeyPressed.Invoke(this, e);
        }
    }
}
