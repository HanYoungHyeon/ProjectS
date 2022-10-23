using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrabbable : MonoBehaviour, ICustomGrabbable
{
    public bool IsGrabed { get;  set; }
    public virtual void GrabBegin(GameObject grabberObj)
    {
        IsGrabed = true;

        if (GetComponent<Rigidbody>() != null)
            GetComponent<Rigidbody>().isKinematic = true;
        transform.parent = grabberObj.transform;
    }

    public virtual void GrabEnd(GameObject grabberObj)
    {
        IsGrabed = false;

        if (GetComponent<Rigidbody>() != null)
            GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
    }
}
