using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    //Source: http://www.unity3d-france.com/unity/phpBB3/viewtopic.php?t=16239

    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private float zPosition;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clic");
            Vector3 wordPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zPosition));
            Instantiate(objectPrefab, wordPos, Quaternion.identity);
        }
    }

    void OnGUI()
    {
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.EndArea();


    }
}
