using UnityEngine;

namespace ZigZag.GameCore
{
    sealed class GameInitializer
    {
        private const string PrimeObjectName = "GameController";

        [RuntimeInitializeOnLoadMethod]
        public static void NewGameInitialize()
        {
            UnityEngine.GameObject gameController = new UnityEngine.GameObject(PrimeObjectName);
            gameController.AddComponent<GameController>();
        }
    }
}
