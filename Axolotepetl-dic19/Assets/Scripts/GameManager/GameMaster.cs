using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public Chef cheffy;

    [HideInInspector] public Animator anim;

    public GameObject basurero;
    public GameObject levelOverUI;
    public GameObject gameWonUI;
    public GameObject gameOverUI;
    public GameObject refrigerador;

    public AudioClip start;
    public AudioClip victory;

    public AudioClip open;
    public AudioClip close;

    public AudioSource source;

    public bool GUI = false;

    public static bool secondChance = false;
    public static bool LevelEnded;
    public static bool GameEnded;

    public List<Meal>[] mealLists = new List<Meal>[4];

    [Header("Ingredients List")]
    public List<Ingredient> currentIngredientsOnPlate;

    [Header("Meal Lists")]
    public MealsUI[] mealsUIs;
    public List<Meal> ordersFront = new List<Meal>();
    public List<Meal> ordersRight = new List<Meal>();
    public List<Meal> ordersLeft = new List<Meal>();
    public List<Meal> ordersBack = new List<Meal>();

    [Header("Minigames Scripts")]
    public JuegaEstufa miniJuegoEstufa;
    public JuegaRallador miniJuegoRallar;
    public JuegaMezclar miniJuegoMezclar;
    public JuegaLicuadora miniJuegoLicuadora;
    public JuegaMolcajete miniJuegoMolcajete;
    public JuegaEmplatado miniJuegoEmplatado;
    public JuegaPicar miniJuegoPicar;

    [Header("Minigames Canvas")]
    public GameObject canvasEstufa;
    public GameObject canvasRallarLic;
    public GameObject canvasMolc;
    public GameObject canvasMezclar;
    public GameObject canvasPicar;
    public GameObject canvasEmplatado;

    [HideInInspector] public bool exitoEstufa;
    [HideInInspector] public bool exitoRallar;
    [HideInInspector] public bool exitoMezclar;
    [HideInInspector] public bool exitoLicuadora;
    [HideInInspector] public bool exitoMolcajete;
    [HideInInspector] public bool exitoPicar;
    [HideInInspector] public bool exitoEmplatado;

    [Header("Axolotl")]
    public Axolotl xolotl;
    public AxolotlSpawner axolotlSpawner;
    public TextMeshProUGUI livesLeft;
    public GameObject retry;
    public float nextWaveTime;

    // Start is called before the first frame update
    void Start()
    {
        refrigerador.SetActive(false);
        levelOverUI.SetActive(false);

        AxolotlTime.axolotlTime = 180f;
        AxolotlTime.nextWaveLoading = false;

        LevelEnded = false;
        GameEnded = false;

        mealLists[0] = ordersFront;
        mealLists[1] = ordersRight;
        mealLists[2] = ordersLeft;
        mealLists[3] = ordersBack;

        miniJuegoEstufa.enabled = false;
        miniJuegoRallar.enabled = false;
        miniJuegoMezclar.enabled = false;
        miniJuegoMolcajete.enabled = false;
        miniJuegoPicar.enabled = false;
        miniJuegoEmplatado.enabled = false;
        miniJuegoLicuadora.enabled = false;

        canvasEstufa.SetActive(false);
        canvasMezclar.SetActive(false);
        canvasRallarLic.SetActive(false);
        canvasMolc.SetActive(false);
        canvasPicar.SetActive(false);
        canvasEmplatado.SetActive(false);

        exitoEmplatado = false;
        exitoEstufa = false;
        exitoLicuadora = false;
        exitoMezclar = false;
        exitoMolcajete = false;
        exitoPicar = false;
        exitoRallar = false;

        currentIngredientsOnPlate = new List<Ingredient>();

        source = GetComponent<AudioSource>();

        if (cheffy.chefIndex == 0)
            anim = GameObject.Find("ChefMujer").GetComponent<Animator>();

        if (cheffy.chefIndex == 1)
            anim = GameObject.Find("ChefHombre").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameEnded)
            return;

        if (secondChance && Chef.Lives <= 0)
            _ = StartCoroutine(GameLost());

        if (GUI)
        {
            basurero.SetActive(false);
        }
        else
        {
            basurero.SetActive(true);
        }

        if (LevelEnded)
            return;

        /*// For debugging.
        if (Input.GetKeyDown("b"))
        {
            for (int i = 0; i < mealsUIs.Length; i++)
            {
                mealsUIs[i].CleanAllSlots();
            }

            ordersFront.Clear();
            ordersRight.Clear();
            ordersLeft.Clear();
            ordersBack.Clear();
        }

        if (Input.GetKeyDown("f"))
        {
            Debug.Log("GroupIndex: " + GroupSpawner.groupIndex);
        }*/

        if (Input.GetKeyDown("n"))
            _ = StartCoroutine(EndLevel());
        //  ----

        if (SceneManager.GetActiveScene().name == "Level01" || SceneManager.GetActiveScene().name == "Level02")
        {
            if (GroupSpawner.groupIndex >= 3 && ordersFront.Count == 0 && ordersRight.Count == 0 && ordersLeft.Count == 0 && ordersBack.Count == 0)
            {
                _ = StartCoroutine(EndLevel());
            }
        }
        else
        {
            if (GroupSpawner.groupIndex >= 4 && ordersFront.Count == 0 && ordersRight.Count == 0 && ordersLeft.Count == 0 && ordersBack.Count == 0)
            {
                _ = StartCoroutine(EndLevel());
            }
        }
    }

    //  Refrigerator UI.
    public void OpenRefri()
    {
        GUI = true;
        refrigerador.SetActive(true);
        anim.SetBool("picking", true);
        source.PlayOneShot(open);
    }

    public void CloseRefri()
    {
        GUI = false;
        refrigerador.SetActive(false);
        anim.SetBool("picking", false);
        source.PlayOneShot(close);
    }
    //  ----

    //  Add Meals and Ingredients to corresponding Lists.
    public void AddMeal(Meal meal, List<Meal> orderSide)
    {
        orderSide.Add(meal);
    }

    public void AddIngredient(Ingredient ingredient)
    {
        currentIngredientsOnPlate.Add(ingredient);
    }
    //  ----

    //  Begin Minigames, except plating.
    public void PlayMinigame()
    {
        if (cheffy.enEstufa)
        {
            canvasEstufa.SetActive(true);
            miniJuegoEstufa.enabled = true;

            GUI = true;

            source.PlayOneShot(start);
            anim.SetBool("cooking", true);
        }

        if (cheffy.enLicuadora)
        {
            canvasRallarLic.SetActive(true);
            miniJuegoLicuadora.enabled = true;

            GUI = true;

            source.PlayOneShot(start);
            anim.SetBool("cooking", true);
        }

        if (cheffy.enMezclar)
        {
            canvasMezclar.SetActive(true);
            miniJuegoMezclar.enabled = true;

            GUI = true;

            source.PlayOneShot(start);
            anim.SetBool("cooking", true);
        }

        if (cheffy.enMolcajete)
        {
            canvasMolc.SetActive(true);
            miniJuegoMolcajete.enabled = true;

            GUI = true;

            source.PlayOneShot(start);
            anim.SetBool("cooking", true);
        }

        if (cheffy.enPicar)
        {
            canvasPicar.SetActive(true);
            miniJuegoPicar.enabled = true;

            GUI = true;

            source.PlayOneShot(start);
            anim.SetBool("cooking", true);
        }

        if (cheffy.enRallador)
        {
            canvasRallarLic.SetActive(true);
            miniJuegoRallar.enabled = true;

            GUI = true;

            source.PlayOneShot(start);
            anim.SetBool("cooking", true);
        }

        if (cheffy.enPlatos)
        {
            Debug.Log("El ingrediente todavía no ha sido preparado.");
        }
    }

    //  Minigame endings, except plating.
    #region EndMinigames

    public void EndEstufa(bool exito)
    {
        Debug.Log("Ganó juego de estufa: " + exito);

        miniJuegoEstufa.enabled = false;
        canvasEstufa.SetActive(false);

        GUI = false;


        exitoEstufa = true;
        // source.PlayOneShot(victory);

        if (exito)
        {
            cheffy.Cook();
        }
    }

    public void EndRallar(bool exito)
    {
        Debug.Log("Ganó juego de rallar: " + exito);

        miniJuegoRallar.enabled = false;
        canvasRallarLic.SetActive(false);

        GUI = false;


        exitoRallar = true;

        if (exito)
        {
            cheffy.Cook();
        }
    }

    public void EndLicuadora(bool exito)
    {
        Debug.Log("Ganó juego de licuadora: " + exito);

        miniJuegoLicuadora.enabled = false;
        canvasRallarLic.SetActive(false);

        GUI = false;


        exitoLicuadora = true;

        if (exito)
        {
            cheffy.Cook();
        }
    }

    public void EndMezclar(bool exito)
    {
        Debug.Log("Ganó juego de mezclar: " + exito);

        miniJuegoMezclar.enabled = false;
        canvasMezclar.SetActive(false);

        GUI = false;


        exitoMezclar = true;

        if (exito)
        {
            cheffy.Cook();
        }
    }

    public void EndMolcajete(bool exito)
    {
        Debug.Log("Ganó juego de molcajete: " + exito);

        miniJuegoMolcajete.enabled = false;
        canvasMolc.SetActive(false);

        GUI = false;


        exitoMolcajete = true;

        if (exito)
        {
            cheffy.Cook();
        }
    }

    public void EndPicar(bool exito)
    {
        Debug.Log("Ganó juego de picar: " + exito);

        miniJuegoPicar.enabled = false;
        canvasPicar.SetActive(false);

        GUI = false;



        exitoPicar = true;
        anim.SetBool("cooking", false);


        if (exito)
        {
            cheffy.Cook();
        }
    }

    #endregion

    //  Plating.
    public void PlateMinigame()
    {
        //Debug.Log("succesfully entered emplatado");
        canvasEmplatado.SetActive(true);
        miniJuegoEmplatado.enabled = true;

        GUI = true;

        source.PlayOneShot(start);
        anim.SetBool("cooking", true);
    }

    public Meal CheckMeal(Meal[] meals)
    {
        Meal found = null;

        Debug.Log(found);

        foreach (Meal meal in meals)
        {
            if (meal.IsCompleted(currentIngredientsOnPlate))
            {
                found = meal;
                break;
            }
        }

        //Debug.Log("After foreach: " + found);

        if (found != null)
        {
            return found;
        }

        return null;
    }

    public void EndEmplatado(bool exito, Meal meal)
    {
        Debug.Log("Ganó juego de emplatado: " + exito);

        miniJuegoEmplatado.enabled = false;
        canvasEmplatado.SetActive(false);

        GUI = false;

        anim.SetBool("cooking", false);

        exitoEmplatado = true;


        if (exito)
        {
            source.PlayOneShot(victory);
            cheffy.GetPlate(meal);
        }
    }
    //  ----

    //  Delivery.
    public void DeliverPlate(List<Meal> orderSide)
    {
        Meal toDeliver = null;

        Debug.Log(toDeliver);

        foreach (Meal meal in orderSide)
        {
            if (meal == cheffy.mealToDeliver)
            {
                toDeliver = meal;
                break;
            }
        }

        //Debug.Log("After foreach: " + toDeliver);

        if (toDeliver != null)
        {
            _ = orderSide.Remove(toDeliver);
        }

        cheffy.Deliver();
    }
    //  ----

    //  Axolotl.
    public IEnumerator NextWave()
    {
        AxolotlTime.axolotlTime += nextWaveTime;
        Debug.Log("Time calculated");

        yield return new WaitForSeconds(0.5f);

        nextWaveTime += 90f;
        AxolotlSpawner.randomIndex = Random.Range(0, 4);
        Debug.Log("Index calculated");

        int mealsLost = ordersFront.Count + ordersRight.Count + ordersLeft.Count + ordersBack.Count;

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < mealsUIs.Length; i++)
        {
            mealsUIs[i].CleanAllSlots();
        }

        ordersFront.Clear();
        ordersRight.Clear();
        ordersLeft.Clear();
        ordersBack.Clear();

        Debug.Log("Meals cleared");

        while (mealsLost > 0 && Chef.Lives >= 0)
        {
            Chef.Lives -= 1;

            mealsLost--;
            livesLeft.text = Chef.Lives.ToString();

            yield return new WaitForSeconds(0.05f);
        }

        Debug.Log("Lives calculated");
        yield return new WaitForSeconds(0.5f);

        if (Chef.Lives > 0)
            _ = StartCoroutine(axolotlSpawner.AxolotlSpawn(AxolotlSpawner.randomIndex));

        if (Chef.Lives <= 0 && !secondChance)
            retry.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        Debug.Log("About to finish loading the next wave");
        AxolotlTime.nextWaveLoading = false;
    }
    //  ----

    //  Level ends.
    public IEnumerator EndLevel()
    {
        GUI = true;
        LevelEnded = true;
        Chef.clients = FindObjectsOfType<Client>();

        Debug.Log("Clients: " + Chef.clients.Length);

        yield return new WaitForSeconds(0.5f);

        foreach (Client client in Chef.clients)
        {
            if (client.state == Client.ClientState.HUNGRY)
                Chef.FaintedClients++;

            if (client.state == Client.ClientState.FULL)
                Chef.SatisfiedClients++;
        }

        Debug.Log("+" + Chef.SatisfiedClients + ", -" + Chef.FaintedClients);

        yield return new WaitForSeconds(0.5f);

        levelOverUI.SetActive(true);
    }
    //  ----

    //  Game ends.
    public IEnumerator GameWon()
    {
        GameEnded = true;

        yield return new WaitForSeconds(1.5f);

        gameWonUI.SetActive(true);
    }

    public IEnumerator GameLost()
    {
        GameEnded = true;
        Debug.Log("Game Over");

        yield return new WaitForSeconds(1.5f);

        levelOverUI.SetActive(false);
        gameOverUI.SetActive(true);
    }
    //  ----
}