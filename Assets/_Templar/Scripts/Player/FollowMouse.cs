
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public Camera MouseCamera;
    [Range(0, 20)] public float DistanceFromCamera = 10;
    private void Update() {
        transform.position = MouseWorldPos();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(MouseWorldPos(), 0.1f);
    }
    public Vector3 MouseWorldPos()
    {
        Vector3 screenPosDepth = Input.mousePosition;
        screenPosDepth.z = DistanceFromCamera;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(screenPosDepth);
        return mousePos;
    }
}
