﻿using UnityEngine;

namespace ZigZag.GameCore
{
    sealed class GameInitializer
    {
        private const string PrimeObjectName = "GameController";

        [RuntimeInitializeOnLoadMethod]
        public static void NewGameInitialize()
        {
            GameObject gameController = new GameObject(PrimeObjectName);
            gameController.AddComponent<GameController>();
        }
    }
}
