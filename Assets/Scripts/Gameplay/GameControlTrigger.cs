using System;
using UnityEngine;
using ZigZag.GameCore.GameInterface;


namespace ZigZag.Gameplay
{
    public sealed class GameControlTrigger : MonoBehaviour, IGameControlTrigger
    {
        private event Action<int, GameTitle> TriggerAction; 

        public void SetupGameControlTrigger( Action<int, GameTitle> action)
        {
            TriggerAction += action;
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerAction?.Invoke(other.GetHashCode(),GameTitle.GroundTitle);
        }
    }
}
