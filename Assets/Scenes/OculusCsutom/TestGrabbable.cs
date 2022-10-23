using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrabbable : CustomGrabbable
{

    private void Update()
    {
        if(IsGrabed)
        {
            if(Input.GetMouseButton(0))         //이부분을 VR 인풋에 맞게 변경하면 됨.
            {
                Debug.Log("총알 발사!!");
            }
            else if(Input.GetMouseButton(1))
            {
                Debug.Log("총알 리로드");
            }
            else
                Debug.Log("으악 잡혀있다.");
        }
    }
}
