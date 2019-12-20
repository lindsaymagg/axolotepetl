using UnityEngine;

public class CubeSideHighlight : MonoBehaviour
{
    public Chef cheffy;

    public int sideIndex;

    public GameObject highlight;

    // Start is called before the first frame update
    void Start()
    {
        highlight.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (cheffy.chefState == Chef.ChefState.DELIVERING)
        {
            if (other is null)
            {
                throw new System.ArgumentNullException(nameof(other));
            }

            cheffy.cubeSideIndex = sideIndex;
            highlight.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other is null)
        {
            throw new System.ArgumentNullException(nameof(other));
        }

        cheffy.cubeSideIndex = 5;
        highlight.SetActive(false);
    }
}
