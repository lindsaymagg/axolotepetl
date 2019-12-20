using UnityEngine;
using UnityEngine.UI;

public class MapScreenManager : MonoBehaviour
{
    public SceneFader fader;

    public GameObject mapCanvasText;
    public GameObject mapCanvasButtons;

    public Button[] levelButtons;

    public GameObject chefSelectionCanvas;

    private int levelReached;

    // Start is called before the first frame update
    void Start()
    {
        levelReached = PlayerPrefs.GetInt("levelReached");

        //Controlar cuales nivels sean disponsibles
        //Control which levels are available
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i >= levelReached)
            {
                levelButtons[i].interactable = false;
            }
            else
            {
                levelButtons[i].interactable = true;
            }
        }

        // si está empezando nuevo juego, activa el menú de seleccionar chef
        // if starting new game, activate menu to select chef
        if (levelReached <= 1)
        {
            mapCanvasText.SetActive(false);
            mapCanvasButtons.SetActive(false);

            chefSelectionCanvas.SetActive(true);
        }
        else
        {
            mapCanvasText.SetActive(true);
            mapCanvasButtons.SetActive(true);

            chefSelectionCanvas.SetActive(false);
        }
    }

    /// <summary>
    /// Escoger la chef mujer.
    /// Choose woman chef.
    /// </summary>
    public void SelectChefMujer()
    {
        PlayerPrefs.SetInt("ChefSelected", 0);

        chefSelectionCanvas.SetActive(false);

        mapCanvasText.SetActive(true);
        mapCanvasButtons.SetActive(true);
    }

    /// <summary>
    /// Escoger el chef hombre.
    /// Choose man chef.
    /// </summary>
    public void SelectChefHombre()
    {
        PlayerPrefs.SetInt("ChefSelected", 1);

        chefSelectionCanvas.SetActive(false);

        mapCanvasText.SetActive(true);
        mapCanvasButtons.SetActive(true);
    }

    /// <summary>
    /// Escoger el nivel para jugar, y cargar ese nivel.
    /// Choose level to play, and load that level.
    /// </summary>
    /// <param name="levelName"></param>
    public void SelectLevel(string levelName)
    {
        fader.FadeTo(levelName);
    }
}
