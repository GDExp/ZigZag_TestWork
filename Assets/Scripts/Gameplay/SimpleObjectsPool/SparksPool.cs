using UnityEngine;
using ZigZag.GameCore;

namespace ZigZag.Gameplay
{
    sealed class SparksPool : BaseObjectsPool<ParticleSystem, GameObjectEnum>
    {
        private ParticleSystem _crystalSparkPrefab;

        public SparksPool(GameSetup setup) : base()
        {
            _crystalSparkPrefab = setup.CurrentGameSetup.sparkPrefab;
        }

        public override void ActiveObjectInScene(ParticleSystem poolObject)
        {
        }

        public override ParticleSystem CreateNewObject(GameObjectEnum key)
        {
            return MonoBehaviour.Instantiate(_crystalSparkPrefab);
        }

        public override void HideObjectInScene(ParticleSystem poolObject)
        {
            poolObject.transform.position = Vector3.one * 888f;
        }
    }
}
