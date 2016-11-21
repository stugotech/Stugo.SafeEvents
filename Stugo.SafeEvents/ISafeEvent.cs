using System;

namespace Stugo
{
    public interface ISafeEvent<out TMessage>
    {
        void AddHandler<TTarget>(TTarget target, Func<TTarget, Action<TMessage>> handlerSelector);
        void RemoveHandler(Action<TMessage> handler);
    }
}
