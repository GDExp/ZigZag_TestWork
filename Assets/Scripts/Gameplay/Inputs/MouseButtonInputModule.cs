using UnityEngine;
using ZigZag.GameCore;

namespace ZigZag.Gameplay
{
    sealed class MouseButtonInputModule : BaseInputModule
    {
        public override void InputGameEvent()
        {
            if (Input.GetMouseButtonDown(0)) Notify();
        }
    }
}
