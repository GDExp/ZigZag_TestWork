using UnityEngine;
using ZigZag.GameCore;

namespace ZigZag.Gameplay
{
    sealed class ZigZagGameField : BaseGameField
    {
        private GameController _gameController;
        private Rigidbody _lastGroundTitle;
        private ZigZagObjectPool _pool;

        private float _gameSpeed;
        private float _groundTitleOffset;
        private float _crystalChance;

        private int _startTitleHash;
        private int _fixGroundTitleHash;

        public ZigZagGameField(GameController gameController, GameSetup gameSetup) : base(gameController)
        {
            _gameController = gameController;
            gameSetup.GameControlTrigger.SetupGameControlTrigger(TriggerAction);
            _pool = new ZigZagObjectPool(gameSetup);
            _gameSpeed = gameSetup.CurrentGameSetup.gameSpeed;
            _groundTitleOffset = gameSetup.CurrentGameSetup.groundPrefab.transform.localScale.x;
            _crystalChance = gameSetup.CurrentGameSetup.crystalChance;

            generator = new ZigZagGenerator(gameSetup.cameraLimitValue);
            var gen = generator as ZigZagGenerator;

            BuildGameFieldOnStart();
        }

        public override void BuildGameFieldOnStart()
        {
            CreateStartField();
            for (int i = 0; i < 50; ++i) BuildGameField();
            //bug fix
            if (_fixGroundTitleHash == 0) return;
            _pool.ReturnObjectInPool(GameObjectEnum.GroundTitle, groundTitles[_fixGroundTitleHash]);
            groundTitles.Remove(_fixGroundTitleHash);
            _fixGroundTitleHash = 0;
        }

        public override void MoveObjectsInGameField()
        {
            MoveGroundTitels();
            MoveCrystalTitles();
        }

        private void MoveGroundTitels()
        {
            foreach(var el in groundTitles)
            {
                el.Value.position += GetMoveDirection();
            }
        }

        private void MoveCrystalTitles()
        {
            foreach(var el in crystalTitles)
            {
                el.Value.position += GetMoveDirection();
            }
        }

        private Vector3 GetMoveDirection()
        {
            return Vector3.forward * _gameSpeed * Time.deltaTime;
        }

        public override void TriggerAction(int objectHash)
        {
            if (!_gameController.IsGameRun) return;
            GameObjectEnum objectType = GameObjectEnum.GroundTitle;
            Rigidbody objectInTrigger;

            if (groundTitles.TryGetValue(objectHash, out objectInTrigger))
            {
                if(_startTitleHash != 0 && objectHash == _startTitleHash)
                {
                    groundTitles[_startTitleHash].transform.localScale -= (Vector3.right * 2 + Vector3.forward * 2) * _groundTitleOffset;
                    _startTitleHash = 0;
                }
                groundTitles.Remove(objectHash);
                BuildGameField();
            }
            else
            {
                if (!crystalTitles.TryGetValue(objectHash, out objectInTrigger)) return;
                objectType = GameObjectEnum.CrystaleTitle;
                crystalTitles.Remove(objectHash);
            }

            _pool.ReturnObjectInPool(objectType, objectInTrigger);
        }

        public override void RuntimeCreateGroundTitle()
        {
            Vector3 createPoint = Vector3.zero;

            var titleObj = _pool.GetObjectInPool(GameObjectEnum.GroundTitle);

            if (_lastGroundTitle is null)
            {
                _fixGroundTitleHash = titleObj.gameObject.GetHashCode();
            }
            else
            {
                createPoint = (generator.GetNextValueInQueue() == 0) ? _lastGroundTitle.position - _lastGroundTitle.transform.right * _groundTitleOffset
                                                                    : _lastGroundTitle.position - _lastGroundTitle.transform.forward * _groundTitleOffset;
            }
            
            titleObj.position = createPoint;
            _lastGroundTitle = titleObj;

            groundTitles.Add(titleObj.gameObject.GetHashCode(), titleObj);
            
        }

        public override void RuntimeCreateCrystalTitle()
        {
            if (_lastGroundTitle is null || !generator.CheckChanceByWeight(_crystalChance)) return;
            var crystal = _pool.GetObjectInPool(GameObjectEnum.CrystaleTitle);
            crystal.position = _lastGroundTitle.position + Vector3.up * 2.8f;//float - offset

            crystalTitles.Add(crystal.gameObject.GetHashCode(), crystal);
        }

        private void CreateStartField()
        {
            var title = _pool.GetObjectInPool(GameObjectEnum.GroundTitle);
            title.transform.localScale += (Vector3.right * 2 + Vector3.forward * 2) * _groundTitleOffset;
            title.position = Vector3.zero;
            title.position += Vector3.forward * 2.45f;

            _startTitleHash = title.gameObject.GetHashCode();
            groundTitles.Add(_startTitleHash, title);
        }

        public override void RestartGameField()
        {
            if(_startTitleHash != 0)
            {
                groundTitles[_startTitleHash].transform.localScale -= (Vector3.right * 2 + Vector3.forward * 2) * _groundTitleOffset;
                _startTitleHash = 0;
            }

            foreach (var ground in groundTitles)
            {
                _pool.ReturnObjectInPool(GameObjectEnum.GroundTitle, ground.Value);
            }
            groundTitles.Clear();

            foreach (var crystal in crystalTitles)
            {
                _pool.ReturnObjectInPool(GameObjectEnum.CrystaleTitle, crystal.Value);
            }
            crystalTitles.Clear();

            _lastGroundTitle = null;
            BuildGameFieldOnStart();
        }
    }
}
