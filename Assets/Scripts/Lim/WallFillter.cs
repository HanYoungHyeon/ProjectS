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
        if(((1<<other.gameObject.layer)&blockLayer)==0)
        {
            return;
        }
        if(other.transform.parent == slimeFarm.InsideObject.transform)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if(other.TryGetComponent(out SceneSlime forSlime))
            {
                Vector3 opposite = Vector3.Reflect(forSlime.Agent.velocity, transform.up);
                forSlime.MoveStop();
                rb.AddForce(opposite, ForceMode.Impulse);
            }
            if (rb != null)
            {
                Vector3 opposite = Vector3.Reflect(rb.velocity, transform.up);
                rb.velocity = Vector3.zero;
                rb.AddForce(opposite, ForceMode.Impulse);
            }
        }
        else
        {
            other.transform.parent = slimeFarm.InsideObject.transform;
        }
    }
}
