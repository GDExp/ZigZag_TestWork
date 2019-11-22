using ZigZag.GameCore;

namespace ZigZag.Gameplay
{
    public sealed class ZigZagGenerator : BaseGenerator
    {
        private const int MaxRepeatValueCount = 4;

        private int generatorCount = 15;
        private int lastRandomValue;

        public ZigZagGenerator() : base()
        {

        }

        public override void FillGeneratorNewValue()
        {
            int value;
            int reapeatCount = 0;
            for (int i = 0; i < generatorCount; ++i)
            {
                value = generatorRandom.Next(0, 2);
                if (i == 0) lastRandomValue = value;
                else
                {
                    if (lastRandomValue == value) reapeatCount++;
                    else reapeatCount = 0;
                }
                lastRandomValue = value;
                if(reapeatCount >= MaxRepeatValueCount)
                {
                    --i;
                    continue;
                } 
                generatorQueueTitleValue.Enqueue(value);
            }
        }
    }
}
