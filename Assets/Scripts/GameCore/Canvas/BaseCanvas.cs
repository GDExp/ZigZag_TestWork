using UnityEngine;
using UnityEngine.UI;

namespace ZigZag.GameCore
{
    public abstract class BaseCanvas : MonoBehaviour
    {
        [SerializeField] protected Text scoreText;
        [SerializeField] protected Text tapToPlayText;

        private int _score;

        public abstract void SetupCanvas();
        public abstract void ShowTapToPlayText();

        public virtual void ResetCanvasText()
        {
            _score = 0;
            scoreText.text = _score.ToString();
        }

        public virtual void AddScoreInText()
        {
            _score++;
            scoreText.text = _score.ToString();
        }
    }
}
