using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechMouseLook
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;
    private Camera camera;

    public MechMouseLook(Camera camera)
    {
        this.camera = camera;
    }

    public void Update()
    {
        dragOrigin = Input.mousePosition;
         
        Vector3 pos = camera.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);

        camera.transform.Translate(move, Space.World);
    }
}
