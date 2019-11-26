using System.Collections.Generic;
using UnityEngine;
using ZigZag.GameCore;

namespace ZigZag.Gameplay
{
    sealed class ZigZagObjectPool : BaseObjectsPool<Rigidbody, GameObjectEnum>
    {
        private IDictionary<GameObjectEnum, Rigidbody> _gamePoolPrefabs;

        public ZigZagObjectPool(GameSetup gameSetup) : base()
        {
            //it is sooooo horrible
            //TO DO remove this later!
            _gamePoolPrefabs = new Dictionary<GameObjectEnum, Rigidbody>();
            _gamePoolPrefabs.Add(GameObjectEnum.GroundTitle, gameSetup.CurrentGameSetup.groundPrefab);
            _gamePoolPrefabs.Add(GameObjectEnum.CrystaleTitle, gameSetup.CurrentGameSetup.crystalPrefab);
        }
                
        public override Rigidbody CreateNewObject(GameObjectEnum key)
        {
            return MonoBehaviour.Instantiate(_gamePoolPrefabs[key]);
        }

        public override void ActiveObjectInScene(Rigidbody poolObject)
        {

        }

        public override void HideObjectInScene(Rigidbody poolObject)
        {
            poolObject.position = Vector3.one * 999f;
        }
        
    }
}
