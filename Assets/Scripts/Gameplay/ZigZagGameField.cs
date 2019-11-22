using UnityEngine;
using ZigZag.GameCore;

namespace ZigZag.Gameplay
{
    sealed class ZigZagGameField : BaseGameField
    {
        private Vector3 _startPoint;
        private Rigidbody _lastGroundTitle;
        private ZigZagObjectPool _pool;

        public ZigZagGameField(GameSetup gameSetup) : base()
        {
            //gameSetup.GameControlTrigger.SetupGameControlTrigger(TriggerAction);
            _startPoint = gameSetup.StartPoint;
            _pool = new ZigZagObjectPool(gameSetup);
            generator = new ZigZagGenerator();
            var gen = generator as ZigZagGenerator;
            for(int i = 0; i < 100; ++i) BuildGameField();
        }

        public override void BuildGameField()
        {
            Vector3 createPoint = Vector3.zero;
            if (_lastGroundTitle is null)
            {
                createPoint = _startPoint;
            }
            else
            {
                createPoint = (generator.GetNextValueInQueue() == 0) ? _lastGroundTitle.position - _lastGroundTitle.transform.right * 1f
                                                                    : _lastGroundTitle.position - _lastGroundTitle.transform.forward * 1f;
            }
            var titleObj = _pool.GetObjectInPool(GameTitle.GroundTitle);
            titleObj.position = createPoint;
            _lastGroundTitle = titleObj;
        }

        public override void MoveObjectsInGameField()
        {

        }

        public override void TriggerAction(int objectHash, GameTitle title)
        {

        }
    }
}
