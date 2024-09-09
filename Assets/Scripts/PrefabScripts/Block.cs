using Arkanoid2D.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace Arkanoid2D.PrefabScripts
{
    public class Block : MonoBehaviour
    {
        [field: SerializeField]
        private int Health { get; set; } = 2;

        [SerializeField]
        private Image _image;
        
        private SpriteConfig _spriteConfig;

        public void InitBlock(SpriteConfig config)
        {
            _spriteConfig = config;
            UpdateSprite();
        }
        
        public void HitBlock(int damage = 1)
        {
            Health -= damage;
            if (Health <= 0)
                DestroyBlock();
            else
                UpdateSprite();
        }
        
        private void DestroyBlock()
        {
            Destroy(gameObject);
        }

        private void UpdateSprite()
        {
            switch (Health)
            {
                case 4:
                    _image.sprite = _spriteConfig.Four;
                    break;
                case 3:
                    _image.sprite = _spriteConfig.Three;
                    break;
                case 2:
                    _image.sprite = _spriteConfig.Two;
                    break;
                case 1:
                    _image.sprite = _spriteConfig.One;
                    break;
                default:
                    _image.sprite = _spriteConfig.Five;
                    break;
            }
        }
    }
}