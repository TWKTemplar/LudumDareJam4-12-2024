using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator myAnim;
    public PlayerMovement playerMovement;
    private void Update()
    {
        if(Mathf.Abs(playerMovement.WishDir.x) > 0.1f)
        {
            myAnim.SetBool("LookingLeft", !(playerMovement.WishDir.x > 0f));
        }
        myAnim.SetBool("Dashing", playerMovement.Dashing);
    }
}
