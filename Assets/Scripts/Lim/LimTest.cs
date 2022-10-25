using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimTest : MonoBehaviour
{
    [SerializeField]
    private Crop crop;
    //Rigidbody rb;
    private void Awake()
    {
        //rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ItemManager.Instance.CreateSceneItem(crop, transform.position);
        }
        /*
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up*0.5f,ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(Vector3.forward * 0.5f, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            rb.AddForce(Vector3.forward * -0.5f, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.AddForce(Vector3.right * -0.5f, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.AddForce(Vector3.right * 0.5f, ForceMode.Impulse);
        }
        */
    }

}
