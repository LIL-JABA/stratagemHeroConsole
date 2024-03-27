using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StratagemHero
{
    internal class OnCountEventArgs : EventArgs
    {
        public decimal Value { get; }

        public OnCountEventArgs(decimal value)
        {
            Value = value;
        }
    }
    internal class Countdown
    {
        public delegate void OnCountEventHandler(object sender, OnCountEventArgs e);
        public event Action CountdownReachedZero;
        public event OnCountEventHandler OnCount;
        private decimal value;
        private bool isActive = false;
        private readonly Timer timer;

        public Countdown(decimal initialValue)
        {
            this.value = initialValue;
            this.timer = new Timer(Update, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(10));
        }

        public void Start() => this.isActive = true;
        public void Stop() => this.isActive = false;
        public void Add(decimal value) => this.value += value;

        private void Update(object state)
        {
            if (!this.isActive) return;
            OnCountVoid(new OnCountEventArgs(this.value));
            if (this.value <= 0)
            {
                Stop();
                CountdownReachedZero?.Invoke();
            }
            this.value -= 0.01m;
        }

        public async Task WaitForCompletionAsync() => await Task.Delay(Timeout.Infinite);
        protected void OnCountVoid(OnCountEventArgs e) => OnCount.Invoke(this, e);
    }
}
