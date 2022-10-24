using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCropCreate : MonoBehaviour
{

    [SerializeField]
    private Crop crop;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            ItemManager.Instance.CreateSceneItem(crop, transform.position);
        }
    }


}
