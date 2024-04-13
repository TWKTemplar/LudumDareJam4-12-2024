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
            if (playerMovement.WishDir.x > 0f)
            {
                myAnim.SetBool("LookingLeft", false);
            }
            else 
            {
                myAnim.SetBool("LookingLeft", true);
            }
        }
    }
}
