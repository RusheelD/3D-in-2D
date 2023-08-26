using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam;
    public bool flatMode;
    public MapData3D mapData;
    Vector3 origin = new Vector3(0, 0, -10);
    Vector2 rotation = Vector2.zero;
    float sensitivity = 5;
    float maxRotationY = 88;

    void Start()
    {
        cam = GetComponent<Camera>();
        flatMode = true;
    }

    void Update()
    {
        if (flatMode)
        {
            cam.orthographic = true;
            transform.position = origin;
            transform.rotation = Quaternion.identity;
        } else
        {
            Vector3 f = mapData.player.transform.forward;
            cam.orthographic = false;
            transform.position = mapData.player.transform.position + (Vector3.up / 4f) - (new Vector3(f.x, 0, f.z)/3f); 
            rotation.x += Input.GetAxis("Mouse X") * sensitivity;
            rotation.y += Input.GetAxis("Mouse Y") * sensitivity;
            rotation.y = Mathf.Clamp(rotation.y, -maxRotationY, maxRotationY);
            var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
            var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

            transform.localRotation = xQuat * yQuat;
        }
    }

    public void Reset()
    {
        transform.rotation = Quaternion.identity;
        rotation = Vector2.zero;
    }
}
