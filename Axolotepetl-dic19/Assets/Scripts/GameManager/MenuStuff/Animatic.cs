using UnityEngine;
using UnityEngine.Video;

public class Animatic : MonoBehaviour
{
    public string sceneToLoad;

    public GameObject skipButton;
    public VideoPlayer animatic;
    public SceneFader sceneFader;

    private float time;

    void Start()
    {
        time = 4f;
        skipButton.SetActive(false);
    }

    void Update()
    {
        if (skipButton.activeInHierarchy)
        {
            if (Input.GetKeyDown("space"))
                Skip();

            if (!animatic.isPlaying)
                Map();

            return;
        }

        time -= 1 * UnityEngine.Time.deltaTime;

        time = Mathf.Clamp(time, 0f, Mathf.Infinity);

        if (time <= 0)
        {
            skipButton.SetActive(true);
        }
    }

    public void Map()
    {
        sceneFader.FadeTo(sceneToLoad);
    }

    public void Skip()
    {
        animatic.Stop();
    }
}
