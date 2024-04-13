using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDetector : MonoBehaviour
{
    [Header("ReadOnly Debug Data")]
    public bool PlayerIsWithinRange = false;
    [Header("Events")]
    public UnityEvent OnPlayerTriggerEnter;
    public UnityEvent OnPlayerTriggerExit;
    public UnityEvent OnPlayerInteract;
    [Header("Optional Things For you :)")]
    public GameObject DestoryMeOnInteract;
    public GameObject SpawnMeOnInteract;
    public AudioSource AudioSourcePrefabToSpawnInOnInteract;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIsWithinRange = true;
            OnPlayerTriggerEnter.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIsWithinRange = false;
            OnPlayerTriggerExit.Invoke();
        }
    }
    private void Update()
    {
        if (PlayerIsWithinRange && Input.GetButtonDown("Jump"))
        {
            OnPlayerInteract.Invoke();
            Debug.Log("player Interacted With Me!");
            if (AudioSourcePrefabToSpawnInOnInteract != null) Instantiate(AudioSourcePrefabToSpawnInOnInteract, transform.position, Quaternion.identity);
            if (SpawnMeOnInteract != null) Instantiate(SpawnMeOnInteract, transform.position, Quaternion.identity);
            if (DestoryMeOnInteract != null) Destroy(DestoryMeOnInteract);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
        }
    }
}
