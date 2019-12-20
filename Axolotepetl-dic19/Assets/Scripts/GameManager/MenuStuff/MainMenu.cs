using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controla el menú principal, y los met0dos de los botones. Carga la próxima escena.
///
/// Controls the main menu. Controls functions of the buttons. Loads the next scene.
/// </summary>
public class MainMenu : MonoBehaviour
{

    public string sceneToLoad;

    [Tooltip("Varios canvases (que representan los menus) - Various canvases (that represent menus)")]
    [Header("Canvases")]
    public GameObject mainMenu;
    public GameObject newGame;
    public GameObject instructions;
    public GameObject credits;

    [Header("Botón de Continuar")]
    [Tooltip("'Continuar' botón que carga un juego ya empezado - 'Continue' button which loads a game already begun")]
    public Button loadGame;

    [Header("Configuración de Instrucciones")]
    //Páginas de instrucciones
    //Instruction pages
    public Image InstruccionesImage;
    public Sprite[] instruccionesPages;

    public GameObject nextPageButton;
    public GameObject previousPageButton;

    public SceneFader sceneFader;

    private int pageIndex;
    private int levelindex;


    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);

        _ = PlayerPrefs.GetInt("levelReached", 1);

        pageIndex = 0;
        instructions.SetActive(false);
        nextPageButton.SetActive(false);
        previousPageButton.SetActive(false);

        levelindex = PlayerPrefs.GetInt("levelReached");

        credits.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        InstruccionesImage.sprite = instruccionesPages[pageIndex];

        //if you hadn't started a game previously, you can't press it
        if (levelindex <= 1)
            loadGame.interactable = false;

        //Controlar páginas de las instrucciones.
        //Control instructions pages.
        if (pageIndex < instruccionesPages.Length - 1)
        {
            nextPageButton.SetActive(true);
        }
        else
        {
            nextPageButton.SetActive(false);
        }

        if (pageIndex > 0)
        {
            previousPageButton.SetActive(true);
        }
        else
        {
            previousPageButton.SetActive(false);
        }
    }


    /// <summary> Cuando se presiona el botón Jugar, carga el menú que pregunta si se quiere continuar el juego o empezar de nuevo.
    /// When the Play button is pressed, load the menu that asks if they want to start over or continue the game.
    /// </summary>
    public void Play()
    {
        mainMenu.SetActive(false);
        newGame.SetActive(true);
    }


    /// <summary> Cuando se presiona el botón Jugar, carga el menú que pregunta si se quiere continuar el juego o empezar de nuevo.
    /// When the Play button is pressed, load the menu that asks if they want to start over or continue the game.
    /// </summary>
    public void NewGame()
    {
        sceneFader.FadeTo(sceneToLoad);
        PlayerPrefs.SetInt("levelReached", 1);
    }


    /// <summary> Cuando se presiona el botón Continuar, carga el mapa.
    /// When the Continue button is pressed, load the map.
    /// </summary>
    public void Continue()
    {
        sceneFader.FadeTo("Map");
    }


     /// <summary>
     /// Cuando se presiona el botón Instrucciones, carga el menú de Instrucciones.
     /// When the Instructions button is pressed, load the Instructions menu.
     /// </summary>
    public void ReadInstructions()
    {
        mainMenu.SetActive(false);
        instructions.SetActive(true);
    }


    /// <summary>
    /// Cambiar la página de instrucciones (próxima).
    /// Changes the instructions page (next).
    /// </summary>
    public void NextPage()
    {
        pageIndex++;
    }


    /// <summary>
    /// Cambiar la página de instrucciones (previa).
    /// Changes the instructions page (previous).
    /// </summary>
    public void PreviousPage()
    {
        pageIndex--;
    }


    /// <summary>
    /// Activar el menú de créditos.
    /// Activates the credits menu.
    /// </summary>
    public void ReadCredits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }


    /// <summary>
    /// Activar el menú principal.
    /// Activates the main menu.
    /// </summary>
    public void GoBackToMenu()
    {
        mainMenu.SetActive(true);
        instructions.SetActive(false);
        credits.SetActive(false);
    }


    /// <summary>
    /// Salir del juego.
    /// Exit the game.
    /// </summary>
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
