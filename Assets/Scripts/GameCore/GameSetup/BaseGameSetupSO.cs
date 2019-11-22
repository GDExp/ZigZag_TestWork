using UnityEngine;

namespace ZigZag.GameCore
{
    [CreateAssetMenu(fileName = "GameSetupValue", menuName = "ZigZag/GameSetup/NewSetupValue", order = 100)]
    public class BaseGameSetupSO : ScriptableObject
    {
        [Range(0,10)]
        public float gameSpeed;
        [Range(5f, 100f)]
        public float crystalChance;

        public Rigidbody groundPrefab;
        public Rigidbody crystalPrefab;
        public Rigidbody playerPrefab;
    }
}
