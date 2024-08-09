using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects.Models.Futures.Socket;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using System.Timers;
using TradingAppMvc.Services;
using TradingAppMvc.Domain.Enums;
namespace TradingAppMvc.Services
{
    public class BinanceAccountProvider : IBinanceAccountProvider
    {
        public BinanceAccountProvider(IBinanceRestClient client, IBinanceSocketClient socketClient)
        {
            _client = client;
            _socketClient = socketClient;
        }

        private IBinanceSocketClient _socketClient;
        private IBinanceRestClient _client;
        private System.Timers.Timer aTimer;
        private Dictionary<string, string> _subscriptions = new Dictionary<string, string>();
        public event EventHandler<BinanceFuturesStreamOrderUpdate> OnRaiseUpdateOrder;
        public class OrderUpdate : EventArgs
        {
            public OrderUpdate(BinanceFuturesStreamOrderUpdate orderUpdate)
            {
                OrderUpdateData = orderUpdate;
            }

            public BinanceFuturesStreamOrderUpdate OrderUpdateData { get; set; }
        }

        protected virtual void RaiseUpdateOrder(BinanceFuturesStreamOrderUpdate e)
        {
            OnRaiseUpdateOrder?.Invoke(this, e);
        }


        public async Task Start(string privateKey, string publicKey, Derivate derivate)
        {
            if (!_subscriptions.ContainsKey(publicKey))
            {
                await SetUserUpdates(privateKey, publicKey, derivate);
            }
        }

        private async Task SetUserUpdates(string publicKey, string privateKey, Derivate derivate)
        {
            var credentials = new ApiCredentials(publicKey, privateKey);
            _client.SetApiCredentials(credentials);

            switch (derivate)
            {
                case Derivate.Futures:
                    var listenKeyFutures = _client.UsdFuturesApi.Account.StartUserStreamAsync().Result.Data;

                    var futuresUserUpdate = await _socketClient.UsdFuturesApi.SubscribeToUserDataUpdatesAsync(listenKeyFutures, onLeverageUpdate =>
                    {
                    }, onMarginUpdate =>
                    {
                    }, onAccountUpdate =>
                    {
                    }, onOrderUpdate =>
                    {
                        RaiseUpdateOrder(onOrderUpdate.Data);
                    }, onListenKeyExpired =>
                    {
                    });
                    if (futuresUserUpdate.Success)
                    {
                        var socketId = futuresUserUpdate.Data.Id;
                        var keyValue = string.Join("|", derivate, listenKeyFutures, socketId);
                        _subscriptions.TryAdd(publicKey, keyValue);
                    }
                    break;
                case Derivate.Coin:
                    var coinUserUpdate = await _socketClient.CoinFuturesApi.SubscribeToUserDataUpdatesAsync("", onLeverageUpdate =>
                    {
                    }, onMarginUpdate =>
                    {
                    }, onAccountUpdate =>
                    {
                    }, onOrderUpdate =>
                    {
                        RaiseUpdateOrder(onOrderUpdate.Data);
                    }, onListenKeyExpired =>
                    {
                    });
                    break;
                default:
                    break;
            }

            SetTimer();
        }

        public async Task Stop(string publicKey)
        {
            if (_subscriptions.TryGetValue(publicKey, out var keyValue))
            {
                var values = keyValue.Split("|");
                var socketId = int.Parse(values[2]);
                await _socketClient.UnsubscribeAsync(socketId);
                _subscriptions.Remove(publicKey);
            }
        }

        private void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(900000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private async void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (_subscriptions.Count > 0)
            {
                foreach (var sub in _subscriptions)
                {
                    var values = sub.Value.Split("|");
                    var derivate = (Derivate)int.Parse(values[0]);
                    var listenKey = values[1];
                    switch (derivate)
                    {
                        case Derivate.Futures:
                            var listenKeyResponse = await _client.UsdFuturesApi.Account.KeepAliveUserStreamAsync(listenKey);
                            if (!listenKeyResponse.Success)
                            {
                                var updatedListenKey = _client.UsdFuturesApi.Account.StartUserStreamAsync().Result.Data;
                                var updatedValue = string.Join("|", values[0], updatedListenKey, values[2]);
                                _subscriptions[sub.Key] = updatedValue;
                            }
                            break;
                    }
                }
            }
        }
    }
}