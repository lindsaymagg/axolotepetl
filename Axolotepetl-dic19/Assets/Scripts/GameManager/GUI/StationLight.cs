using UnityEngine;

/// <summary>
/// Prender la luz de una estación cuando el chef se está parado cerca a ella.
/// Turn on a station's spotlight when the chef is standing close to it.
/// </summary>
public class StationLight : MonoBehaviour
{
    public GameObject spotlight;

    // Start is called before the first frame update
    void Start()
    {
        spotlight.SetActive(false);
    }

    /// <summary>
    /// Prender luz.
    /// Turn on light.
    /// </summary>
    /// <param name="other"></param> collider de la estación / station's collider
    private void OnTriggerEnter(Collider other)
    {
        if (other is null)
        {
            throw new System.ArgumentNullException(nameof(other));
        }

        spotlight.SetActive(true);
    }

    /// <summary>
    /// Apagar luz.
    /// Turn off light.
    /// </summary>
    /// <param name="other"></param> collider de la estación / station's collider
    private void OnTriggerExit(Collider other)
    {
        if (other is null)
        {
            throw new System.ArgumentNullException(nameof(other));
        }

        spotlight.SetActive(false);
    }
}
