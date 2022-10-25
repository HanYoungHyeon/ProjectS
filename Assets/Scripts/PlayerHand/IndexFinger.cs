using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexFinger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌");
        if(other.TryGetComponent(out IStabable target))
        {
            Debug.Log("충돌2");
            target.StabEvent();
        }

    }

}
