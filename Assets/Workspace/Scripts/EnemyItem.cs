using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Workspace.Scripts
{
    public class EnemyItem : MonoBehaviour
    {
        #region Variables

        public EnemyItemProperties properties;
        public int health;
        public Transform target;

        #endregion


        #region Unity Funcs

        private void Start()
        {
            health = properties.health;
        }

        #endregion
        #region Funcs
        
        public void StartWalk(Transform targetObj)
        {
            transform.DOMove(targetObj.position, 10/properties.speed)
                .SetEase(Ease.Linear)
                .SetId(22)
                .OnComplete(() =>
                {
                    ObjectPool.instance.AddEnemyItem(gameObject);
                    GameManager.instance.Lose();
                });
        }

        public void TakeDamage(int damageCount)
        {
            health -= damageCount;

            if (health <= 0)
            {
                // death
                GameManager.instance.totalKilledEnemy++;
                if (GameManager.instance.totalKilledEnemy >= EnemySpawnManager.instance.totalEnemyCount)
                {
                    GameManager.instance.Win();
                }
                ObjectPool.instance.AddEnemyItem(gameObject);
            }
        }

        public void ResetEnemy()
        {
            health = properties.health;
        }

        #endregion


    }
}
