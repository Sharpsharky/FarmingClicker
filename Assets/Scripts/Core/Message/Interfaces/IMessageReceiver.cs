namespace Core.Message.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IMessageReceiver
    {
        List<Type> ListenedTypes { get; }
        void OnMessageReceived(object message);
    }
}