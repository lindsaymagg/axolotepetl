using UnityEngine;

/// <summary>
/// Controlar movimento de un solo lado del chef y sus animaciones de caminar. Inicializar
/// configuraciones de nivel (estado = cocinando, número de clientes alimentados = 0, número
/// de clientes desmayados = 0), y cual de los chefs para usar. Abrir el refri. Detectar para ver
/// si el chef está en frente de una estación. Jugar minijuego si está en frente de una estación
/// y si lleva un ingrediente crudo. Poner ingrediente preparado en emplatado. Jugar emplatado. Tirar
/// ingrediente o platillo. Cocinar ingrediente después de jugar un minijuego exitosamente. Servir platillo
/// a cliente si hay un cliente en el mismo lado como el chef que pidió ese platillo. Alimentar al Axolote si
/// está en el nivel 5 y si está en el mismo lado como el chef. Cambiar estados del chef.
///
/// Controls same-size movement of chef and walking animations. Initializes settings of 
/// level (state = cooking, number of fed clients = 0, number of fainted clientes = 0,
/// which chef to use). Opening of refrigerator. Detecting to see if chef is in front of a
/// station. Play minigame if in front of station and holding raw ingredient. Put prepared
/// ingrediente at plating station. Play plating minigame.Throw away ingredient. Cook
/// ingredient after successfully playing minigame at correct station. Deliver meal to
/// client if client on same side as chef ordered meal held. Feed Axolote if on level 5 and
/// on correct side of kitchen. Controls changes of states of chef from cooking to serving
/// and vice versa.
/// </summary>
public class Chef : MonoBehaviour
{
    public enum ChefState { COOKING, DELIVERING };

    public GameMaster master;

    public static Client[] clients;

    [HideInInspector] public Meal mealToDeliver;
    [HideInInspector] public Ingredient currentIngredient;

    public OnPlateUI UI;
    public MealsManager mealsManager;

    public AudioClip basurero;
    public AudioClip preparado;
    public AudioClip noPreparado;

    [HideInInspector] public Animator anim;
    [HideInInspector] public ChefState chefState;

    public static int Lives = 5;

    public static int SatisfiedClients;
    public static int FaintedClients;

    [HideInInspector] public int chefIndex;

    public float speed;
    public float autoSpeed;

    public bool atCorrectStation;

    [Header("Station Triggers")]
    [HideInInspector] public bool enEstufa;
    [HideInInspector] public bool enLicuadora;
    [HideInInspector] public bool enMolcajete;
    [HideInInspector] public bool enRallador;
    [HideInInspector] public bool enMezclar;
    [HideInInspector] public bool enPicar;
    [HideInInspector] public bool enPlatos;

    [HideInInspector] public int cubeSideIndex;

    private AudioSource source;
    private IngredientsManager ingredientsManager;

    /// <summary>
    /// Iniciar nivel. Ningun cliente se ha desmayado, ninguno ha sido servido.
    /// Chef empieza cocinando, no serviendo. Usar el chef según lo que seleccionó el jugador.
    /// Initialize level. No clients have fainted, none have been served.
    /// Chef starts out cooking, not delivering. Use correct chef based on what player chose.
    /// </summary>
    protected void Init()
    {
        FaintedClients = 0;
        SatisfiedClients = 0;

        cubeSideIndex = 5;
        chefState = ChefState.COOKING;

        chefIndex = PlayerPrefs.GetInt("ChefSelected");
        ingredientsManager = IngredientsManager.instance;

        if (chefIndex == 0)
            anim = GameObject.Find("ChefMujer").GetComponent<Animator>();

        if (chefIndex == 1)
            anim = GameObject.Find("ChefHombre").GetComponent<Animator>();

        source = GameObject.Find("MainCamera").GetComponent<AudioSource>();
    }

    /// <summary>
    /// Controlar movimiento del chef en un solo lado de la cocina. Mientras camina el chef, activar la animación de caminar.
    /// Si el jugador presiona espacio, el refri abre. Jugar minijuego. Poner ingrediente en emplatado.
    /// Controls same-side movement of chef. When moving, activate walking animation.
    /// If player presses space, open refrigerador. Play minigames. Put ingredient at plating station.
    /// </summary>
    public void Move()
    {
        //  Movimiento horizontal.
        //  Horizontal movement.
        float horizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(horizontal) > 0)
        {
            this.transform.Translate(horizontal * Time.deltaTime * speed, 0, 0);
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }

