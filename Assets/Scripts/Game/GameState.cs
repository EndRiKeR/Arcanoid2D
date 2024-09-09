using Arkanoid2D.Configs;
using Arkanoid2D.PrefabScripts;
using UnityEngine;

namespace Arkanoid2D.Game
{
    public class GameState : MonoBehaviour
    {
        [Header("Services")]
        [SerializeField]
        private HealthService _healthService;
        
        [Space]
        [Header("Configs")]
        [SerializeField]
        private GameConfig _gameConfig;
        
        [SerializeField]
        private SpriteConfig _spritesConfig;
        
        [SerializeField]
        private BlocksPrefabsConfig _blocksPrefabsConfig;
        
        [Space]
        [Header("Base Objects")]
        [SerializeField]
        private Carriage _carriage;

        [SerializeField]
        private Ball _ball;

        [SerializeField]
        private Collider2D _field;

        [SerializeField]
        private GameEndScreenView _gameEndScreenView;
        
        [SerializeField]
        private GameInitScreenView _gameInitScreenView;

        [Space]
        [Header("Flags")]
        public bool IsNeedInit = true;
        public bool IsBallOnCarriage = true;
        
        void Awake()
        {
            _gameEndScreenView.Hide();
            _gameInitScreenView.Show();
            
            _ball.DeadBall += BallDead;
            
            _healthService.OnGainHealth += _carriage.UpdateHeartIcon;
            _healthService.OnLoseHealth += _carriage.UpdateHeartIcon;
            
            _gameEndScreenView.Button.onClick.AddListener(RestartGame);
        }

        void FixedUpdate()
        {
            if (IsNeedInit)
                InitGame();

            UpdateGame();

            if (CheckGameEnd())
                EndGame();
        }

        private bool CheckGameEnd()
        {
            bool IsHealthRunOut = _healthService.Health == 0;
            bool IsAllBlocksDestroyed = _field.GetComponentsInChildren<Block>().Length == 0;
            return IsHealthRunOut || IsAllBlocksDestroyed;
        }

        private void InitGame()
        {
            IsNeedInit = false;

            _healthService.GainHealth(_gameConfig.PlayerHealth);

            _ball.transform.position = _carriage.BallPosition;
            _ball.Angle = _gameConfig.DefBallAngle;

            var points = _field.GetComponentsInChildren<SpawnPoint>();
            foreach (var point in points)
            {
                Instantiate(_blocksPrefabsConfig.GetRandomPrefab(), point.transform);
            }
            
            var blocks = _field.GetComponentsInChildren<Block>();
            foreach (var block in blocks)
            {
                block.InitBlock(_spritesConfig);
            }
        }
    
        private void UpdateGame()
        {
            UpdateCarriagePosition();
            UpdateBallPosition();
        }

        private void EndGame()
        {
            if (_healthService.Health == 0)
                _gameEndScreenView.Show("DEFEAT");
            else
                _gameEndScreenView.Show("WIN!");
            
            IsBallOnCarriage = true;
            _ball.transform.position = _carriage.BallPosition;
            _ball.Angle = _gameConfig.DefBallAngle;
        }
        
        private void RestartGame()
        {
            _gameEndScreenView.Hide();
            _gameInitScreenView.Show();
            
            var points = _field.GetComponentsInChildren<BlocksHolder>();
            foreach (var point in points)
            {
                Destroy(point.gameObject);
            }

            IsNeedInit = true;
            IsBallOnCarriage = true;
        }

        private void OnDestroy()
        {
            _ball.DeadBall -= BallDead;
            
            _healthService.OnGainHealth -= _carriage.UpdateHeartIcon;
            _healthService.OnLoseHealth -= _carriage.UpdateHeartIcon;
            
            _gameEndScreenView.Button.onClick.RemoveAllListeners();
        }

        private void UpdateCarriagePosition()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                _carriage.Position += Vector2.left * _gameConfig.DefCarriageSpeed;
            
            if (Input.GetKey(KeyCode.RightArrow))
                _carriage.Position += Vector2.right * _gameConfig.DefCarriageSpeed;

            float carriageMax = _carriage.Position.x + _carriage.Bounds.extents.x;
            float carriageMin = _carriage.Position.x - _carriage.Bounds.extents.x;
            
            float fieldMax = _field.bounds.max.x;
            float fieldMin = _field.bounds.min.x;
            
            if (carriageMax > fieldMax)
                _carriage.Position = new Vector2(fieldMax - _carriage.Bounds.extents.x, _carriage.Position.y);
            else if (carriageMin < fieldMin)
                _carriage.Position = new Vector2(fieldMin + _carriage.Bounds.extents.x, _carriage.Position.y);
        }

        private void UpdateBallPosition()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                IsBallOnCarriage = false;
            }
            
            if (IsBallOnCarriage)
            {
                _ball.Follow(_carriage.transform);
            }
            else
            {
                _ball.UpdatePosition(_gameConfig.DefBallSpeed, Time.deltaTime);
            }
            
        }

        private void BallDead()
        {
            IsBallOnCarriage = true;
            _ball.transform.position = _carriage.BallPosition;
            _ball.Angle = _gameConfig.DefBallAngle;
            
            _healthService.LoseHealth();
        }
    }
}
