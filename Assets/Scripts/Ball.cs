using Unity.Collections;
using UnityEditor;
using UnityEngine;

namespace Arkanoid2D
{
    public class Ball : MonoBehaviour
    {
        [field: SerializeField]
        public float Speed { get; private set; }

        [field: SerializeField]
        public float Angle { get; set; }

        public Vector2 Position
        {
            get => _rigidbody.position;
            set => _rigidbody.MovePosition(value);
        }

        public Bounds Bounds => _collider.bounds;

        [SerializeField]
        private Collider2D _collider;

        [SerializeField]
        private Rigidbody2D _rigidbody;

        public void UpdatePosition(float deltaTime)
        {
            Position += AngleToVector(Angle) * Speed * deltaTime;
        }

        public void Follow(Transform target)
        {
            float yOffset = transform.position.y - target.position.y;
            Position = new Vector2(target.position.x, target.position.y + yOffset);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            ProcessCollision(this, other);
        }
        
        public void ProcessCollision(Ball ball, Collision2D collision)
        {
            ProcessBounce(ball, collision);

            if (collision.gameObject.TryGetComponent(out Block block))
                block.HitBlock();
        }

        private void ProcessBounce(Ball ball, Collision2D collision)
        {
            Vector2 normal = collision.contacts[0].normal;
            float mirrorAngle = GetMirrorAngle(ball.Angle, normal);

            if (collision.gameObject.TryGetComponent(out Carriage carriage))
            {
                if (ShouldIgnoreCarriageCollision(ball, normal))
                {
                    Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
                    return;
                }

                float steerFactor = -1 * (ball.Position.x - carriage.Position.x) / carriage.Bounds.extents.x;
                steerFactor = Mathf.Clamp(steerFactor, -1f, 1f);
                float steerAngle = Remap(steerFactor, -1, 1, 10, 170);
                ball.Angle = (mirrorAngle + steerAngle) / 2f;
            }
            else
            {
                ball.Angle = mirrorAngle;
            }
        }

        private bool ShouldIgnoreCarriageCollision(Ball ball, Vector2 normal)
        {
            bool collidedWithSide = normal != Vector2.up;
            bool movingUpwards = Vector2.Dot(normal, AngleToVector(ball.Angle)) > 0;
            return collidedWithSide || movingUpwards;
        }
        
        public Vector2 AngleToVector(float angleDegrees)
        {
            return new Vector2(
                    Mathf.Cos(angleDegrees * Mathf.Deg2Rad),
                    Mathf.Sin(angleDegrees * Mathf.Deg2Rad))
                .normalized;
        }
		
        public Vector2 GetMirrorVector(Vector2 vector, Vector2 normal)
        {
            if (Vector2.Dot(vector, normal) > 0)
                return vector;
            return vector - 2f * Vector2.Dot(vector, normal) * normal;
        }
        
        public float VectorToAngle(Vector2 vector2)
        {
            return Mathf.Atan2(vector2.y, vector2.x) * Mathf.Rad2Deg;
        }
        
        public float GetMirrorAngle(float angleDegrees, Vector2 normal)
        {
            return VectorToAngle(GetMirrorVector(AngleToVector(angleDegrees), normal));
        }
        
        public static float Remap(float from, float fromMin, float fromMax, float toMin, float toMax)
        {
            var fromAbs  =  from - fromMin;
            var fromMaxAbs = fromMax - fromMin;

            var normal = fromAbs / fromMaxAbs;

            var toMaxAbs = toMax - toMin;
            var toAbs = toMaxAbs * normal;

            var to = toAbs + toMin;

            return to;
        }
    }
}