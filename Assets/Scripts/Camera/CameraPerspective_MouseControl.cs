using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPerspective_MouseControl : MonoBehaviour {

    public float scrollSpeed = 1f;
    public float rayDistance = 250f;

    Vector3 groundCamOffset;
    Vector3 camTarget;

    private Vector3 prevRaycastHit = Vector3.zero;
    private bool camMoving = false;

    private void Start()
    {
        Vector3 groundPos = GetWorldPosAtViewportPoint(0.5f, 0.5f);
        Debug.Log("groundPos: " + groundPos);
        groundCamOffset = transform.position - groundPos;
        camTarget = Camera.main.transform.position;
    }

    void Update()
    {   
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask cameraRaycasts = 1 << LayerMask.NameToLayer("CameraRaycasts");

            if (Physics.Raycast(ray, out hit, rayDistance, cameraRaycasts))
            {
                prevRaycastHit = hit.point;
                camMoving = true;
            }
        }

        // While dragging the camera around
        else if (Input.GetMouseButton(1) && camMoving)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask cameraRaycasts = 1 << LayerMask.NameToLayer("CameraRaycasts");

            if (Physics.Raycast(ray, out hit, rayDistance, cameraRaycasts))
            {
                var mousePosition = hit.point;
                var movement = prevRaycastHit - mousePosition;
                Camera.main.transform.position += movement;
            }
        }

        // Stop dragging camera
        else if (Input.GetMouseButtonUp(1))
        {
            camMoving = false;
        }

        // Center whatever position is right-clicked
        //if (Input.GetMouseButtonDown(1))
        //{            
        //    float mouseX = Input.mousePosition.x / Camera.main.pixelWidth;
        //    float mouseY = Input.mousePosition.y / Camera.main.pixelHeight;

        //    Vector3 clickPt = GetWorldPosAtViewportPoint(mouseX, mouseY);
        //    camTarget = clickPt + groundCamOffset;

        //    Camera.main.transform.position = camTarget;
        //}        

        // Zoom with scrollwheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            var cam = Camera.main.transform;
            var newPos = cam.position + cam.forward * scroll * scrollSpeed;
            cam.position = newPos;
        }
    }

    private Vector3 GetWorldPosAtViewportPoint(float vx, float vy)
    {
        float distanceToGround;

        Ray worldRay = Camera.main.ViewportPointToRay(new Vector3(vx, vy, 0));
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);        
        groundPlane.Raycast(worldRay, out distanceToGround);

        Debug.Log("distance to ground:" + distanceToGround);

        return worldRay.GetPoint(distanceToGround);
    }
}
