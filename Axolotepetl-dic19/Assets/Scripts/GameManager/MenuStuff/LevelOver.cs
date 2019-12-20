using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOver : MonoBehaviour
{
    public TextMeshProUGUI timeLeft;
    public TextMeshProUGUI livesLeft;
    public TextMeshProUGUI clientsText;
    public TextMeshProUGUI deadClientsText;

    public GameObject progress;
    public GameObject retry;
    public GameObject menu;

    public Chef cheffy;
    public Timer timer;

    public string menuSceneName;

    public string fadeTo;
    public int nextLevelUnlocked;

    public SceneFader sceneFader;

    private float time;

    private int min;
    private int sec;

    private void OnEnable()
    {
        _ = StartCoroutine(AnimateText());
        _ = StartCoroutine(AnimateOtherText());

        time = timer.totalTime;

        EndTime();

        timeLeft.text = string.Format("{0:0}:{1:00}", min, sec);
    }

    private IEnumerator AnimateText()
    {
        int client = 0;
        clientsText.text = "0";

        yield return new WaitForSeconds(0.5f);

        Debug.Log("Alive: " + Chef.SatisfiedClients);

        while (client < Chef.SatisfiedClients)
        {
            client++;
            clientsText.text = client.ToString();

            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.5f);

        _ = StartCoroutine(AnimateLives());
    }

    private IEnumerator AnimateOtherText()
    {
        int deadClients = Chef.clients.Length;
        deadClientsText.text = deadClients.ToString();

        yield return new WaitForSeconds(0.5f);

        Debug.Log("Dead: " + Chef.FaintedClients);

        while (deadClients > Chef.FaintedClients)
        {
            deadClients--;
            deadClientsText.text = deadClients.ToString();

            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator AnimateLives()
    {
        int lives = Chef.Lives;
        livesLeft.text = lives.ToString();

        yield return new WaitForSeconds(0.5f);

        CountLives();

        Debug.Log("Lives: " + Chef.Lives);

        while (lives > Chef.Lives && lives >= 0)
        {
            lives--;
            livesLeft.text = lives.ToString();

            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.5f);

        menu.SetActive(true);

        if (Chef.Lives > 0)
        {
            PlayerPrefs.SetInt("levelReached", nextLevelUnlocked);
            progress.SetActive(true);
        }

        if (Chef.Lives <= 0)
            retry.SetActive(true);
    }

    public void CountLives()
    {
        Chef.Lives -= Chef.FaintedClients;
    }

    public void EndTime()
    {
        min = Mathf.FloorToInt(time / 60F);
        sec = Mathf.FloorToInt(time - (min * 60));
    }

    public void NextLevel()
    {
        sceneFader.FadeTo(fadeTo);
    }

    public void SecondChance()
    {
        if (cheffy.chefIndex == 0)
            PlayerPrefs.SetInt("ChefSelected", 1);

        if (cheffy.chefIndex == 1)
            PlayerPrefs.SetInt("ChefSelected", 0);

        Chef.Lives = 5;
        GameMaster.secondChance = true;

        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
