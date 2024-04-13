using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [Range(0, 5)] public float Speed;
    [Header("Ref")]
    public Vector3 MovementDir;
    public Rigidbody rb;
    void Update()
    {
        MovementDir = CalculateMovementDirection();
        if (Input.GetAxis("Jump") > 0)
        {
            MovementDir *= 2;
        }

        ApplyMovement(MovementDir);
    }
    public void ApplyMovement(Vector3 moveDir)
    {
        rb.MovePosition(transform.position + (moveDir*Time.deltaTime* Speed*100));
    }
    public Vector3 CalculateMovementDirection()
    {
        Vector3 movementDir = Vector3.zero;
        movementDir.x += Input.GetAxis("Horizontal");
        movementDir.z += Input.GetAxis("Vertical");
        return movementDir;
    }
}
