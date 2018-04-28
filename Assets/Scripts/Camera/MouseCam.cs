using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCam : MonoBehaviour {

    private Vector3 prevRaycastHit = Vector3.zero;
    private bool camMoving = false;

    private void Start()
    {
        
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask cameraRaycasts = 1 << LayerMask.NameToLayer("CameraRaycasts");

            if (Physics.Raycast(ray, out hit, 50f, cameraRaycasts))
            {
                prevRaycastHit = hit.point;
                camMoving = true;
            } 
        }

        else if (Input.GetMouseButton(0) && camMoving)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask cameraRaycasts = 1 << LayerMask.NameToLayer("CameraRaycasts");

            if (Physics.Raycast(ray, out hit, 50f, cameraRaycasts))
            {
                var mousePosition = hit.point;
                var movement = prevRaycastHit - mousePosition;
                transform.position += movement;
            }
        }

        else if (Input.GetMouseButtonDown(1))
        {
            RaycastHit mouseHit, cameraHit;
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray cameraRay = Camera.main.ScreenPointToRay(transform.position);
            LayerMask cameraRaycasts = 1 << LayerMask.NameToLayer("CameraRaycasts");

            if (Physics.Raycast(mouseRay, out mouseHit, 50f, cameraRaycasts))
            {
                if (Physics.Raycast(cameraRay, out cameraHit, 50f, cameraRaycasts))
                {
                    var mousePosition = mouseHit.point;
                    var cameraPosition = cameraHit.point;
                    var movement = cameraPosition - mousePosition;
                    transform.position -= movement;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            camMoving = false;
        }
    }
}
