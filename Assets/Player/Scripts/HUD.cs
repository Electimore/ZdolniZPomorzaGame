using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text health;
    public Slider shieldIndicator;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Shield shield = player.GetComponent<Inventory>().shield.GetComponent<Shield>();
        shieldIndicator.value = shield.durability;
        shieldIndicator.maxValue = shield.maxDurability;
    }

    public void SetHealth(float hp)
    {
        health.text = "HP:" + hp.ToString("00");
    }
}
