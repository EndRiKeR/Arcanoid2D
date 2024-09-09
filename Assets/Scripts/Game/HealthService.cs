using System;
using UnityEngine;

namespace Arkanoid2D.Game
{
    public class HealthService : MonoBehaviour
    {
        public int Health { get; private set; } = 0;
        
        public event Action<int> OnGainHealth;
        public event Action<int> OnLoseHealth;

        public void GainHealth(int gain = 1)
        {
            Health += gain;
            
            OnGainHealth?.Invoke(Health);
        }
        
        public void LoseHealth(int lose = 1)
        {
            Health -= lose;
            
            OnLoseHealth?.Invoke(Health);
        }
    }
}
