using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPlayerDetector : MonoBehaviour
{
    private GameController gameController;

    private void Start()
    {
        gameController = GameObject.Find("GameMaster").GetComponent<GameController>();
    }

    public void Visible(bool visible)
    {
        this.gameObject.SetActive(true);
    }
}
