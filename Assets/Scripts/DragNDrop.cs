using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DragNDrop : MonoBehaviour
{
    public string draggingTag;
    public ARRaycastManager raycastManager;

    private Vector3 dis;
    private float posX;
    private float posY;
    private float posZ;

    private bool touched = false;
    private bool dragging = false;

    private Transform toDrag;
    private Rigidbody toDragRigidbody;
    private Vector3 previousPosition;

    void Update()
    {
        if (!(Input.touchCount == 1))
        {
            dragging = false;
            touched = false;
            if (toDragRigidbody)
            {
                SetFreeProperties(toDragRigidbody);
            }
            return;
        }

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            Vector2 touchPosition = touch.position;
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == draggingTag)
            {
                toDrag = hit.transform;
                previousPosition = toDrag.position;
                toDragRigidbody = toDrag.GetComponent<Rigidbody>();

                // dis = Camera.main.WorldToScreenPoint(previousPosition);
                // posX = touch.position.x - dis.x;
                // posY = touch.position.y - dis.y;
                // posZ = Input.GetTouch(0).position.z;
                

                SetDraggingProperties(toDragRigidbody);

                touched = true;
            }
        }

        if (touched && touch.phase == TouchPhase.Moved)
        {
            dragging = true;

            Vector3 touchPosition = touch.position;
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Update the position of the object based on the hit point
                toDrag.position = hit.point;
            }

            // float posXNow = touch.position.x - posX;
            // float posYNow = touch.position.y - posY;
            // float posZNow = Input.GetTouch(0).position.z;
            // float deltaZ = posZNow - posZ;
            // Vector3 curPos = new Vector3(posXNow, posYNow, dis.z+ deltaZ);

            // if (toDragRigidbody != null)
            // {
            //     Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos) - previousPosition;
            //     worldPos = new Vector3(worldPos.x, worldPos.y, worldPos.z);

            //     toDragRigidbody.velocity = worldPos / (Time.deltaTime * 10);
            //     posZ = posZNow;
            // }

            // previousPosition = toDrag.position;
        }

        if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            dragging = false;
            touched = false;
            previousPosition = new Vector3(0.0f, 0.0f, 0.0f);
            SetFreeProperties(toDragRigidbody);
        }
    }

    private void SetDraggingProperties(Rigidbody rb)
    {
        rb.useGravity = false;
        rb.drag = 10;
    }

    private void SetFreeProperties(Rigidbody rb)
    {
        rb.useGravity = true;
        rb.drag = 8;
    }
}
