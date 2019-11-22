using System;

namespace ZigZag.GameCore.GameInterface
{
    public interface IGameControlTrigger
    {
        void SetupGameControlTrigger(Action<int, GameTitle> action);

    }
}
