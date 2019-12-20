using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float totalTime;

    public GameMaster master;
    public TextMeshProUGUI time;

    private int minutes;
    private int seconds;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("starting");
        time = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.LevelEnded)
            return;

        totalTime -= 1 * UnityEngine.Time.deltaTime;

        totalTime = Mathf.Clamp(totalTime, 0f, Mathf.Infinity);

        Time();

        time.text = string.Format("{0:0}:{1:00}", minutes, seconds);

        if (totalTime <= 0)
        {
            Debug.Log("Time's up");
            _ = StartCoroutine(master.EndLevel());
        }
    }

    public void Time()
    {
        minutes = Mathf.FloorToInt(totalTime / 60F);
        seconds = Mathf.FloorToInt(totalTime - (minutes * 60));
    }
}