using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MultiDrag1 : MonoBehaviour
{
    public LayerMask layer;
    private Transform selected;
    ARRaycastManager raycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        raycastManager = GetComponent<ARRaycastManager>();
        //selected = GameObject.FindGameObjectWithTag("Grab").transform;
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                /*****/

                Ray ray = mainCam.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit hit, 20, layer))
                {
                    if (selected != null)
                    {
                        selected.GetComponent<Highlight>().Selected(false);
                    }
                    selected = hit.collider.transform;
                    selected.GetComponent<Highlight>().Selected(true);

                }
                /****/
                else
                {
                    DragObj(touch.position);
                }
            }

            if (touch.phase == TouchPhase.Moved)
            {
                DragObj(touch.position);
            }
        }
    }

    void DragObj(Vector2 pos)
    {
        if (selected != null && raycastManager.Raycast(pos, hits, TrackableType.PlaneWithinPolygon))
        {
            selected.position = hits[0].pose.position;
        }
    }
}
