using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenUI : MonoBehaviour
{
    public Slider playerHPSlider;
    public Player player;
    public GameManager gameManager;
    public void OnValidate()
    {
        if (gameManager == null) gameManager = FindObjectOfType<GameManager>();
        if (player == null) player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        UpdateSlider();
    }
    public void UpdateSlider()
    {
        playerHPSlider.maxValue = player.MaxHealth;
        var hp = player.Health;
        if (hp <= 0) hp = 1;
        playerHPSlider.value = hp;
    }
}
