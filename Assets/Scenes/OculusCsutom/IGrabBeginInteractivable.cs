using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICustomGrabbable
{
    public bool IsGrabed            //잡은 상태인지를 체크하는 프로퍼티
    {
        get; set; 
    }
    public void GrabBegin(GameObject grabberObj);   //잡았을때,처리할 부분
    public void GrabEnd(GameObject grabberObj);     //놓았을때,처리할 부분
}
