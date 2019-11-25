using UnityEngine;
using ZigZag.GameCore;

namespace ZigZag.Gameplay
{
    public sealed class ZigZagGenerator : BaseGenerator
    {
        private const int MaxRepeatValueCount = 5;
        private const int LeftValue = 1;
        private const int RightValue = 0;

        private int generatorCount = 15;
        private int lastRandomValue;
        private readonly int limitBySide;

        public ZigZagGenerator(float limit) : base()
        {
            limitBySide = Mathf.RoundToInt(limit);
        }

        public override void FillGeneratorNewValue()
        {
            int rightCount = 0;
            int leftCount = 0;
            

            for(int i = 0; i < generatorCount; ++i)
            {
                WriteRandomValueInSideBalance(generatorRandom.Next(0, 2));
                CheckRandomValueByLimit();
            }

            void WriteRandomValueInSideBalance(int checkValue)
            {
                if(checkValue == LeftValue)
                {
                    leftCount++;
                    rightCount--;
                }
                else
                {
                    leftCount--;
                    rightCount++;
                }

                generatorQueueTitleValue.Enqueue(checkValue);
            }

            void CheckRandomValueByLimit()
            {
                if (leftCount >= limitBySide)
                {
                    WriteInversValueInQueue(RightValue);
                }
                if (rightCount >= limitBySide)
                {
                    WriteInversValueInQueue(LeftValue);
                }
            }

            void WriteInversValueInQueue(int inversValue)
            {
                int count = limitBySide;
                while(count > 0)
                {
                    WriteRandomValueInSideBalance(inversValue);
                    count--;
                }
            }
        }

        public override bool CheckChanceByWeight(float weight)
        {
            var randomValue = Random.Range(0f, 100f);

            bool result = (randomValue - weight) < 0;
            if (result) currentWeight = 0f;

            return result;
        }
    }
}
