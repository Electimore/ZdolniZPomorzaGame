using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float maxDurability;
    public float durability;

    private void Start()
    {
        durability = 0;
    }

    public void RestoreShield()
    {
        durability = maxDurability;
    }
}