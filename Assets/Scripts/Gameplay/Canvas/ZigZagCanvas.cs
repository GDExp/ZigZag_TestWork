using UnityEngine;
using ZigZag.GameCore;

namespace ZigZag.Gameplay
{
    [AddComponentMenu("ZigZag/GameCanvas")]
    public sealed class ZigZagCanvas : BaseCanvas
    {
        public override void SetupCanvas()
        {
            
        }

        public override void ShowTapToPlayText()
        {
            tapToPlayText.enabled = !tapToPlayText.enabled;
        }
    }
}
