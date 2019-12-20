using TMPro;
using UnityEngine;

public class CounterPicar : MonoBehaviour
{
    TextMeshProUGUI timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<TextMeshProUGUI>();
        timer.text = "10";

    }

    // Update is called once per frame
    void Update()
    {

    }
}
