using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    public GameObject enemySpawnButton;
    public Text pausedText;
    public Text resumeText;
    public Text resumeKeyText;
    public Text restartText;
    public Text restartKeyText;

    public void SetEnabled(bool state)
    {
        pausedText.enabled = state;
        resumeText.enabled = state;
        resumeKeyText.enabled = state;
        restartText.enabled = state;
        restartKeyText.enabled = state;
        enemySpawnButton.SetActive(state);
    }
}
