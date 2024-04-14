using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
     public float ChangeDirectionSpeed= 0.2f;
    [Range(0, 5)] public float BaseSpeed = 0.4f;
    [Range(0, 5)] public float DashSpeed = 0.8f;
    [Range(0, 1)] public float DashLengthInSeconds = 0.5f;
    [Range(0, 2)] public float DashCoolDownAfterEndOfDash = 0.5f;
    [Header("ReadOnly")]
    public bool Dashing = false;
    public bool CantStartDash = false;
    [Header("Ref")]
    public Vector3 WishDir;
    public Vector3 TargetVelocity;
    public Rigidbody rb;
    public Collider col;
    void Update()
    {
        if (Input.GetButtonDown("Jump")) StartDash();
        WishDir = CalculateMovementDirection();
    }
    private void FixedUpdate()
    {
        ApplyMovement(WishDir);
    }
    public void StartDash()
    {
        if (CantStartDash) return;
        Dashing = true;
        CantStartDash = true;
        Invoke("EndDash", DashLengthInSeconds);
        Invoke("EndDashCoolDown", DashLengthInSeconds + DashCoolDownAfterEndOfDash);
    }
    public void EndDash()
    {
        if(Dashing == true) Dashing = false;
    }
    public void EndDashCoolDown()
    {
        if (CantStartDash == true) CantStartDash = false;
    }
    public void ApplyMovement(Vector3 moveDir)
    {
        TargetVelocity = WishDir * (Dashing ? DashSpeed : BaseSpeed) * 10;
        if (WishDir.magnitude > 0.3f)
        {
            Vector3 velocity = rb.velocity;
            //Vector3 targetVelocity = Vector3.Lerp(rb.velocity, TargetVelocity, ChangeDirectionSpeed);
            rb.AddForce(Vector3.right * Mathf.Clamp(-velocity.x + TargetVelocity.x, -1, 1) * ChangeDirectionSpeed);
            rb.AddForce(Vector3.forward * Mathf.Clamp(-velocity.z + TargetVelocity.z,-1,1)* ChangeDirectionSpeed);
        }
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
        return movementDir.normalized;
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, rb.velocity);
    }
}
