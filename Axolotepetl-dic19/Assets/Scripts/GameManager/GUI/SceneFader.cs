using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public Image blackCanvas;

    // Start is called before the first frame update
    void Start()
    {
        _ = StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        _ = StartCoroutine(FadeOut(scene));
    }

    private IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime * 0.8f;
            blackCanvas.color = new Color(0f, 0f, 0f, t);
            yield return 0;
        }
    }

    private IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * 0.8f;
            blackCanvas.color = new Color(0f, 0f, 0f, t);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}
