using BC;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class WallFillter : MonoBehaviour
{
    [SerializeField]
    private LayerMask blockLayer;
    private SlimeFarmMachine slimeFarm;

    private void OnEnable()
    {
        slimeFarm = GetComponentInParent<SlimeFarmMachine>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & blockLayer) == 0)
        {
            return;
        }
        //if(other.transform.parent == slimeFarm.InsideObject.transform)
        //{
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (other.TryGetComponent(out SceneSlime forSlime))
        {
            Vector3 opposite;
            if (forSlime.Agent.enabled)
            {
                opposite = Vector3.Reflect(forSlime.Agent.velocity, transform.up);
            }
            else
            {
                opposite = Vector3.Reflect(forSlime.rigi.velocity, transform.up);
            }
            forSlime.MoveStop(0.2f);
            rb.velocity = Vector3.zero;
            rb.AddForce(opposite*0.05f, ForceMode.Impulse);
        }
        if (rb != null)
        {
            Vector3 opposite = Vector3.Reflect(rb.velocity, transform.up);
            //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z * -1);
            rb.velocity = Vector3.zero;
            rb.AddForce(opposite*0.05f,ForceMode.Impulse);
        }
        //}
        //else
        //{
        //    other.transform.parent = slimeFarm.InsideObject.transform;
        //}
    }
}
