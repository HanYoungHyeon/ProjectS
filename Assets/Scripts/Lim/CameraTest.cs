using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraTest : MonoBehaviour
{
    public LayerMask mask;
    UIEventFunc target;

    Scrollbar targetSlider;
    public static int money = 100000;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hitSlider;

        Debug.DrawRay(transform.position, transform.forward * 1000);
        if(Physics.Raycast(transform.position,transform.forward, out hitSlider,Mathf.Infinity))
        {
            
            targetSlider = hitSlider.transform.GetComponent<Scrollbar>();
        }
        if(targetSlider != null)
        {
            if(Input.GetKey(KeyCode.O))
            {
                targetSlider.value += 0.1f;
            }
            if(Input.GetKey(KeyCode.L))
            {
                targetSlider.value -= 0.1f;
            }
        }


        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.up*0.05f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.up*0.05f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right* 0.05f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * 0.05f;
        }
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity,mask);
            if (target == null)
            {
                if (hit.transform != null)
                {
                    target = hit.transform.GetComponent<UIEventFunc>();
                    target.OnClick.Invoke();
                }
            }
            else if (target != null)
            {
                if (hit.transform != null)
                {
                    hit.transform.TryGetComponent(out UIEventFunc check);
                    if (check != target)
                    {
                        target.OffClick.Invoke();
                        target = check;
                        target.OnClick.Invoke();
                    }
                    else if (check == target)
                    {
                        target.OnClick.Invoke();
                    }
                }
            }
        }
        else
        {
            if(target != null)
            {
                target.OffClick.Invoke();
                target = null;
            }
        }
    }
}
