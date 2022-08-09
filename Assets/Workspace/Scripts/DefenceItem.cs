using System;
using UnityEngine;

namespace Workspace.Scripts
{
    public class DefenceItem : MonoBehaviour
    {
        #region Variables

        public DefenceItemProperties properties;

        public ItemChoice currentItemChoicer;

        public bool isActive = false;

        private float curTime = 0.0f;

        public GameObject targetEnemy;
        public Vector2 targetDirection;

        public LayerMask enemyLayer;

        #endregion

        #region Unity Funcs

        private void Start()
        {
            curTime = properties.attackCd;
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (properties.allDirections)
            {
                Debug.DrawRay(transform.position,Vector3.right*properties.range,Color.red,1);
                Debug.DrawRay(transform.position,Vector3.left*properties.range,Color.red,1);
                Debug.DrawRay(transform.position,Vector3.up*properties.range,Color.red,1);
                Debug.DrawRay(transform.position,Vector3.down*properties.range,Color.red,1);
            }
            else
            {
                Debug.DrawRay(transform.position,Vector3.right*properties.range,Color.red,1);
            }
#endif
           
            
            if(!isActive) return;
            if (targetEnemy == null)
            {
                CheckEnemyInRange();
            }
            else
            {
                Fire();
            }
        }

        #endregion

        public void Fire()
        {
            if (curTime < properties.attackCd)
            {
                curTime += Time.deltaTime;
            }
            else
            {
                if(!CheckEnemyInRange()) return;
                // Shoot
                var bullet = ObjectPool.instance.GetBullet();
                bullet.transform.position = transform.position;
                bullet.gameObject.SetActive(true);
                bullet.PrepareBullet(targetEnemy.transform,properties.attackDamage);
                bullet.Shoot();
                
                curTime = 0;
            }
        }
        public bool CheckEnemyInRange()
        {
            if (properties.allDirections)
            {
                var position = transform.position;
                var hitRight = Physics2D.Raycast(position, Vector2.right, properties.range,enemyLayer);
                var hitLeft = Physics2D.Raycast(position, Vector2.left, properties.range,enemyLayer);
                var hitUp = Physics2D.Raycast(position, Vector2.up, properties.range,enemyLayer);
                var hitDown = Physics2D.Raycast(position, Vector2.down, properties.range,enemyLayer);

                if (hitRight.collider != null && hitRight.transform.CompareTag("Enemy"))
                {
                    targetEnemy = hitRight.transform.gameObject;
                    targetDirection = Vector2.right;
                    return true;
                }

                if (hitLeft.collider != null && hitLeft.transform.CompareTag("Enemy"))
                {
                    targetEnemy = hitLeft.transform.gameObject;
                    targetDirection = Vector2.left;
                    return true;
                }

                if (hitUp.collider != null && hitUp.transform.CompareTag("Enemy"))
                {
                    targetEnemy = hitUp.transform.gameObject;
                    targetDirection = Vector2.up;
                    return true;
                }

                if (hitDown.collider != null && hitDown.transform.CompareTag("Enemy"))
                {
                    targetEnemy = hitDown.transform.gameObject;
                    targetDirection = Vector2.down;
                    return true;
                }

            }
            else
            {
                var hitRight = Physics2D.Raycast(transform.position, Vector2.right, properties.range,enemyLayer);
                
                if (hitRight.collider != null && hitRight.collider.CompareTag("Enemy"))
                {
                    targetEnemy = hitRight.transform.gameObject;
                    targetDirection = Vector2.right;
                    return true;
                }
            }

            return false;

        }


    }
}