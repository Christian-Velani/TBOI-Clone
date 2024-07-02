using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float zoomIn = Input.GetAxis("MinimapZoomIn");
        float zoomOut = Input.GetAxis("MinimapZoomOut");

        if (zoomIn > 0) { ZoomIn(); }
        if (zoomOut > 0) { ZoomOut(); }
    }

    void ZoomIn()
    {
        GetComponent<Camera>().orthographicSize += 5;
    }

    void ZoomOut()
    {
        GetComponent<Camera>().orthographicSize -= 5;
    }
}
