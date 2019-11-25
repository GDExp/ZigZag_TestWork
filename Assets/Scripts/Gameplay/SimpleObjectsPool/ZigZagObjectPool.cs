using System.Collections.Generic;
using UnityEngine;
using ZigZag.GameCore;

namespace ZigZag.Gameplay
{
    sealed class ZigZagObjectPool : BaseObjectsPool<Rigidbody, GameTitle>
    {
        private IDictionary<GameTitle, Rigidbody> _gamePoolPrefabs;

        public ZigZagObjectPool(GameSetup gameSetup) : base()
        {
            //it is sooooo horrible
            //TO DO remove this later!
            _gamePoolPrefabs = new Dictionary<GameTitle, Rigidbody>();
            _gamePoolPrefabs.Add(GameTitle.GroundTitle, gameSetup.CurrentGameSetup.groundPrefab);
            _gamePoolPrefabs.Add(GameTitle.CrystaleTitle, gameSetup.CurrentGameSetup.crystalPrefab);
        }
                
        public override Rigidbody CreateNewObject(GameTitle key)
        {
            return MonoBehaviour.Instantiate(_gamePoolPrefabs[key]);
        }

        public override void ActiveObjectInScene(Rigidbody poolObject)
        {
            poolObject.gameObject.SetActive(true);
        }

        public override void HideObjectInScene(Rigidbody poolObject)
        {
            poolObject.position = Vector3.one * 999f;
            //poolObject.gameObject.SetActive(false);
        }
        
    }
}
