using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundKit : MonoBehaviour
{
    public enum PlayType {PlayAllSounds,PlayRandomSound}
    public PlayType playType = PlayType.PlayAllSounds;
    public AudioSource[] audioSources;
    public void Play()
    {
        if(playType == PlayType.PlayAllSounds)
        {
            foreach (var ass in audioSources)
            {
                if(!ass.isPlaying) ass.Play();
            }
        }
        else if (playType == PlayType.PlayRandomSound)
        {
            var i= Random.Range(0, audioSources.Length - 1);
            if(audioSources[i].isPlaying) i = Random.Range(0, audioSources.Length - 1);
            if(audioSources[i].isPlaying) i = Random.Range(0, audioSources.Length - 1);
            if(audioSources[i].isPlaying) i = Random.Range(0, audioSources.Length - 1);
            audioSources[i].Play();
        }
        
    }
    public void PlayOnce()
    {
        foreach (var ass in audioSources) ass.loop = false;
        Play();
    }
    public void PlayLoop()
    {
        foreach (var ass in audioSources) ass.loop = true;
        Play();
    }
    public void Stop()
    {
        foreach (var ass in audioSources) ass.Stop();
    }

}
