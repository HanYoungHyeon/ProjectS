using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BC
{
    public class TypeChanger : MonoBehaviour,IStabable
    {
        VacuumPack vacuumPack;
        private void Awake()
        {
            transform.parent.TryGetComponent(out vacuumPack);
        }
        public void StabEvent()
        {
            if (vacuumPack.isSlime)
            {
                vacuumPack.isSlime = false;
                transform.localPosition -= new Vector3(0, 0, 1.8f);
                return;
            }
            vacuumPack.isSlime = true;
            transform.localPosition += new Vector3(0, 0, 1.8f);
        }

 


    }

}
