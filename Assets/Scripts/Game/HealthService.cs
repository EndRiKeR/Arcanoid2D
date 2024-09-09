using System;
using Arkanoid2D.Configs;
using Arkanoid2D.PrefabScripts;
using UnityEditor;
using UnityEngine;

namespace Arkanoid2D.Game
{
    public class HealthService : MonoBehaviour
    {
        public int Health { get; private set; } = 0;
        
        public event Action OnGainHealth;
        public event Action OnLoseHealth;

        void Awake()
        {
            //OnGainHealth
            //OnLoseHealth
        }

        private void OnDestroy()
        {
            //OnGainHealth
            //OnLoseHealth
        }

        public void GainHealth(int gain = 1)
        {
            Health += gain;
            
            OnGainHealth?.Invoke();
        }
        
        public void LoseHealth(int lose = 1)
        {
            Health += lose;
            
            OnLoseHealth?.Invoke();
        }
    }
}
