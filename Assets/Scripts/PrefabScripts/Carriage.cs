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
    }
}