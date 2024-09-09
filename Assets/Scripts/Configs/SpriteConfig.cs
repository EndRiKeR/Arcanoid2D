using UnityEngine;

namespace Arkanoid2D.Configs
{
    [CreateAssetMenu(fileName = "SpriteConfig", menuName = "_Configs/SpriteConfig", order = 0)]
    public class SpriteConfig : ScriptableObject
    {
        [Header("Blocks Sprites")]
        [SerializeField] public Sprite One;
        [SerializeField] public Sprite Two;
        [SerializeField] public Sprite Three;
        [SerializeField] public Sprite Four;
        [SerializeField] public Sprite Five;
    }
}