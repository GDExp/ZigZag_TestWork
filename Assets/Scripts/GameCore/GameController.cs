using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZigZag.GameCore.GameInterface;
using ZigZag.Gameplay;


namespace ZigZag.GameCore
{
    public sealed class GameController : MonoBehaviour
    {
        public static GameController Instance { get; }
        private GameController _instance;
        
        private GameSetup _currentGameSetup;
        private IList<IUpdatableObject> _updatableObectsList;

        private void Awake()
        {
            if (Instance is null) _instance = this;
            else Destroy(gameObject);
            DontDestroyOnLoad(gameObject);

            _updatableObectsList = new List<IUpdatableObject>();

            FindCurrentGameSetup();
        }

        private void FindCurrentGameSetup()
        {
            _currentGameSetup = FindObjectOfType<GameSetup>();
            if(_currentGameSetup is null) Debug.LogError("Необходим объект с настройками на сцене!");
        }

        private void Start()
        {
            CreateGameFiled();
        }

        private void CreateGameFiled()
        {
            IUpdatableObject gameField = new ZigZagGameField(_currentGameSetup);
            _updatableObectsList.Add(gameField);
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _updatableObectsList.Count; ++i) _updatableObectsList[i].OnUpdate();
        }
    }
}
