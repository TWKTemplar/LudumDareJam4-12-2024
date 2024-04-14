using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class PostProcessControl : MonoBehaviour
{
    public PostProcessVolume DeadPlayerOverlay;
    public Player player;
    public float hpPercent;
    public float WeightTarget;
    public void SetDeadPercent(float percent)
    {
        DeadPlayerOverlay.weight = percent;
    }
    public void UpdatePerecnt()
    {
        hpPercent = player.Health / player.MaxHealth;
        hpPercent *= 0.5f;
        hpPercent += 0.5f;
        if(player.Health <= 0)
            hpPercent = 0;
        WeightTarget = 1 - hpPercent;
        DeadPlayerOverlay.weight = Mathf.Lerp(DeadPlayerOverlay.weight, WeightTarget, 0.01f);
    }
    private void Update()
    {
        UpdatePerecnt();
    }
}
