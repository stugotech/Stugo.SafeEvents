namespace Stugo
{
    public interface ISafeEventManager<TMessage> : ISafeEvent<TMessage>
    {
        void Invoke(TMessage message);
    }
}
