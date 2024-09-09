using Arkanoid2D.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace Arkanoid2D.PrefabScripts
{
    public class Bonus : MonoBehaviour
    {
        [SerializeField]
        private Image _image;
        
        public BonusType Type = BonusType.None;
        private SpriteConfig _spriteConfig;
        
        public void InitBonus(SpriteConfig config)
        {
            _spriteConfig = config;
            UpdateSprite();
        }

        private void UpdateSprite()
        {
            switch (Type)
            {
                case BonusType.None:
                    _image.sprite = _spriteConfig.None;
                    break;
                case BonusType.Shield:
                    _image.sprite = _spriteConfig.Shield;
                    break;
                case BonusType.DoubleDamage:
                    _image.sprite = _spriteConfig.DoubleDamage;
                    break;
            }
        }
    }

    public enum BonusType
    {
        None,
        Shield,
        DoubleDamage
    }
}