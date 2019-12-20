using UnityEngine;

public class RetryAxolotl : MonoBehaviour
{
    public string menuSceneName;

    public GameObject UI;
    public GameMaster master;

    public Chef cheffy;
    public SceneFader sceneFader;

    private void OnEnable()
    {
        Time.timeScale = 0f;
        master.GUI = true;
    }

    public void Retry()
    {
        if (cheffy.chefIndex == 0)
            PlayerPrefs.SetInt("ChefSelected", 1);

        if (cheffy.chefIndex == 1)
            PlayerPrefs.SetInt("ChefSelected", 0);

        Chef.Lives = 5;
        GameMaster.secondChance = true;

        UI.SetActive(false);

        Time.timeScale = 1f;
        master.GUI = false;
    }

    public void NoRetry()
    {
        if (cheffy.chefIndex == 0)
            PlayerPrefs.SetInt("ChefSelected", 1);

        if (cheffy.chefIndex == 1)
            PlayerPrefs.SetInt("ChefSelected", 0);

        Chef.Lives = 5;
        GameMaster.secondChance = true;

        UI.SetActive(false);

        Time.timeScale = 1f;
        master.GUI = false;

        sceneFader.FadeTo(menuSceneName);
    }
}
