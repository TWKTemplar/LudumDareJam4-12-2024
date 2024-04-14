using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //hi :)
    public MusicFade musicFade;
    public SummonSpawner summonSpawner;
    public Player player;
    public bool PlayerHasSummons = false;
    public bool PlayerHasMoreThan1HP = false;

    public void UpdateMusic()
    {
        musicFade.AudioSourceVolumesToggles[1] = PlayerHasSummons;
        musicFade.AudioSourceVolumesToggles[2] = !PlayerHasMoreThan1HP;
    }
    private void OnValidate()
    {
        GetThings();   
    }
    private void Start()
    {
        GetThings();
        UpdateMusic();
    }
    private void GetThings()
    {
        if (musicFade == null) musicFade = FindObjectOfType<MusicFade>();
        if (summonSpawner == null) summonSpawner = FindObjectOfType<SummonSpawner>();
        if (player == null) player = FindObjectOfType<Player>();
    }
}
