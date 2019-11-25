using UnityEngine;
using ZigZag.GameCore;

namespace ZigZag.Gameplay
{
    sealed class SparksPool : BaseObjectsPool<ParticleSystem, int>
    {
        private ParticleSystem _crystalSparkPrefab;

        public SparksPool(BaseGameSetupSO setupSO) : base()
        {
            _crystalSparkPrefab = setupSO.sparkPrefab;
        }

        public override void ActiveObjectInScene(ParticleSystem poolObject)
        {
            throw new System.NotImplementedException();
        }

        public override ParticleSystem CreateNewObject(int key)
        {
            throw new System.NotImplementedException();
        }

        public override void HideObjectInScene(ParticleSystem poolObject)
        {
            throw new System.NotImplementedException();
        }
    }
}
