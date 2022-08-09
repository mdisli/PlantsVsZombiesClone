using System;
using UnityEngine;
using UnityEngine.UI;

namespace Workspace.Scripts
{
    public class ItemHolder : MonoBehaviour
    {
        #region Variables

        private GameObject outlineImage;

        public bool isEmpty = true;
        
        public DefenceItem currentItem;
        
        #endregion

        #region Unity Funcs

        private void Start()
        {
            outlineImage = transform.GetChild(0).gameObject;
            outlineImage.SetActive(false);
        }

        #endregion
        
        #region Triggers

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("DefItem"))
            {
                currentItem = col.GetComponent<DefenceItem>();
                currentItem.currentItemChoicer.currentItemHolder = this.GetComponent<ItemHolder>();
                if (isEmpty)
                {
                    outlineImage.SetActive(true);
                    
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("DefItem"))
            {
                outlineImage.SetActive(false);
                currentItem.currentItemChoicer.currentItemHolder = null;
            }

            
        }

        #endregion
    }
}
