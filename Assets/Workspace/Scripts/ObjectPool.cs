using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace Workspace.Scripts
{
    public class ObjectPool : MonoBehaviour
    {
        #region Variables

        public static ObjectPool instance;

        //[TabGroup("Defence Items")]
        public List<PoolObject> defItems;
        
        //[TabGroup("Enemy Items")]
        public List<PoolObject> enemyItems;

        public List<PoolObject> bulletItems;

        public Transform defItemParent;
        public Transform enemyItemParent;
        public Transform bulletItemParent;
        

        #endregion

        #region Unity Funcs

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            CreatePool(defItems,defItemParent);
            CreatePool(enemyItems,enemyItemParent);
            CreatePool(bulletItems,bulletItemParent);
        }

        #endregion

        #region Pool Funcs

        private void CreatePool(List<PoolObject> poolList,Transform parent)
        {
            foreach (var variable in poolList)
            {
                for (int i = 0; i < variable.poolCount; i++)
                {
                    var obj = Instantiate(variable.objectPrefab, Vector3.zero, quaternion.identity, parent);
                    variable.poolList.Add(obj);
                    obj.SetActive(false);
                }
            }
        }

        public DefenceItem GetDefItem(int index)
        {
            if (defItems[index].poolList.Count > 0)
            {
                return defItems[index].poolList[0].GetComponent<DefenceItem>();
            }
            else
            {
                var variable = defItems[index];
                for (int i = 0; i < variable.poolCount; i++)
                {
                    var obj = Instantiate(variable.objectPrefab, Vector3.zero, quaternion.identity, defItemParent);
                    variable.poolList.Add(obj);
                    obj.SetActive(false);
                }

                return variable.poolList[0].GetComponent<DefenceItem>();
            }
        }

        public EnemyItem GetEnemyItem(int index)
        {
            var enemy = enemyItems[index].poolList[0];
            
            enemyItems[index].poolList.RemoveAt(0);

            return enemy.GetComponent<EnemyItem>();
        }

        public void AddEnemyItem(GameObject enemy, int index = 0)
        {
            DOTween.Kill(enemy.transform);
            
            var item = enemy.GetComponent<EnemyItem>();
            item.ResetEnemy();
            
            enemyItems[index].poolList.Add(enemy);
            enemy.SetActive(false);
        }

        public BulletController GetBullet(int index = 0)
        {
            var bulletItem = bulletItems[index];
            if (bulletItem.poolList.Count > 0)
            {
                var bullet = bulletItem.poolList[0].GetComponent<BulletController>();
                bulletItem.poolList.RemoveAt(0);

                return bullet;
            }
            else
            {
                for (int i = 0; i < bulletItem.poolCount; i++)
                {
                    var obj = Instantiate(bulletItem.objectPrefab, Vector3.zero, quaternion.identity, bulletItemParent);
                    bulletItem.poolList.Add(obj);
                    obj.SetActive(false);
                }
                
                var bullet = bulletItem.poolList[0].GetComponent<BulletController>();
                bulletItem.poolList.RemoveAt(0);

                return bullet;
            }
        }

        public void AddBullet(GameObject bullet, int index= 0)
        {
            bulletItems[index].poolList.Add(bullet);
            bullet.SetActive(false);
        }

        #endregion
    }

    [System.Serializable]
    public class PoolObject
    {
        public GameObject objectPrefab;
        public int poolCount;
        public List<GameObject> poolList;
        public Sprite itemIcon;
    }
}
