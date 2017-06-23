﻿using System;
using System.Threading.Tasks;

namespace LagoVista.Core.Networking.Interfaces
{
    public interface IMQTTClientBase : IDisposable
    {
        event EventHandler<IMQTTAppStatusReceivedEventArgs> AppStatusReceived;
        event EventHandler<IMQTTCommandEventArgs> CommandReceived;
        event EventHandler<IMQTTEventReceivedEventArgs> EventReceived;
        event EventHandler<IMQTTEventDeviceStatusReceivedEventArgs> DeviceStatusReceived;

        event EventHandler<bool> ConnectionStateChanged;

        String BrokerHostName { get; set; }
        int BrokerPort { get; set; }

        String ClientId { get; }

        bool IsConnected { get; }

        Task<ConnAck> ConnectAsync();

        void Disconnect();

        bool ShowDiagnostics { get; set; }
    }

}