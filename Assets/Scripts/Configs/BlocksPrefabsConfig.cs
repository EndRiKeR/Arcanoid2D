using UnityEngine;

namespace Arkanoid2D.Configs
{
    [CreateAssetMenu(fileName = "BlocksPrefabsConfig", menuName = "_Configs/BlocksPrefabsConfig", order = 0)]
    public class BlocksPrefabsConfig : ScriptableObject
    {
        public GameObject GetRandomPrefab() => Prefabs[Random.Range(0, Prefabs.Length - 1)];

        [SerializeField]
        public GameObject[] Prefabs;
    }
}