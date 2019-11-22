using System.Collections.Generic;
using UnityEngine;

namespace ZigZag.GameCore
{
    class ZigZagObjectPool : BaseObjectsPool<Rigidbody, GameTitle>
    {
        private IDictionary<GameTitle, Rigidbody> _gamePoolPrefabs;

        public ZigZagObjectPool(GameSetup gameSetup) : base()
        {
            //it is sooooo horrible
            //TO DO remove this later!
            _gamePoolPrefabs = new Dictionary<GameTitle, Rigidbody>();
            _gamePoolPrefabs.Add(GameTitle.GroundTitle, gameSetup.CurrentGameSetup.groundPrefab);
            _gamePoolPrefabs.Add(GameTitle.CristaleTitle, gameSetup.CurrentGameSetup.crystalPrefab);
        }

        public override Rigidbody CreateNewObject(GameTitle key)
        {
            return MonoBehaviour.Instantiate(_gamePoolPrefabs[key]);
        }
    }
}
