using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimTest : MonoBehaviour
{
    [SerializeField]
    private Gem crop;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ItemManager.Instance.CreateSceneItem(crop, transform.position);
        }
    }

}
