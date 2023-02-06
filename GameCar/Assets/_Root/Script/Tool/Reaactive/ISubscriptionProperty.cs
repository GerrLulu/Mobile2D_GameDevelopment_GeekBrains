using System;

namespace GameCarTool
{
    internal interface ISubscriptionProperty<out TValue>
    {
        TValue Value { get; }
        void SubscribeOnChange(Action<TValue> subscriptionAction);
        void UnSubscribeOnChange(Action<TValue> unsubscriptionAction);
    }
}
