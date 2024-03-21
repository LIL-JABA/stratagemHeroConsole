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
        // Define a delegate to specify the signature of the event handler method
        public delegate void KeyPressEventHandler(object sender, KeyPressEventArgs e);

        // Define the event using the delegate
        public event KeyPressEventHandler KeyPressed;

        // Method to start listening for key presses
        public async Task StartListening()
        {
            while (true)
            {
                // Await for a key press asynchronously
                await Task.Run(() =>
                {
                    var keyInfo = Console.ReadKey(true);
                    // Raise the event when a key is pressed
                    OnKeyPressed(new KeyPressEventArgs(keyInfo));
                });
            }
        }

        // Method to raise the KeyPressed event
        protected void OnKeyPressed(KeyPressEventArgs e)
        {
            KeyPressed?.Invoke(this, e);
        }
    }
}
