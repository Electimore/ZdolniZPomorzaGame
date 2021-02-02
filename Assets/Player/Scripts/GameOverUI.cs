using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public Text gameOverText;
    public Text gameOverRestartText;
    public Text gameOverRestartKeyText;

    public void SetEnabled(bool state)
    {
        gameOverText.enabled = state;
        gameOverRestartText.enabled = state;
        gameOverRestartKeyText.enabled = state;
    }
}
