using System;
using UnityEngine;
using ZigZag.GameCore;
using ZigZag.GameCore.GameInterface;

namespace ZigZag.Gameplay
{
    [AddComponentMenu("ZigZag/Player")]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public sealed class Player : MonoBehaviour, IUpdatableObject, IObserver
    {
        private const string DeadTriggerTag = "Dead Trigger";
        private GameController _gameController;
        private Rigidbody _playerRB;

        private event Action<int> TriggerAction;
        public event Action<Vector3> FXTriggerAction;

        private float _gameSpeed;
        private sbyte _side = 1;

        public void PlayerSetup(GameController gameController, BaseGameField gameField)
        {
            _playerRB = GetComponent<Rigidbody>();

            _gameController = gameController;
            _gameController.SubscribeUpdatableObject(this);
            _gameSpeed = gameController.GameSpeed;

            TriggerAction += gameField.TriggerAction;
        }

        public void RestartPlayerPosition(Vector3 startPosition)
        {
            _playerRB.position = startPosition;
            _playerRB.velocity = Vector3.zero;
            _playerRB.angularVelocity = Vector3.zero;
        }

        public void OnUpdate()
        {
            OnCheckGroundTitle();
            if (!_gameController.IsGameRun) return;
            PlayerMovement();
        }

        public void UpdateObserver()
        {
           _side *= -1;
        }

        private void PlayerMovement()
        {
            _playerRB.velocity = Vector3.right * _side * _gameSpeed;
        }

        private void OnCheckGroundTitle()
        {
            if (Physics.Raycast(_playerRB.transform.position, Vector3.down, 4f)) return;
            _gameController.GameEnd();
        }

        private void OnTriggerEnter(Collider other)
        {
            TriggerAction?.Invoke(other.gameObject.GetHashCode());
            FXTriggerAction?.Invoke(other.transform.position);
            _gameController.GameCanvas.AddScoreInText();
        }
    }
}
