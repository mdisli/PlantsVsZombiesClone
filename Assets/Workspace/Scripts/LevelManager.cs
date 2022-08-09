using System;
using System.Collections.Generic;
using UnityEngine;

namespace Workspace.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        #region Variables

        public static LevelManager instance;

        public List<LevelData> LevelDatas;

        #endregion

        #region Unity Funcs

        private void Awake()
        {
            instance = this;
        }

        #endregion
    }

    [System.Serializable]
    public class LevelData
    {
        public List<int> defItemsCount;
        public List<int> enemyItemsCount;
    }
}