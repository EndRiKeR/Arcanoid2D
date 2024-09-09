using UnityEngine;

namespace Arkanoid2D.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "_Configs/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public float DefCarriageSpeed { get; set; } = 30f;
        [field: SerializeField] public float DefBallAngle { get; set; } = 90f;
        [field: SerializeField] public float DefBallSpeed { get; set; } = 800f;
        [field: SerializeField] public float BoostBallSpeed { get; set; } = 50f;
        [field: SerializeField] public int PlayerHealth { get; set; } = 3;
    }
}