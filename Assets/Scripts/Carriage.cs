using Unity.Collections;
using UnityEditor;
using UnityEngine;

namespace Arkanoid2D
{
    public class Carriage : MonoBehaviour
    {
        public Vector2 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Bounds Bounds => _collider.bounds;

        [SerializeField]
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private BoxCollider2D _collider;
    }
}