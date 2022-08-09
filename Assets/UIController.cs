using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIController : MonoBehaviour
{
    #region Variables

    public static UIController instance;

    public GameObject winScreen;
    public GameObject loseScreen;
    
    

    #endregion

    #region Unity Funcs

    private void Awake()
    {
        instance = this;
    }

    #endregion

    #region Funcs

    public void WinScreen()
    {
        if(winScreen.activeSelf) return;
        var canvasGroup = winScreen.GetComponent<CanvasGroup>();
        
        winScreen.SetActive(true);

        canvasGroup.DOFade(1, .35f)
            .From(0)
            .SetEase(Ease.Linear);
    }

    public void LoseScreen()
    {
        if(loseScreen.activeSelf) return;
        
        var canvasGroup = loseScreen.GetComponent<CanvasGroup>();
        
        loseScreen.SetActive(true);

        canvasGroup.DOFade(1, .35f)
            .From(0)
            .SetEase(Ease.Linear);
    }

    #endregion
}
