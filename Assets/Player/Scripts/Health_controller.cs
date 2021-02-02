using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_controller : MonoBehaviour
{
    string toSend;
    public float hp;
    public Text health;
    public Text gameOverTxt;
    public Text gameRestartTxtOne;
    public Text gameRestartTxtTwo;
    public Text gPT1;
    public Text gPT2;
    public Text gPT3;
    public Text gPT4;
    public Text gPT5;
    public GameObject player;
    public GameObject playerspawner;
    public Animator doggoController;

    bool paused = false;
    bool attacked;
    Animator anim;
    public bool alive;
    public bool movable;


    // Start is called before the first frame update
    void Start()
    {
        Start_set();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "weapon_1" && attacked == false)
        {
            hp -= 1;
            attacked = true;
            anim.SetTrigger("Gethit");
        }
        else if (collision.gameObject.tag == "weapon_5" && attacked == false)
        {
            hp -= 0.5f;
            attacked = true;
            anim.SetTrigger("Gethit");
        }
        else if (collision.gameObject.tag == "weapon_2" && attacked == false)
        {
            hp -= 2;
            attacked = true;
            anim.SetTrigger("Gethit");
        }
        if (hp <= 0 && alive)
        {
            anim.SetTrigger("Die");
            alive = false;
            gameOverTxt.enabled = true;
            gameRestartTxtOne.enabled = true;
            gameRestartTxtTwo.enabled = true;
            movable = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
            attacked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <10 && hp >=0)
        {
            toSend = "HP: 0" + hp.ToString();
        }
        else
        {
            toSend = "HP: " + hp.ToString();
        }
        health.text = toSend;
        if(Input.GetKeyDown(KeyCode.R))
        {
            Start_set();
        }
        if(alive == true && Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            movable = false;
            gPT1.enabled = true;
            gPT2.enabled = true;
            gPT3.enabled = true;
            gPT4.enabled = true;
            gPT5.enabled = true;
            paused = true;
        } else if (alive == true && Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            movable = true;
            gPT1.enabled = false;
            gPT2.enabled = false;
            gPT3.enabled = false;
            gPT4.enabled = false;
            gPT5.enabled = false;
            paused = false;
        }
    }

    private void Start_set()
    {
        doggoController.Play("DieRecover");
        hp = 20f;
        attacked = false;
        anim = GetComponent<Animator>();
        alive = true;
        gameOverTxt.enabled = false;
        gameRestartTxtOne.enabled = false;
        gameRestartTxtTwo.enabled = false;
        gPT1.enabled = false;
        gPT2.enabled = false;
        gPT3.enabled = false;
        gPT4.enabled = false;
        gPT5.enabled = false;
        player.transform.position = playerspawner.transform.position;
        movable = true;
    }
}
