using System;
using UnityEngine;
using ZigZag.GameCore;
using ZigZag.GameCore.GameInterface;


namespace ZigZag.Gameplay
{
    public sealed class GameControlTrigger : MonoBehaviour, IGameControlTrigger
    {
        private event Action<int> TriggerAction; 

        public void SetupGameControlTrigger( Action<int> action)
        {
            TriggerAction += action;
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerAction?.Invoke(other.gameObject.GetHashCode());
        }
    }
}
