using System;
using System.Collections.Generic;

namespace Stugo
{
    public class SafeEventManager<TMessage> : ISafeEventManager<TMessage>
    {
        private readonly object handlerLock = new object();
        private readonly List<IEventHandler<TMessage>> handlers = new List<IEventHandler<TMessage>>();


        public void AddHandler<TTarget>(TTarget target, Func<TTarget, Action<TMessage>> handlerSelector)
        {
            lock (handlerLock)
            {
                handlers.Add(new EventHandler<TTarget, TMessage>(target, handlerSelector));
                Purge();
            }
        }


        public void RemoveHandler(Action<TMessage> handler)
        {
            lock (handlerLock)
            {
                for (var i = handlers.Count - 1; i >= 0; --i)
                {
                    if (!handlers[i].IsAlive || handlers[i].Target == handler.Target 
                        && handlers[i].Method == handler.Method)
                    {
                        handlers.RemoveAt(i);
                    }
                }
            }
        }


        public void Invoke(TMessage message)
        {
            bool allAlive = true;

            lock (handlerLock)
            {
                foreach (var h in handlers)
                    allAlive &= h.Invoke(message);

                if (!allAlive)
                    Purge();
            }
        }


        private void Purge()
        {
            lock (handlerLock)
            {
                for (var i = handlers.Count - 1; i >= 0; --i)
                {
                    if (!handlers[i].IsAlive)
                        handlers.RemoveAt(i);
                }
            }
        }
    }
}
