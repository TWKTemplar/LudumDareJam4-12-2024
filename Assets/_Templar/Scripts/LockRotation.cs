using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    public Player player;
    public bool LookingRight = true;
    public Vector3 RightRot = new Vector3(-90,0,0);
    public Vector3 LeftRot = new Vector3(-90,180,0);
    public void AssignPlayer()
    {
        if (player == null) player = FindObjectOfType<Player>();
    }
    private void OnValidate()
    {
        AssignPlayer();
    }
    private void Start()
    {
        AssignPlayer();
    }
    private void LateUpdate()
    {
        AlignToRot();
    }
    private void OnDrawGizmos()
    {
        AlignToRot();
    }
    public void AlignToRot()
    {
        LookingRight = (player.transform.position.x - transform.position.x) >0 ;
        transform.rotation = Quaternion.Euler(LookingRight ? RightRot : LeftRot);
    }
}
