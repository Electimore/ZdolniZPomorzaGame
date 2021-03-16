using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Inventory UI

public class Inventory : MonoBehaviour
{
    public List<GameObject> weapons;
    public List<GameObject> slots;
    public int current;
    public int maxInventorySize;

    public GameObject player;
    public GameObject shield;
    public GameObject followTarget;

    private RaycastHit lookingAt;

    void Start()
    { 
        weapons[current].SetActive(true);
    }

    void Update()
    {
        if (Input.GetButtonDown("Weapon1"))
        {
            SetWeapon(0);
        }
        else if (Input.GetButtonDown("Weapon2"))
        {
            SetWeapon(1);
        }
        else if (Input.GetButtonDown("Weapon3"))
        {
            SetWeapon(2);
        }
        
        Vector3 forward = followTarget.transform.TransformDirection(Vector3.forward) * 10;

        if (Physics.Raycast(followTarget.transform.position, (forward), out lookingAt))
        {
            CheckIfLookingAtWeapon();
            CheckIfLookingAtShield();
        }
    }

    void SetWeapon(int wpIndex)
    {
        if (wpIndex != current && wpIndex < weapons.Count)
        {
            weapons[current].SetActive(false);
            current = wpIndex;
            weapons[current].SetActive(true);
        }
    }

    void CheckIfLookingAtWeapon()
    {
        if (GameObject.Find(lookingAt.collider.gameObject.name).CompareTag("weapon"))
        {
            GameObject.Find(lookingAt.collider.gameObject.name).GetComponent<ThingyOutliner>().LookingAtObject();
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickRightHand(lookingAt.collider.gameObject);
                ReplaceUIWeapons();
            }
        }
    }

    void CheckIfLookingAtShield()
    {
        if (GameObject.Find(lookingAt.collider.gameObject.name).CompareTag("shield"))
        {
            GameObject.Find(lookingAt.collider.gameObject.name).GetComponent<ThingyOutliner>().LookingAtObject();
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickLeftHand(lookingAt.collider.gameObject);
            }
        }
    }

    void PickRightHand(GameObject weaponToPick)
    {
        if (weapons.Count >= maxInventorySize)
        {
            GameObject oldWeapon = weapons[current];

            oldWeapon.transform.parent = weaponToPick.transform.parent;
            oldWeapon.transform.position = weaponToPick.transform.position;
            oldWeapon.transform.rotation = weaponToPick.transform.rotation;
            oldWeapon.transform.localScale = weaponToPick.transform.localScale;
            oldWeapon.tag = "weapon";

            weapons[current] = weaponToPick;
        }
        else
        {
            weapons.Add(weaponToPick);
            SetWeapon(weapons.Count - 1);
        }
        GameObject weaponSlot = GameObject.Find("Weapon");
        weaponToPick.transform.parent = weaponSlot.transform;
        weaponToPick.transform.localPosition = Vector3.zero;
        weaponToPick.transform.rotation = new Quaternion(0, 0, 0, 0);
        weaponToPick.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        weaponToPick.tag = "weapon_picked";
    }

    void PickLeftHand(GameObject shieldToPick)
    {
        shield.transform.parent = shieldToPick.transform.parent;
        shield.transform.position = shieldToPick.transform.position;
        shield.transform.rotation = shieldToPick.transform.rotation;
        shield.transform.localScale = shieldToPick.transform.localScale;
        shield.tag = "shield";

        shield = shieldToPick;

        player.GetComponent<PlayerController>().OnShieldPickup();

        GameObject shieldSlot = GameObject.Find("Shield");
        shieldToPick.transform.parent = shieldSlot.transform;
        shieldToPick.transform.localPosition = Vector3.zero;
        shieldToPick.transform.localRotation = Quaternion.Euler(0, -110, 180);
        shieldToPick.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        shieldToPick.tag = "shield_picked";
    }

    void ReplaceUIWeapons()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            GameObject toReplace = slots[i];
            slots[i] = Instantiate(weapons[i]);
            slots[i].transform.parent = toReplace.transform.parent;
            slots[i].transform.localPosition = toReplace.transform.localPosition;
            slots[i].transform.localRotation = toReplace.transform.localRotation;
            slots[i].transform.localScale = toReplace.transform.localScale;
            slots[i].SetActive(true);
            Destroy(toReplace);
        }
    }

    public Weapon GetCurrentWeapon()
    {
        return weapons[current].GetComponent<Weapon>();
    }
}