        // Abrir el refri.
        // Open fridge.
        if (Input.GetKeyDown("down"))
        {
            if (ingredientsManager.CanPlay())
            {
                Debug.Log("Ya tienes un ingrediente.");
                return;
            }
            else
            {
                master.OpenRefri();
            }
        }

        // Empezar minijuego si el chef lleva un ingrediente crudo. Dejar un ingrediente preparado en la estación
        // de emplatado si el chef está en esa estación.
        // Enter minigame if chef is holding a raw ingredient. Leave a prepared ingredient at plating station if the
        // chef is at that station.
        if (Input.GetKeyDown("space"))
        {
            if (ingredientsManager.CanPlay())
            {
                // si ingredient está crudo, entra minijuego
                // if ingredient is raw, enter minigame
                if (currentIngredient.state == Ingredient.IngredientState.RAW)
                {
                    master.PlayMinigame();

                }

                // si ingredient está preparado y el chef está en la estación de emplatado, poner ingrediente.
                // if ingredient is prepared and the chef is at the plating state, put down ingredient at the station.
                else if (currentIngredient.state == Ingredient.IngredientState.COOKED)
                {
                    if (enPlatos)
                    {
                        //llenar plaza en emplatado con el ingrediente preparado
                        //fill spot at plating station with prepared ingredient
                        UI.FillSlot(currentIngredient);
                        master.AddIngredient(currentIngredient);

                        //corregir color, correct color
                        currentIngredient.state = Ingredient.IngredientState.RAW;

                        //vaciar ingrediente actual, make current ingredient null
                        currentIngredient = null;
                        ingredientsManager.ResetIngredient();
                    }
                }
            }
            else
            {
                Debug.Log("No tienes un ingrediente.");
            }
        }

        // Jugar minijuego Emplatado.
        // Play plating minigame.
        if (Input.GetKeyDown("q"))
        {
            if (enPlatos)
            {
                mealToDeliver = master.CheckMeal(mealsManager.mealsInLevel);

                if (mealToDeliver != null)
                {
                    master.PlateMinigame();
                }
                else
                {
                    Debug.Log("Eso no es un platillo.");
                }
            }
        }

