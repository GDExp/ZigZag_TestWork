using System.Collections.Generic;
using UnityEngine;
using ZigZag.GameCore.GameInterface;

namespace ZigZag.GameCore
{
    abstract class BaseGameField : IUpdatableObject
    {
        protected Dictionary<int, Rigidbody> groundTitles;
        protected Dictionary<int, Rigidbody> crystalTitles;
        protected BaseGenerator generator;

        public abstract void MoveObjectsInGameField();
        public abstract void TriggerAction(int objectHash, GameTitle title);
        public abstract void BuildGameField();

        public BaseGameField()
        {
            groundTitles = new Dictionary<int, Rigidbody>();
            crystalTitles = new Dictionary<int, Rigidbody>();
        }

        public void OnUpdate()
        {
            MoveObjectsInGameField();
        }
    }
}
