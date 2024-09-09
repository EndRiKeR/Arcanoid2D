using Unity.Collections;
using UnityEditor;
using UnityEngine;

namespace Arkanoid2D
{
    public abstract class Block
    {
        public abstract int Health { get; }
        public abstract void HitBlock();
        public abstract void OnDestroy();
    }
}