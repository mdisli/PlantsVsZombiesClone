using UnityEngine;

namespace Workspace.Scripts
{
    [CreateAssetMenu(fileName = "EnemyItem", menuName = "EnemyItem", order = 1)]
    public class EnemyItemProperties : ScriptableObject
    {
        public float speed;
        public int health;
    }
}