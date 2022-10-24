using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace BC
{
    public class Slot : MonoBehaviour
    {
        [HideInInspector]
        public Item curSlotItem;
        Image itemImage;
        private int itemCount;
        TextMeshProUGUI itemCountUI;
        public int ItemCount { get { return itemCount; }
            set 
            {
                if(value == 0)
                {
                    curSlotItem = null;
                    itemImage .sprite= null;
                }
                itemCount = value;
                itemCountUI.text = value.ToString();
            }
        }

        private void Start()
        {
            transform.FindComponentChild(out itemImage);
            transform.FindComponentChild(out itemCountUI);
        }

        public void AddItem(Item item)
        {
            curSlotItem = item;
            itemImage.sprite = item.itemSprite;
        }
    }
}

