using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

namespace Workspace.Scripts
{
    public class EnemySpawnManager : MonoBehaviour
    {
        #region Variables

        public static EnemySpawnManager instance;

        public List<Transform> spawnPoints;
        public List<Transform> endPoints;
        
        public int enemy1Count;
        public int enemy2Count;
        public int enemy3Count;

        public int totalEnemyCount;

        public float spawnInterval;
        

        #endregion

        #region Unity Funcs

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            enemy1Count = LevelManager.instance.LevelDatas[GameManager.instance.activeLevel].enemyItemsCount[0];
            enemy2Count = LevelManager.instance.LevelDatas[GameManager.instance.activeLevel].enemyItemsCount[1];
            enemy3Count = LevelManager.instance.LevelDatas[GameManager.instance.activeLevel].enemyItemsCount[2];

            totalEnemyCount = enemy1Count + enemy2Count + enemy3Count;

            StartCoroutine(Spawn());
        }

        #endregion

        #region Funcs
        
        public IEnumerator Spawn()
        {
            var waitForSeconds = new WaitForSeconds(spawnInterval);

            yield return waitForSeconds;
            for (int i = 0; i < totalEnemyCount;)
            {
                var randomNum = Random.Range(0, 3);
                var randomLine = Random.Range(0, spawnPoints.Count);

                EnemyItem enemy;
                switch (randomNum)
                { 
                        
                    case 0:
                        if (enemy1Count <= 0) continue;
                        enemy = ObjectPool.instance.GetEnemyItem(0);
                        enemy.transform.position = spawnPoints[randomLine].position;
                        enemy.gameObject.SetActive(true);
                        enemy.StartWalk(endPoints[randomLine]);
                        enemy1Count--;
                        i++;

                        yield return waitForSeconds;
                        break;
                    case 1:
                        if (enemy2Count <= 0) continue;
                        enemy = ObjectPool.instance.GetEnemyItem(1);
                        enemy.transform.position = spawnPoints[randomLine].position;
                        enemy.gameObject.SetActive(true);
                        enemy.StartWalk(endPoints[randomLine]);
                        enemy2Count--;
                        i++;
                        yield return waitForSeconds;
                        break;
                    case 2:
                        if (enemy3Count <= 0) continue;
                        enemy = ObjectPool.instance.GetEnemyItem(2);
                        enemy.transform.position = spawnPoints[randomLine].position;
                        enemy.gameObject.SetActive(true);
                        enemy.StartWalk(endPoints[randomLine]);
                        enemy3Count--;
                        i++;
                        yield return waitForSeconds;
                        break;
                }
            }
        }

        #endregion
    }
}
