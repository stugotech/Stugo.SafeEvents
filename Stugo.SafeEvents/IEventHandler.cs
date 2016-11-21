using System.Reflection;

namespace Stugo
{
    internal interface IEventHandler<in TMessage>
    {
        bool Invoke(TMessage message);
        bool IsAlive { get; }
        MethodInfo Method { get; }
        object Target { get; }
    }
}
