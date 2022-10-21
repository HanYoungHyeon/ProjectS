using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BC
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        GameObject movePointObject;
        [SerializeField]
        LayerMask groundMask;
        [SerializeField]
        LayerMask uiMask;
        [SerializeField]
        float uiInteractionDistance;
        [SerializeField]
        Camera cameraMain;

        RaycastHit movementHit;
        Ray moveRay;
        RaycastHit uiHit;
        Ray interactionRay;

        LineRenderer lineRenderer;
        Vector3[] linePositions = new Vector3[2];

        private void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }


        private void Update()
        {
            //interactionRay = cameraMain.ScreenPointToRay(transform.forward*5);
            //Physics.Raycast(interactionRay, out uiHit, uiInteractionDistance, uiMask);
            if (Input.GetMouseButton(1))
            {
                Time.timeScale = 0.1f;
                moveRay = cameraMain.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(moveRay, out movementHit, Mathf.Infinity, groundMask))
                {
                    linePositions[0] = transform.position;
                    movePointObject.SetActive(true);
                    movePointObject.transform.position = movementHit.point;
                    linePositions[1] = movementHit.point;
                    lineRenderer.SetPositions(linePositions);
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                Time.timeScale = 1f;
                transform.position = movePointObject.transform.position;
                movePointObject.SetActive(false);
            }
        }

    }

}
