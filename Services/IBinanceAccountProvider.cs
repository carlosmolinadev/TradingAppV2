using System;
using Binance.Net.Objects.Models.Futures.Socket;
using TradingAppMvc.Domain.Enums;
using TradingAppMvc.Services;

namespace TradingAppMvc.Services
{
    public interface IBinanceAccountProvider
    {
        event EventHandler<BinanceFuturesStreamOrderUpdate> OnRaiseUpdateOrder;
        Task Start(string privateKey, string publicKey, Derivate derivate);
        Task Stop(string publicKey);
    }
}