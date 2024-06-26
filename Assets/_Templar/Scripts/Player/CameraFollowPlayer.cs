using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player; // Player's transform
    public Vector3 offsetFromPlayer; // Offset from the player
    public Vector3 TargetPosition;
    [Range(0,10)]public float CameraSpeed = 0.5f;
    public Camera AcutalCamera;
    public bool DoDeathAnim = false;
    private float StartFOV = 45;
    public float DeathFOV = 20;
    private void Update()
    {
        if (DoDeathAnim)
        {
            AcutalCamera.fieldOfView = Mathf.Lerp(AcutalCamera.fieldOfView, DeathFOV, 0.002f);
        }
        else
        {
            AcutalCamera.fieldOfView = AcutalCamera.fieldOfView;

        }
    }
    void FixedUpdate()
    {
        CalculateDesiredPosition();
        float dist = Vector3.Distance(transform.position, TargetPosition);
        dist = Mathf.Clamp(dist* dist, 0.5f, 99);
        transform.position += (TargetPosition - transform.position).normalized * (dist * CameraSpeed * Time.deltaTime);
        transform.LookAt(player.position, Vector3.up);
        LerpCameraToSelf();
    }
    public void LerpCameraToSelf()
    {
        AcutalCamera.transform.position = Vector3.Lerp(AcutalCamera.transform.position, transform.position,0.1f);
        AcutalCamera.transform.rotation = Quaternion.Lerp(AcutalCamera.transform.rotation, transform.rotation, 0.1f);
    }
    [ContextMenu("Apply Movement")]
    public void ApplyMovement()
    {
        transform.position = TargetPosition;
        if(player != null) transform.LookAt(player.position, Vector3.up);
    }
    private void Start()
    {
        if(player == null) player = FindObjectOfType<Player>().transform;
        StartFOV = AcutalCamera.fieldOfView;
    }
    private void OnValidate()
    {
        ApplyMovement();
        StartFOV = AcutalCamera.fieldOfView;
    }
    public void CalculateDesiredPosition()
    {
        if (player == null) player = FindObjectOfType<Player>().transform;
        Vector3 desiredPosition = player.position + offsetFromPlayer;
        TargetPosition = new Vector3(desiredPosition.x, desiredPosition.y, desiredPosition.z);
    }
    private void OnDrawGizmos()
    {
        CalculateDesiredPosition();
       // Gizmos.color = Color.cyan;
       // Gizmos.DrawSphere((player.position + offsetFromPlayer), 0.1f);
       // Gizmos.color = Color.blue;
       // Gizmos.DrawSphere(TargetPosition, 0.1f);
       // Gizmos.DrawSphere(TargetPosition, 0.1f);
       // Gizmos.DrawLine(transform.position, TargetPosition);
    }
}

