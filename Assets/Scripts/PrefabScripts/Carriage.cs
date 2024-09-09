using System.Linq;
using UnityEngine;

namespace Arkanoid2D.PrefabScripts
{
    public class Carriage : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private BoxCollider2D _collider;
        
        [SerializeField]
        private Transform _ballPos;

        public Vector2 Position
        {
            get => transform.position;
            set => transform.position = value;
        }
        
        public Vector2 BallPosition
        {
            get => _ballPos.position;
        }

        public Bounds Bounds => _collider.bounds;

        public void UpdateHeartIcon(int healthValue)
        {
            var allHearts = GetComponentsInChildren<HeartIcon>();
            var needToEnable = allHearts.Take(healthValue);
            
            foreach (var heart in allHearts)
            {
                heart.IsEnabled = false;
            }
            
            foreach (var heart in needToEnable)
            {
                heart.IsEnabled = true;
            }
        }
    }
}