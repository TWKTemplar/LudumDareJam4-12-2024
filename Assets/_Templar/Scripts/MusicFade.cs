using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFade : MonoBehaviour
{
    [Range(0, 1)] public float MaxMusicVolume = 1;
    public bool[] AudioSourceVolumesToggles;
    [Range(0,1)]public float[] AudioSourceVolumes;
    [Range(0,0.25f)]public float LerpSpeed;
    public AudioSource[] audioSources;
    void Update()
    {
        UpdateLerps();
        ApplyVolumes();
    }
    public void ApplyVolumes()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = AudioSourceVolumes[i];
        }
    }
    private void OnValidate()
    {
        if(AudioSourceVolumes.Length != audioSources.Length)
            AudioSourceVolumes = new float[audioSources.Length];
        if (AudioSourceVolumesToggles.Length != audioSources.Length)
            AudioSourceVolumesToggles = new bool[audioSources.Length];
        for (int i = 0; i < audioSources.Length; i++)
        {
           //AudioSourceVolumes[i] = AudioSourceVolumesToggles[i] ? MaxMusicVolume : 0;
        }
    }
    public void UpdateLerps()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            AudioSourceVolumes[i] = Mathf.Lerp(AudioSourceVolumes[i],
                AudioSourceVolumesToggles[i] ? MaxMusicVolume : 0
                ,LerpSpeed);
        }
    }
}
