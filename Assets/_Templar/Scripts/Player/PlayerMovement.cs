using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [Range(0, 5)] public float Speed = 0.4f;
    [Header("Ref")]
    public Vector3 MovementDir;
    public Rigidbody rb;
    public Collider col;
    void Update()
    {
        MovementDir = CalculateMovementDirection();
        if (Input.GetButtonDown("Jump"))
        {
            MovementDir *= 2;
        }

        ApplyMovement(MovementDir);
    }
    public void ApplyMovement(Vector3 moveDir)
    {
        rb.MovePosition(transform.position + (moveDir*Time.deltaTime* Speed*100));
    }
    private void OnValidate()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (col == null) col = GetComponent<Collider>();
    }
    public Vector3 CalculateMovementDirection()
    {
        Vector3 movementDir = Vector3.zero;
        movementDir.x += Input.GetAxis("Horizontal");
        movementDir.z += Input.GetAxis("Vertical");
        return movementDir;
    }
}
