using ZigZag.GameCore.GameInterface;

namespace ZigZag.GameCore
{
    /// <summary>
    /// Use Observer Pattern, this class is Subject
    /// </summary>
    abstract class BaseInputModule : Observer, IUpdatableObject
    {
        public void OnUpdate()
        {
            InputGameEvent();
        }

        public abstract void InputGameEvent();        
    }
}
