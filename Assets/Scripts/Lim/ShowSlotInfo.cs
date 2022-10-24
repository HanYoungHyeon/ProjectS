using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowSlotInfo : MonoBehaviour
{
    private TextMeshProUGUI countText;
    private Image itemImage;
    private Sprite emptyImage;

    public void ChangeImage(Item item, int value)
    {
        if (item == null || value == 0)
        {
            itemImage.sprite = emptyImage;
            countText.text = "";
        }
        else
        {
            itemImage.sprite = item.itemSprite;
            countText.text = value+"";
        }
    }
}
