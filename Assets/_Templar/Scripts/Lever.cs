using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public MeshRenderer myRend;
    public TriggerDetector triggerDetector;
    public Material GlowLever;
    public Material NoGlowLever;
    public void PlayerClose()
    {
        myRend.material = GlowLever;
    }
    public void PlayerFar()
    {
        myRend.material = NoGlowLever;
    }
    public void PlayerInteract()
    {
      myRend.material = NoGlowLever;
      gameObject.transform.Rotate(Vector3.forward, 180);
      Invoke("DisableTrigger", 0.05f);
    }
    public void DisableTrigger()
    {
        triggerDetector.enabled = false;

    }
}
