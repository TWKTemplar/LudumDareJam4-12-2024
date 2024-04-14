using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropKick : MonoBehaviour
{
    public Enemy enemy;
    public Rigidbody lastTouchRb;
    private void OnValidate()
    {
        if (enemy == null) enemy = GetComponent<Enemy>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(enemy.CurrentState != Enemy.EnemyState.Idle && collision.gameObject.CompareTag("Prop"))
        {
            Rigidbody propRb = null;
            if(collision.gameObject.TryGetComponent<Rigidbody>(out propRb))
            {
                PushAway(propRb);
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (enemy.CurrentState != Enemy.EnemyState.Idle && collision.gameObject.CompareTag("Prop"))
        {
            Rigidbody propRb = null;
            if (collision.gameObject.TryGetComponent<Rigidbody>(out propRb))
            {
                PushAway(propRb);
            }
        }
    }
    public void PushAway(Rigidbody propRb)
    {
        lastTouchRb = propRb;
        Debug.Log("Enemy Pushed Prop");
        Vector3 pushDirection = (propRb.transform.position - transform.position).normalized * enemy.PushForce*0.2f;
        propRb.AddForce(pushDirection, ForceMode.Impulse);
    }
}
