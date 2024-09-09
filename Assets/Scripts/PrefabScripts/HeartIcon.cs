using UnityEngine;
using UnityEngine.UI;

namespace Arkanoid2D.PrefabScripts
{
    public class HeartIcon : MonoBehaviour
    {
        [SerializeField]
        private Image _image;

        public bool IsEnabled
        {
            get => _image.enabled;
            set => _image.enabled = value;
        }
    }
}