using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [Header("Settings")]
    public bool DisableLevelAfterInteract = true;
    [Header("Ref")]
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
         gameObject.transform.Rotate(Vector3.forward, 180);
        if (DisableLevelAfterInteract)
        {
            myRend.material = NoGlowLever;
            triggerDetector.DisableTriggerDetection();
        }
    }
    
}
