using UnityEngine;
using ZigZag.GameCore;

namespace ZigZag.Gameplay
{
    sealed class SpaceInputModule : BaseInputModule
    {
        public override void InputGameEvent()
        {
            if (Input.GetKeyDown(KeyCode.Space)) Notify();
        }
    }
}
