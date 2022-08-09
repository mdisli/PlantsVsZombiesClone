using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Workspace.Scripts
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
    
        public static GameManager instance;

        public int Level
        {
            get => PlayerPrefs.GetInt("Level", 0);
            set => PlayerPrefs.SetInt("Level",value);
        }
        public int activeLevel;

        public int totalKilledEnemy = 0;

        #endregion

        #region Unity Funcs

        private void Awake()
        {
            instance = this;

            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            activeLevel = Level % LevelManager.instance.LevelDatas.Count;
            
        }

        #endregion

        #region Level Funcs

        public void Win()
        {
            Level++;
            UIController.instance.WinScreen();
        }

        public void Lose()
        {
            UIController.instance.LoseScreen();
        }
        
        public void NextLevel()
        {
            // var totalLevelCount = SceneManager.sceneCountInBuildSettings;
            // var nextSceneIndex = Level <= totalLevelCount ? Level - 1 : (Level % totalLevelCount) - 1;
            //
            // SceneManager.LoadScene(nextSceneIndex);

            SceneManager.LoadScene(0);

        }

        public void Restart()
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(0);
        }

        #endregion
    }
}
