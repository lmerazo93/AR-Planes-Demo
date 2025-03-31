using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;


public class Drag1 : MonoBehaviour
{
    public Transform draggable;
    ARRaycastManager raycastManager;

    List<ARRaycastHit> hits = new();

    void Start()
    {
        EnhancedTouchSupport.Enable();
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Touch.activeTouches.Count == 1)
        {
            Touch myTouch = Touch.activeTouches[0];
            if (myTouch.phase == TouchPhase.Began || myTouch.phase == TouchPhase.Moved)
            {
                DragObj(myTouch.screenPosition);
            }
        }
    }

    void DragObj(Vector2 pos)
    {
        if (raycastManager.Raycast(pos, hits, TrackableType.PlaneWithinPolygon))
        {
            draggable.position = hits[0].pose.position;
        }
    }
}
