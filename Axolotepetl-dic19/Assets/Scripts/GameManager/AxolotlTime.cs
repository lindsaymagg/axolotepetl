using UnityEngine;

public class AxolotlTime : Timer
{
    public static bool nextWaveLoading;

    public static float axolotlTime;

    private int min;
    private int sec;

    // Update is called once per frame
    void Update()
    {
        int lives = Chef.Lives;
        master.livesLeft.text = lives.ToString();

        if (GameMaster.LevelEnded || nextWaveLoading)
            return;

        axolotlTime -= 1 * UnityEngine.Time.deltaTime;
        axolotlTime = Mathf.Clamp(axolotlTime, 0f, Mathf.Infinity);

        AxolotlTimer();
        time.text = string.Format("{0:0}:{1:00}", min, sec);

        if (axolotlTime <= 0)
        {
            nextWaveLoading = true;

            Debug.Log("Out of time, next wave incoming");
            _ = StartCoroutine(master.NextWave());
        }
    }

    private void AxolotlTimer()
    {
        min = Mathf.FloorToInt(axolotlTime / 60F);
        sec = Mathf.FloorToInt(axolotlTime - (min * 60));
    }
}
