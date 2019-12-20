using UnityEngine;

public class GameOver : MonoBehaviour
{
    public string menuSceneName;

    public SceneFader sceneFader;

    private void OnEnable()
    {
        PlayerPrefs.SetInt("levelReached", 1);
    }

    public void MainMenu()
    {
        sceneFader.FadeTo(menuSceneName);

        Chef.Lives = 5;
        GameMaster.secondChance = false;
    }
}
