using UnityEngine;
using UnityEngine.Serialization;

namespace Workspace.Scripts
{
    [CreateAssetMenu(fileName = "DefenceItemProperties", menuName = "DefenceItem", order = 0)]
    public class DefenceItemProperties : ScriptableObject
    {
        public int range;
        public float health;
        public float attackCd;
        public int attackDamage;

        public bool allDirections;
    }
}