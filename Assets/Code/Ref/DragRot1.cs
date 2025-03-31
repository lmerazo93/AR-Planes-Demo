using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;


public class DragRot1 : MonoBehaviour
{
    float rotSpeed = 1;
    float scaleSpeed = .001f;
    Vector2 startPos1;
    Vector2 startPos2;
    float startDist;
    public Transform draggable;

    ARRaycastManager raycastManager;

    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        EnhancedTouchSupport.Enable();
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Touch.activeTouches.Count == 1)
        {
            Touch touch1 = Touch.activeTouches[0];
            if (touch1.phase == TouchPhase.Began || touch1.phase == TouchPhase.Moved)
            {
                DragObj(touch1.screenPosition);
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch touch1 = Touch.activeTouches[0];
            Touch touch2 = Touch.activeTouches[1];

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                startPos1 = touch1.screenPosition;
                startPos2 = touch2.screenPosition;
                startDist = Vector2.Distance(startPos1, startPos2);
                //DragObj(Vector2.Lerp(startPos1, startPos2, .5f));
            }

            if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                if (Vector2.Angle(touch1.screenPosition - startPos1, touch2.screenPosition - startPos2) < 30)
                {
                    RotObj(touch1.delta.x);
                }
                else
                {
                    ScaleObject(touch1.screenPosition, touch2.screenPosition);
                }

                startDist = Vector2.Distance(touch1.screenPosition, touch2.screenPosition);
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

    void RotObj(float deltaX)
    {
        draggable.Rotate(0, -deltaX * rotSpeed, 0, Space.Self);
    }

    void ScaleObject(Vector2 pos1, Vector2 pos2)
    {
        float curDist = Vector2.Distance(pos1, pos2);

        draggable.localScale += (curDist - startDist) * scaleSpeed * Vector3.one;
        if (draggable.localScale.x < .01f)
        {
            draggable.localScale = .01f * Vector3.one;
        }
    }
}
