using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrtho_MouseControl : MonoBehaviour {
    
    [Range(5,10)]
    public float minCamDistance = 5;

    [Range(10, 20)]
    public float maxCamDistance = 15;

    [Range(1, 20)]
    public int scrollSpeed = 10;

    [Range(0.5f, 4f)]
    public float rotateSpeed = 1f;

    public float rayDistance = 250f;

    private Camera cam;

    private bool camMoving = false;
    private Vector3 prevRaycastHit = Vector3.zero;    

    private bool camRotating = false;
    private Vector3 rotationPoint;
    private Vector3 prevMousePosition;
    private float remainingRotation;

    private int rotateButton = 1;
    private int moveButton = 2;

    private void Start()
    {
        cam = GetComponent<Camera>();
        Vector3 groundPos = GetWorldPosAtViewportPoint(0.5f, 0.5f);
    }

    void Update()
    {
        if (!camMoving) 
            Rotation_MouseWheel();

        if (!camRotating) 
            Movement();

        if (!camRotating && !camMoving)
            Zoom();
    }

    bool Movement()
    {
        if (Input.GetMouseButtonDown(moveButton))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            LayerMask cameraRaycasts = 1 << LayerMask.NameToLayer("CameraRaycasts");

            if (Physics.Raycast(ray, out hit, rayDistance, cameraRaycasts))
            {
                prevRaycastHit = hit.point;
                camMoving = true;
            }

            return true;
        }

        // While dragging the camera around
        else if (Input.GetMouseButton(moveButton) && camMoving)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            LayerMask cameraRaycasts = 1 << LayerMask.NameToLayer("CameraRaycasts");

            if (Physics.Raycast(ray, out hit, rayDistance, cameraRaycasts))
            {
                var mousePosition = hit.point;
                var movement = prevRaycastHit - mousePosition;
                cam.transform.position += movement;
            }

            return true;
        }

        // Stop dragging camera
        else if (Input.GetMouseButtonUp(moveButton))
        {
            camMoving = false;
            return true;
        }

        else return false;
    }

    bool Zoom()
    {
        var scrollVal = Input.GetAxis("Mouse ScrollWheel");

        if (scrollVal == 0)
            return false;

        if (scrollVal > 0 && (scrollVal + cam.orthographicSize) > minCamDistance)
        {
            cam.orthographicSize += scrollVal * scrollSpeed * -1f;
        }

        if (scrollVal < 0 && (scrollVal + cam.orthographicSize) < maxCamDistance)
        {
            cam.orthographicSize += scrollVal * scrollSpeed * -1f;
        }

        return true;
    }

    void Rotation_MouseWheel()
    {
        if (Input.GetMouseButtonDown(rotateButton))
        {
            RaycastHit hit;
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            LayerMask cameraRaycasts = 1 << LayerMask.NameToLayer("CameraRaycasts");

            if (Physics.Raycast(ray, out hit, rayDistance, cameraRaycasts))
            {
                prevMousePosition = Input.mousePosition;
                rotationPoint = hit.point;
                camRotating = true;
            }
        }

        else if (Input.GetMouseButton(rotateButton) && camRotating)
        {
            var deltaMousePosition = Input.mousePosition - prevMousePosition;
            var deltaX = deltaMousePosition.x / cam.pixelWidth;
            var rotateDegrees = Time.deltaTime * rotateSpeed * 1000 * deltaX;
            cam.transform.RotateAround(rotationPoint, Vector3.up, rotateDegrees);
            prevMousePosition = Input.mousePosition;
        }

        else if (Input.GetMouseButtonUp(rotateButton) && camRotating)
        {
            camRotating = false;
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

    //void Rotation()
    //{
    //    if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        remainingRotation += 90;

    //        if (remainingRotation > 360)
    //            remainingRotation = 360;
    //    }

    //    else if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        remainingRotation -= 90;

    //        if (remainingRotation < -360)
    //            remainingRotation = -360;
    //    }

    //    Rotate();
    //}
    //private void Rotate()
    //{
    //    RaycastHit hit;
    //    Ray ray = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
    //    LayerMask cameraRaycasts = 1 << LayerMask.NameToLayer("CameraRaycasts");

    //    if (Physics.Raycast(ray, out hit, rayDistance, cameraRaycasts))
    //    {
    //        var rotationStep = Time.deltaTime * rotateSpeed * remainingRotation;
    //        var orbitDistance = 2f * Mathf.PI * hit.distance;


    //        if (remainingRotation > 0)
    //        {
    //            if (Mathf.Abs(rotationStep) > Mathf.Abs(remainingRotation))
    //                rotationStep = remainingRotation;

    //            camera.transform.RotateAround(hit.point, Vector3.up, rotationStep);
    //            remainingRotation -= rotationStep;
    //        }

    //        else if (remainingRotation < 0)
    //        {
    //            if (Mathf.Abs(rotationStep) > Mathf.Abs(remainingRotation))
    //                rotationStep = remainingRotation;

    //            camera.transform.RotateAround(hit.point, Vector3.up, -rotationStep);
    //            remainingRotation += rotationStep;
    //        }
    //    }
    //}
}
