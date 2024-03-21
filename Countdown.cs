using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StratagemHero
{
    internal class Countdown
    {
        public event Action CountdownReachedZero;
        private decimal value;
        private bool isActive = false;
        private readonly Timer timer;

        public Countdown(decimal initialValue)
        {
            this.value = initialValue;
            this.timer = new Timer(Update, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(10));
        }

        public void Start()
        {
            this.isActive = true;
        }

        public void Stop()
        {
            this.isActive = false;
        }

        private void Update(object state)
        {
            if (!this.isActive) return;

            this.value -= 0.01m;
            Console.Title = this.value.ToString("0.00");
            if (this.value <= 0)
            {
                Stop();
                CountdownReachedZero?.Invoke();
            }
        }

        public async Task WaitForCompletionAsync()
        {
            await Task.Delay(Timeout.Infinite);
        }
    }
}
