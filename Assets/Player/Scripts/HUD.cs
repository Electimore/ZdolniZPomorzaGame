using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text health;
    public Text shieldIndicator;
    public Text enemyIndicator;
    public Text damageIndicator;
    public List<Image> weaponFrames;
    public List<RawImage> weaponFramesFill;
    public List<Text> weaponFramesText;
    GameObject player;

    private Inventory inventory;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<Inventory>();
    }

    void Update()
    {
        SetShield();
        SetFramesColors();
        SetWeaponDamage();
    }

    public void SetHealth(float hp, float maxHp)
    {
        health.text = "HP:" + hp.ToString("00") + "/" + maxHp.ToString("00");
    }

    public void SetShield()
    {
        Shield shield = player.GetComponent<Inventory>().shield.GetComponent<Shield>();
        shieldIndicator.text = "SHIELD:" + shield.durability.ToString("00") + "/" + shield.maxDurability.ToString("00");
    }

    public void SetEnemyCount(float enemies, float maxEnemies)
    {
        enemyIndicator.text = "ENEMIES:" + enemies.ToString("00") + "/" + maxEnemies.ToString("00");
    }

    public void SetWeaponDamage()
    {
        float damage = inventory.GetCurrentWeapon().damage;
        damageIndicator.text = "DAMAGE:" + damage.ToString();
    }

    public void SetFramesColors()
    {
        int frame = inventory.current;
        for (int i = 0; i < weaponFrames.Count; i++)
        {
            if (i == frame)
            {
                weaponFrames[i].color = new Color32(255, 240, 165, 255);
                weaponFramesFill[i].color = new Color32(255, 240, 165, 255);
                weaponFramesText[i].color = new Color32(255, 240, 165, 255);
            }
            else
            {
                weaponFrames[i].color = new Color32(255, 255, 255, 255);
                weaponFramesFill[i].color = new Color32(255, 255, 255, 255);
                weaponFramesText[i].color = new Color32(255, 255, 255, 255);
            }
        }
    }
}
