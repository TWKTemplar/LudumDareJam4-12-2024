using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDetector : MonoBehaviour
{
    [Header("ReadOnly Debug Data")]
    public bool TargetTagIsWithinRange = false;
    public bool DoTriggerDetection = true;
    public string TargetTag = "Player";
    [Header("Events")]
    public UnityEvent OnPlayerTriggerEnter;
    public UnityEvent OnPlayerTriggerExit;
    public UnityEvent OnPlayerInteract;
    [Header("Optional Things For you :)")]
    public GameObject DestoryMeOnInteract;
    public GameObject SpawnMeOnInteract;
    public AudioSource AudioSourcePrefabToSpawnInOnInteract;
    public void DisableTriggerDetection()
    {
        DoTriggerDetection = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!DoTriggerDetection) return;
        if (other.CompareTag(TargetTag))
        {
            TargetTagIsWithinRange = true;
            OnPlayerTriggerEnter.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!DoTriggerDetection) return;
        if (other.CompareTag(TargetTag))
        {
            TargetTagIsWithinRange = false;
            OnPlayerTriggerExit.Invoke();
        }
    }
    private void Update()
    {
        if (!DoTriggerDetection) return;
        if (TargetTagIsWithinRange && TargetTag == "Player" && Input.GetButtonDown("Jump"))
        {
            OnPlayerInteract.Invoke();
            Debug.Log("player Interacted With Me! : " + gameObject);
            if (AudioSourcePrefabToSpawnInOnInteract != null) Instantiate(AudioSourcePrefabToSpawnInOnInteract, transform.position, Quaternion.identity);
            if (SpawnMeOnInteract != null) Instantiate(SpawnMeOnInteract, transform.position, Quaternion.identity);
            if (DestoryMeOnInteract != null) Destroy(DestoryMeOnInteract);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!DoTriggerDetection) return;
        if (other.CompareTag(TargetTag))
        {
            
        }
    }
}
