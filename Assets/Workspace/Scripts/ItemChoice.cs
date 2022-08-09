using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Workspace.Scripts
{
    public class ItemChoice : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        #region Variables

        public int itemIndex;
        public int count;

        public DefenceItem currentItem;
        public ItemHolder currentItemHolder;

        public TextMeshProUGUI countText;
        public TextMeshProUGUI indexText;
        

        #endregion

        #region Unity Funcs

        private void Start()
        {
            count = LevelManager.instance
                .LevelDatas[GameManager.instance.activeLevel]
                .defItemsCount[itemIndex];
        
            indexText.SetText((itemIndex+1).ToString());
            countText.SetText(count.ToString());
            
            GetComponent<Image>().sprite = ObjectPool.instance.defItems[itemIndex].itemIcon;
        }

        #endregion
    
        #region Touching Funcs

        public void OnDrag(PointerEventData eventData)
        {
            if (currentItem != null)
            {
                currentItem.transform.position = Input.mousePosition;
            }
            
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            TakeObject();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            DropObject();
        }

        #endregion

        #region Funcs

        public void TakeObject()
        {
            if (count <= 0)
            {
                return;
            }

            currentItem = ObjectPool.instance.GetDefItem(itemIndex);
            currentItem.currentItemChoicer = this;
            currentItem.transform.position = Input.mousePosition;
            currentItem.gameObject.SetActive(true);
        }

        public void DropObject()
        {
            if (currentItemHolder != null && currentItemHolder.isEmpty)
            {
                currentItem.transform.SetParent(currentItemHolder.transform,true);
                currentItem.transform.localPosition = Vector3.zero;
                currentItem.isActive = true;

                ObjectPool.instance.defItems[itemIndex].poolList.RemoveAt(0);
                currentItemHolder.isEmpty = false;
                currentItem = null;

                count--;
                
                countText.SetText(count.ToString());

                if (count <= 0)
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                currentItem.gameObject.SetActive(false);
                currentItem.currentItemChoicer = null;
                currentItem = null;
            }
            
        }

        #endregion
    }
}
