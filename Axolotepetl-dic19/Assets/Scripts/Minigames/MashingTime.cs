using TMPro;
using UnityEngine;

public class MashingTime : MonoBehaviour
{

    //funciona bien...no toques por favor!
    //si hay fallas, me avisas

    float currentTime;
    public float numSec = 3f;
    public TextMeshProUGUI timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("RallarLicTimer").GetComponent<TextMeshProUGUI>();
        timer.enabled = false;

    }

    private void OnEnable()
    {
        //Debug.Log("starting timer");
        timer.enabled = true;
        float startingTime = numSec;
        currentTime = numSec;

    }

    // Update is called once per frame
    void Update()
    {
        if (timer.text == "0")
        {
            timer.text = "0";
        }

        else
        {
            currentTime -= 1 * Time.deltaTime;
            timer.text = currentTime.ToString("0");
            //Debug.Log(currentTime);
        }

    }
}
