using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float zoom;
    private float zoomMultiplier = 4f;
    private float minZoom = 3f;
    private float maxZoom = 6f;
    private float velocity = 0f;
    private float smoothTime = 1f;

    public float fixedRotation = 270;

    Transform cameraTransform;
    [SerializeField] Transform playerTransform;
    Camera cam;
    void Awake()
    {
        cam = GetComponent<Camera>();
        cameraTransform = cam.transform;
    }

    void Update()
    {
        cameraTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, cameraTransform.position.z);
        cameraScale();
        Vector3 eulerAngles = playerTransform.eulerAngles;
        transform.eulerAngles = new Vector3(eulerAngles.x, eulerAngles.y, fixedRotation);
    }

    void cameraScale()
    {
        float input = Input.GetAxis("Vertical");
        zoom += input * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
    }
}
