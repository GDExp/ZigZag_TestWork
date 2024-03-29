﻿using System;
using System.Collections.Generic;

namespace ZigZag.GameCore
{
    public abstract class BaseGenerator
    {
        protected Queue<int> generatorQueueTitleValue;
        protected Random generatorRandom;
        
        protected float currentWeight;

        public BaseGenerator()
        {
            generatorQueueTitleValue = new Queue<int>();
            generatorRandom = new Random();
        }

        public virtual int GetNextValueInQueue()
        {
            if (generatorQueueTitleValue.Count == 0) FillGeneratorNewValue();

            return generatorQueueTitleValue.Dequeue();
        }

        public abstract void FillGeneratorNewValue();
        public abstract bool CheckChanceByWeight(float weight);
    }
}
