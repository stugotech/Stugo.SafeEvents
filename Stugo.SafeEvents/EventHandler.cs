using System;
using System.Reflection;

namespace Stugo
{
    internal struct EventHandler<TTarget, TMessage> : IEventHandler<TMessage>
    {
        private readonly WeakReference target;
        private readonly Func<TTarget, Action<TMessage>> handler;


        public bool IsAlive => target.IsAlive;
        public MethodInfo Method { get; }
        public object Target => target.Target;


        public EventHandler(TTarget target, Func<TTarget, Action<TMessage>> handler)
        {
            this.target = new WeakReference(target);
            this.handler = handler;
            Method = handler(target).Method;
        }


        public bool Invoke(TMessage message)
        {
            var obj = (TTarget)target.Target;
            if (obj != null)
                handler(obj)(message);
            return obj != null;
        }
    }
}
