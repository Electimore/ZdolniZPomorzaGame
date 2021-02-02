using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text health;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void SetHealth(float hp)
    {
        health.text = "HP:" + hp.ToString("00");
    }
}
