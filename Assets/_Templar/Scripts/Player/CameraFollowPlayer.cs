using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player; // Player's transform
    public Vector3 offsetFromPlayer; // Offset from the player
    public Vector3 TargetPosition;
    [Range(0,10)]public float CameraSpeed = 0.5f;

    void Update()
    {
        CalculateDesiredPosition();
        float dist = Vector3.Distance(transform.position, TargetPosition);
        dist = Mathf.Clamp(dist* dist, 0.5f, 99);
        transform.position += (TargetPosition - transform.position).normalized * (dist * CameraSpeed * Time.deltaTime);
        transform.LookAt(player.position, Vector3.up);
    }
    [ContextMenu("Apply Movement")]
    public void ApplyMovement()
    {
        transform.position = TargetPosition;
        transform.LookAt(player.position, Vector3.up);
    }
    private void OnValidate()
    {
        ApplyMovement();
    }
    public void CalculateDesiredPosition()
    {
        Vector3 desiredPosition = player.position + offsetFromPlayer;
        TargetPosition = new Vector3(desiredPosition.x, desiredPosition.y, desiredPosition.z);
    }
    private void OnDrawGizmos()
    {
        CalculateDesiredPosition();
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere((player.position + offsetFromPlayer), 0.1f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(TargetPosition, 0.1f);
        Gizmos.DrawSphere(TargetPosition, 0.1f);
        Gizmos.DrawLine(transform.position, TargetPosition);
    }
}

