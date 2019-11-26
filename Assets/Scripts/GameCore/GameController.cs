using System.Collections.Generic;
using UnityEngine;
using ZigZag.GameCore.GameInterface;
using ZigZag.Gameplay;


namespace ZigZag.GameCore
{
    public sealed class GameController : MonoBehaviour, IObserver
    {
        public static GameController Instance { get { return _instance; } }
        private static GameController _instance;

        private BaseGameField _gameField;
        private BaseInputModule _currentBaseModule;
        private BaseCanvas _gameCanvas;
        private SparkFXModule _fxModule;
        private Player _player;
        
        private GameSetup _currentGameSetup;
        private IList<IUpdatableObject> _updatableObectsList;

        private float _gameSpeed;

        private bool _isRestart;
        private bool _isGameRun;
        private bool loadNewLevel;

        public BaseCanvas GameCanvas { get => _gameCanvas; }
        public float GameSpeed { get => _gameSpeed; }
        public bool IsGameRun { get => _isGameRun; }

        public void SubscribeUpdatableObject(IUpdatableObject updatableObject)
        {
            if (_updatableObectsList.Contains(updatableObject)) return;
            _updatableObectsList.Add(updatableObject);
        }

        public void GameEnd()
        {
            _isRestart = true;
            _isGameRun = false;
            _gameCanvas.ShowTapToPlayText();
        }

        private void Awake()
        {
            if (Instance is null) _instance = this;
            else Destroy(gameObject);
            DontDestroyOnLoad(gameObject);

            _updatableObectsList = new List<IUpdatableObject>();

            FindCurrentGameSetup();
            _gameSpeed = _currentGameSetup.CurrentGameSetup.gameSpeed;
        }

        private void FindCurrentGameSetup()
        {
            _currentGameSetup = FindObjectOfType<GameSetup>();
            if (_currentGameSetup is null) Debug.LogError($"На сцене нет объекта со скриптом {typeof(GameSetup)} ");
        }

        private void Start()
        {
            CreateGameFiled();
            CreateInputModule();
            SetupPlayerObject();
            CreateFXModule();
            SetupGameCanvas();
        }

        private void CreateGameFiled()
        {
            _gameField = new ZigZagGameField(this, _currentGameSetup);
        }

        private void CreateInputModule()
        {
            _currentBaseModule = new MouseButtonInputModule();
            //_currentBaseModule = new SpaceInputModule();
            (_currentBaseModule as ISubject).Attach(this);
        }
        
        private void SetupPlayerObject()
        {
            _player = FindObjectOfType<Player>();
            if (_player is null) Debug.LogError($"На сцене нет объекта со скриптом {typeof(Player)}");
            _player.PlayerSetup(this, _gameField);
            _player.RestartPlayerPosition(_currentGameSetup.StartPlayerPoint);
            (_currentBaseModule as ISubject).Attach(_player);
        }

        private void CreateFXModule()
        {
            _fxModule = new SparkFXModule(this, _player, _currentGameSetup);
        }

        private void SetupGameCanvas()
        {
            _gameCanvas = FindObjectOfType<BaseCanvas>();
            if (_gameCanvas is null) Debug.LogError($"На сцене нет объекта со скриптом {typeof(BaseCanvas)}");
            _gameCanvas.ResetCanvasText();
        }

        private void Update()
        {
            _currentBaseModule.OnUpdate();
        }

        private void FixedUpdate()
        {
            if (!_isGameRun) return;
            for (int i = 0; i < _updatableObectsList.Count; ++i) _updatableObectsList[i].OnUpdate();
        }

        public void UpdateObserver()
        {
            if (loadNewLevel)
            {
                _isGameRun = true;
                loadNewLevel = false;
                _gameCanvas.ResetCanvasText();
                _gameCanvas.ShowTapToPlayText();
            }
            if (_isGameRun) return;
            _isGameRun = true;
            _player.RestartPlayerPosition(_currentGameSetup.StartPlayerPoint);
            _gameCanvas.ShowTapToPlayText();
            if (!_isRestart) return;
            _isRestart = false;
            _gameField.RestartGameField();
            loadNewLevel = true;
        }
    }
}
