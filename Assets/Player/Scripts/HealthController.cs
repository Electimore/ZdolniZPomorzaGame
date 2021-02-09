using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float hp;
    public GameObject player;
    public GameObject playerspawner;
    public Animator doggoController;

    bool paused = false;
    bool attacked;
    Animator anim;
    public bool alive;
    public bool movable;

    private GamePauseUI gamePauseUI;
    private GameOverUI gameOverUI;
    private HUD hud;


    void Start()
    {
        doggoController.Play("DieRecover");
        hp = 20f;
        attacked = false;
        anim = GetComponent<Animator>();
        alive = true;
        gameOverUI = player.GetComponent<GameOverUI>();
        gameOverUI.SetEnabled(false);
        gamePauseUI = player.GetComponent<GamePauseUI>();
        gamePauseUI.SetEnabled(false);
        hud = player.GetComponent<HUD>();
        player.transform.position = playerspawner.transform.position;
        movable = true;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "weapon_1" && attacked == false) //1 DMG weapon hit
        {
            hp -= 1;
            attacked = true;
            anim.SetTrigger("Gethit");
        }
        else if (collision.gameObject.tag == "weapon_5" && attacked == false) //.5 DMG weapon hit
        {
            hp -= 0.5f;
            attacked = true;
            anim.SetTrigger("Gethit");
        }
        else if (collision.gameObject.tag == "weapon_2" && attacked == false) //2 DMG weapon hit
        {
            hp -= 2;
            attacked = true;
            anim.SetTrigger("Gethit");
        }
        if (hp <= 0 && alive) //Checks if player is alive
        {
            anim.SetTrigger("Die");
            alive = false;
            gameOverUI.SetEnabled(true);
            movable = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        attacked = false;
    }


    void Update()
    {
        hud.SetHealth(hp);
        if (Input.GetKeyDown(KeyCode.R))
        {
            Start();
        }

        if (alive && Input.GetKeyDown(KeyCode.Escape))
        {
            movable = paused;
            doggoController.enabled = paused;
            gamePauseUI.SetEnabled(!paused);

            paused = !paused;
        }
    }
}
