using BC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_Absoltion : MonoBehaviour
{
    [SerializeField]
    Transform targetTransform;
    [SerializeField]
    VacuumPack vacuumPack;

    public void OnTriggerStay(Collider other)
    {
        if (other.transform.TryGetComponent(out IInteraction target))
        {
            bool isSlime = target.type == Item.ItemType.Slime;
            if (isSlime != vacuumPack.isSlime) return;
            Vector3 direction = targetTransform.position - other.transform.position;
            target.rigi.AddForce(direction);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out IInteraction target))
        {
            bool isSlime = target.type == Item.ItemType.Slime;
            if (isSlime != vacuumPack.isSlime) return;
            target.MoveStop();
        }
    }
}
