using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace CraneAPI
{
    public class CraneController : INotifyPropertyChanged
    {
        private HttpClient mainClient, secondaryClient;
        private Dictionary<EScope, bool> runningMotors;

        public event PropertyChangedEventHandler PropertyChanged;

        public Uri BaseAdress
        {
            get
            {
                return mainClient.BaseAddress;
            }
            set
            {
                mainClient.BaseAddress = value;
            }
        }

        public bool Initialized { get { return State == EState.Initialized; } }
        public EState State { get; private set; }

        public CraneController()
        {
            mainClient = new HttpClient();
            secondaryClient = new HttpClient();
            runningMotors = new Dictionary<EScope, bool>();
        }

        public async Task Initialize()
        {
            if (mainClient.BaseAddress == null)
                throw new ArgumentException("Base adress not set.");

            State = EState.Initializing;

            while (true)
            {
                var result = await mainClient.GetAsync($"getOtherEsp");
                if (!result.IsSuccessStatusCode)
                {
                    State = EState.Error;
                    return;
                }

                var ipString = await result.Content.ReadAsStringAsync();
                if (ipString != "0.0.0.0" && !ipString.Contains("unset"))
                {
                    secondaryClient.BaseAddress = new Uri("http://" + ipString);

                    await CheckSecondaryAsync();

                    return;
                }

                await Task.Delay(1000);
            }
        }

        private async Task CheckSecondaryAsync()
        {
            while (true)
            {
                var result = await secondaryClient.GetAsync($"getOtherEsp");
                if (!result.IsSuccessStatusCode)
                {
                    State = EState.Error;
                    return;
                }

                var ipString = await result.Content.ReadAsStringAsync();

                if (ipString != "0.0.0.0" && !ipString.Contains("unset"))
                {
                    secondaryClient.BaseAddress = new Uri("http://" + ipString);

                    State = EState.Initialized;

                    return;
                }

            }
        }

        public async Task<Tuple<int, int>> GetBatteryAsync()
        {
            var resultMain = await mainClient.GetAsync($"getBat?n=1");
            var resultSecondary = await secondaryClient.GetAsync($"getBat?n=2");

            if (!resultMain.IsSuccessStatusCode || !resultSecondary.IsSuccessStatusCode)
            {
                State = EState.Error;
                return null;
            }

            return new Tuple<int, int>(int.Parse(await resultMain.Content.ReadAsStringAsync()), int.Parse(await resultSecondary.Content.ReadAsStringAsync()));
        }

        public async Task StartMoveAsync(EScope scope, EDirection direction)
        {
            if (!Initialized)
                throw new NotSupportedException("Instance needs to be in state Initialized.");

            var localClient = SelectClient(ref scope);

            var result = await localClient.GetAsync($"startM?sNr={(int)scope}&turn={(int)direction}");

            if (!result.IsSuccessStatusCode)
            {
                this.State = EState.Error;
                return;
            }

        }

        public async Task StopMoveAsync(EScope scope)
        {
            if (!Initialized)
                throw new NotSupportedException("Instance needs to be in state Initialized.");

            var localClient = SelectClient(ref scope);

            var result = await localClient.GetAsync($"stopM?sNr={(int)scope}");

            if (!result.IsSuccessStatusCode)
            {
                this.State = EState.Error;
                return;
            }
        }

        private HttpClient SelectClient(ref EScope scope)
        {
            switch (scope)
            {
                case EScope.Crane:
                    return mainClient;
                case EScope.Crab:
                case EScope.Spreader:
                    scope -= 1;
                    return secondaryClient;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum EState
    {
        Created,
        Initializing,
        Initialized,
        Error
    }

    public enum EDirection
    {
        Up = 1,
        Right = Up,
        Forward = Up,
        Down = -1,
        Left = Down,
        Reverse = Down
    }

    public enum EScope
    {
        Crane = 1,
        Crab = 2,
        Spreader = 3
    }
}
