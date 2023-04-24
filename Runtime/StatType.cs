using UnityEngine;

namespace Hybel.StatSystem
{
    [CreateAssetMenu(fileName = "New Stat Type", menuName = "Stats/Stat Type")]
    public class StatType : ScriptableObject, IStatType
    {
        [SerializeField] private float defaultValue;
#if UNITY_EDITOR
        [Space]
        [TextArea]
        [SerializeField] private string description;
#endif

        public string Name => name;
        public float DefaultValue => defaultValue;
    }
}