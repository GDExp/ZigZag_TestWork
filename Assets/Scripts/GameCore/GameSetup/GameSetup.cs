using UnityEngine;
using ZigZag.Gameplay;
using ZigZag.GameCore.GameInterface;

namespace ZigZag.GameCore
{
    [AddComponentMenu("ZigZag/GameSetup")]
    public sealed class GameSetup : MonoBehaviour
    {
        [SerializeField] private GameControlTrigger _gameControlTrigger;
        [SerializeField] private BaseGameSetupSO _currentGameSetup;
        [SerializeField] private Vector3 _startPoint;
        public BaseGameSetupSO CurrentGameSetup { get => _currentGameSetup; }
        public IGameControlTrigger GameControlTrigger { get => _gameControlTrigger as IGameControlTrigger; }
        public Vector3 StartPoint { get => _startPoint; }
    }
}
