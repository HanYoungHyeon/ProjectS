using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowSlotInfo : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI countText;
    [SerializeField]
    private Image itemImage;
    [SerializeField]
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
