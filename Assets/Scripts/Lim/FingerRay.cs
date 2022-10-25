using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerRay : MonoBehaviour
{
    public LayerMask mask;
    private UIEventFunc target;

    private void ClickUI()
    {
        if (target != null) // 버튼 입력시로 변경
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, mask);//레이 발사 위치 조정
            if (target == null)
            {
                if (hit.transform != null)
                {
                    target = hit.transform.GetComponent<UIEventFunc>();
                    target?.OnClick.Invoke();
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
                        target?.OnClick.Invoke();
                    }
                    else if (check == target)
                    {
                        target?.OnClick.Invoke();
                    }
                }
            }
        }
        else if (target == null)
        {
            if (target != null)
            {
                target.OffClick.Invoke();
                target = null;
            }
        }
    }
}
