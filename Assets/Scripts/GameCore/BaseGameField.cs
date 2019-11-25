using System.Collections.Generic;
using UnityEngine;
using ZigZag.GameCore.GameInterface;

namespace ZigZag.GameCore
{
    public abstract class BaseGameField : IUpdatableObject
    {
        protected Dictionary<int, Rigidbody> groundTitles;
        protected Dictionary<int, Rigidbody> crystalTitles;
        protected BaseGenerator generator;
        
        public BaseGameField(GameController gameController)
        {
            groundTitles = new Dictionary<int, Rigidbody>();
            crystalTitles = new Dictionary<int, Rigidbody>();
            gameController.SubscribeUpdatableObject(this);
        }

        public abstract void BuildGameFieldOnStart();
        public abstract void MoveObjectsInGameField();
        public abstract void TriggerAction(int objectHash);
        public abstract void RuntimeCreateGroundTitle();
        public abstract void RuntimeCreateCrystalTitle();
        public abstract void RestartGameField();

        protected virtual void BuildGameField()
        {
            this.RuntimeCreateCrystalTitle();
            this.RuntimeCreateGroundTitle();
        }

        public void OnUpdate()
        {
            MoveObjectsInGameField();
        }
    }
}