        // Throw away ingredient & Clean Plate.
        if (Input.GetKeyDown("x"))
        {
            //si está en frente de emplatado, borrar ingredientes
            //if in front of plating station, erase ingredients
            if (enPlatos && currentIngredient == null)
            {
                UI.EmptySlots();
                master.currentIngredientsOnPlate.Clear();
                Debug.Log("Ingredientes de la estación emplatado tirados.");
            }

            //tirar ingrediente
            //throw away ingrediente
            else if (currentIngredient != null)
            {
                currentIngredient = null;
                ingredientsManager.ResetIngredient();
                source.PlayOneShot(basurero);
                Debug.Log("Ingrediente tirado.");
            }

            //EMPTY PLATILLO
        }
    }

    /// <summary>
    /// Igual al met0do "Move", excepto ahora ya es imposible interactuar con las estaciones y el refri porque tiene el estado "servir" el chef.
    /// Same as the method "Move", except now it's impossible to interact with the stations and the fridge because the chef has state "delivering".
    /// "Move":
    /// Controlar movimiento del chef en un solo lado de la cocina, y la animación de caminar. Mientras camina el chef, activar la animación de caminar.
    /// Si el jugador presiona espacio, el refri abre.
    /// Controls same-side movement of chef and the walking animations. When moving, activate walking animation.
    /// If player presses space, open refrigerador. 
    /// </summary>
    public void MoveDeliver()
    {
        //  Horizontal movement.
        float horizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(horizontal) > 0)
        {
            transform.Translate(horizontal * Time.deltaTime * speed, 0, 0);
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }
    }

    //  Enables Stations.
    /// <summary>
    /// Activar una estación si el chef está cerca a ella.
    /// Enables a station if chef is close to the station.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (chefState == ChefState.COOKING)
        {
            switch (other.name)
            {
                case "Estufa":
                    enEstufa = true;
                    break;

                case "Cortar":
                    enPicar = true;
                    break;

                case "Mezclar":
                    enMezclar = true;
                    break;

                case "Licuadora":
                    enLicuadora = true;
                    break;

                case "Molcajete":
                    enMolcajete = true;
                    break;

                case "Rallar":
                    enRallador = true;
                    break;

                case "Emplatar":
                    enPlatos = true;
                    break;
            }
        }
    }

    /// <summary>
    /// Desactivar una estación si el chef no está cerca a ella.
    /// Disables a station if chef is not close to the station.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        switch (other.name)
        {
            case "Estufa":
                enEstufa = false;
                break;

            case "Cortar":
                enPicar = false;
                break;

            case "Mezclar":
                enMezclar = false;
                break;

            case "Licuadora":
                enLicuadora = false;
                break;

            case "Molcajete":
                enMolcajete = false;
                break;

            case "Rallar":
                enRallador = false;
                break;

            case "Emplatar":
                enPlatos = false;
                break;
        }

        atCorrectStation = false;
    }

    /// <summary>
    /// El ingrediente actual se vuelve preparado si se ganó el minijuego y si el chef está en la estación adecuada para ese ingrediente.
    /// Current ingredient becomes cooked if minigame is won and if chef is at correct station for that ingredient.
    /// </summary>
    public void Cook()
    {
        if (atCorrectStation)
        {
            currentIngredient.CookedIngredient();
            source.PlayOneShot(preparado);
        }

        else
        {
            source.PlayOneShot(noPreparado);
            Debug.Log("Estación incorrecta.");
        }
    }


    /// <summary>
    /// Ingrediente actual se hace null, porque se transfere a la estación emplatado.
    /// Current ingredient becomes null as it transfers to the plate.
    /// </summary>
    //public void PutIngredientOnPlate()
   /// {
   //     currentIngredient = null;
      //  ingredientsManager.ResetIngredient();
    //}



    /// <summary>
    /// Conseguir platillo despues de jugar el minijuego emplatado.
    /// Get dish after playing plating minigame.
    /// </summary>
    /// <param name="meal"></param>
    public void GetPlate(Meal meal)
    {
        //cambiar estado a servir, change state to deliver
        chefState = ChefState.DELIVERING;

        //vaciar plazas de ingredientes de emplatado, empty ingredient slots from plating station
        UI.EmptySlots();
        master.currentIngredientsOnPlate.Clear();

        //get meal
        mealToDeliver = meal;
        ingredientsManager.GetMeal(mealToDeliver);
    }

    /// <summary>
    /// Deliver meal to client (iff client with that order on that side exists.)
    /// </summary>
    /// <param name="cubeSide"></param> Side of kitchen chef is currently at.
    public void Feed(int cubeSide)
    {
        clients = FindObjectsOfType<Client>();

        //get ALL clients
        for (int i = 0; i < clients.Length; i++)
        {

            //solo checar los clientes que están en el mismo lado como el chef
            //only check clients on same side as chef is currently
            if (clients[i].sideID == cubeSide)
            {
                //si su segundo pedido es igual al platillo, if second meal is the same
                if (clients[i].secondMeal != null && clients[i].secondMeal == mealToDeliver)
                {
                    clients[i].secondMeal = null;
                    //no checar los clientes restantes
                    //don't check rest of client
                    break;
                }

                //si su primer pedido es igual al platillo, if first meal is the same
                else if (clients[i].meal != null && clients[i].meal == mealToDeliver)
                {
                    clients[i].meal = null;
                    //no checar los clientes restantes
                    //don't check rest of clients
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Servir platillo al axolote (si pidió ese platillo, y si el chef está en el lado de la cocina adecuado.
    /// Deliver meal to Axolote (iff if he ordered the meal and the chef is on the right side.)
    /// </summary>
    public void FeedXolotl()
    {
        for (int i = 0; i < Axolotl.finalMeals.Length; i++)
        {
            if (Axolotl.finalMeals[i] != null && Axolotl.finalMeals[i] == mealToDeliver)
            {
                Axolotl.finalMeals[i] = null;
                break;
            }
        }
    }

    /// <summary>
    /// Terminar ciclo de este platillo. Cambiar el estado del chef de "servir" a "cocinar".
    /// End cycle of this plate. Change state of chef from delivering to cooking.
    /// </summary>
    public void Deliver()
    {
        mealToDeliver = null;
        ingredientsManager.ResetMeal();
        chefState = ChefState.COOKING;
    }
}