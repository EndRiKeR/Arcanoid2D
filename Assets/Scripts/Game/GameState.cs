using System;
using Arkanoid2D.Configs;
using Arkanoid2D.PrefabScripts;
using UnityEngine;

namespace Arkanoid2D.Game
{
    public class GameState : MonoBehaviour
    {
        public event Action InitGameEvent;
        public event Action WaitForMoveGameEvent;
        public event Action ScorePointsGameEvent;
        public event Action EndGameEvent;
        
        [Header("Services")]
        [SerializeField]
        private HealthService _healthService;
        
        [Space]
        [Header("Configs")]
        [SerializeField]
        private GameConfig _gameConfig;
        
        [Space]
        [Header("Base Objects")]
        [SerializeField]
        private Carriage _carriage;

        [SerializeField]
        private Ball _ball;

        [SerializeField]
        private Collider2D _field;

        [Space]
        [Header("Flags")]
        public bool IsNeedInit = true;
        public bool IsBallOnCarriage = true;
        
        void Awake()
        {
            _healthService.GainHealth(_gameConfig.PlayerHealth);
            
            _ball.DeadBall += BallDead;
            // add UI
        }

        void FixedUpdate()
        {
            if (IsNeedInit)
                InitGame();

            UpdateGame();
        
            if (CheckGameEnd())
                EndGame();
        }

        private void InitGame()
        {
            IsNeedInit = false;

            _ball.transform.position = _carriage.BallPosition;
            _ball.Angle = _gameConfig.DefBallAngle;
            
            InitGameEvent?.Invoke();
        }
    
        private void UpdateGame()
        {
            UpdateCarriagePosition();
            UpdateBallPosition();
        }
    
        private bool CheckGameEnd() => _healthService.Health == 0;

        private void EndGame()
        {
            EndGameEvent?.Invoke();
        }

        private void OnDestroy()
        {
            _ball.DeadBall -= BallDead;
            
            // delete UI
        }

        private void UpdateCarriagePosition()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                    _carriage.Position += Vector2.left * _gameConfig.DefCarriageSpeed;
            
            if (Input.GetKey(KeyCode.RightArrow))
                _carriage.Position += Vector2.right * _gameConfig.DefCarriageSpeed;

            ClampCarriagePosition();
            
        }

        private void UpdateBallPosition()
        {
            if (Input.GetKey(KeyCode.Space))
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
            
            WaitForMoveGameEvent?.Invoke();
        }
        
        private void ClampCarriagePosition()
        {
            float carriageHalfSize = _carriage.Bounds.extents.x;
            float carriageMax = _carriage.Position.x + carriageHalfSize;
            float fieldMax = _field.bounds.max.x;
            float carriageMin = _carriage.Position.x - carriageHalfSize;
            float fieldMin = _field.bounds.min.x;

            if (carriageMax > fieldMax)
                _carriage.Position = new Vector2(fieldMax - carriageHalfSize, _carriage.Position.y);
            else if (carriageMin < fieldMin)
                _carriage.Position = new Vector2(fieldMin + carriageHalfSize, _carriage.Position.y);
        }
    }
}
