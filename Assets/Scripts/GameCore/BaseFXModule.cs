using System.Collections.Generic;
using UnityEngine;
using ZigZag.GameCore.GameInterface;

namespace ZigZag.GameCore
{
    /// <summary>
    /// Use particle system in game - IUpdatableObject
    /// </summary>
    public abstract class BaseFXModule<FXType, FXKey> : IUpdatableObject
        where FXType: class
    {
        private FXKey _fxKey;
        private List<FXType> _activeParticlesInScene;
        private BaseObjectsPool<FXType, FXKey> _fxPool;

        public BaseObjectsPool<FXType, FXKey> FxPool { get => _fxPool; set => SetFxPool(value); }
        public FXKey FxKey { get => _fxKey; set => SetFxKey(value); }

        public BaseFXModule(GameController gameController)
        {
            _activeParticlesInScene = new List<FXType>();
            gameController.SubscribeUpdatableObject(this);
        }

        public void OnUpdate()
        {
            CheckActiveParticleInList();
        }

        public abstract void InvokeFXInPoint(Vector3 point);
        public abstract bool CheckCurrentFX(FXType objectFX);
        
        protected virtual FXType GetFX()
        {
            FXType objectFX = _fxPool.GetObjectInPool(_fxKey);
            _activeParticlesInScene.Add(objectFX);
            return objectFX;
        }

        protected virtual void CheckActiveParticleInList()
        {
            if (_activeParticlesInScene.Count == 0) return;
            for(int i = 0; i < _activeParticlesInScene.Count; ++i)
            {
                if (CheckCurrentFX(_activeParticlesInScene[i]))
                {
                    _fxPool.ReturnObjectInPool(_fxKey, _activeParticlesInScene[i]);
                    _activeParticlesInScene.Remove(_activeParticlesInScene[i]);
                }
            }
        }

        protected virtual void SetFxPool(BaseObjectsPool<FXType, FXKey> pool)
        {
            _fxPool = pool;
        }

        protected virtual void SetFxKey(FXKey key)
        {
            _fxKey = key;
        }
    }
}
