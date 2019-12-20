using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase de cliente. Cada cliente tiene: un enum estado (hambriente o satisfecho),
/// un int sideID que corresponde con el lado del cliente, un int ID para identificar cada cliente,
/// un int numMeals que describe el numero de platillos pedidos, dos Meals (platillos) meal y secondMeal (secondMeal
/// puede ser null) que representan los platillos pedidos por este cliente.
/// Sus platillos pedidos se asignan en esta clase, y aquí también se cambia el estado.
/// Client class. Every client has an enum state (hungry or full), an int sideID that corresponds to the side of the
/// client, an int ID to identify each client, an int numMeals that describes the number of dishes ordered, and two Meals
/// (dishes) called meal and secondMeal that represent the dishes ordered. secondMeal can be null.
/// A client's ordered dishes are assigned in this class, and their state changes in this class.
/// </summary>
public class Client : MonoBehaviour
{
    public enum ClientState { HUNGRY, FULL };

    public float speed;
    public bool alreadyOrdered;

    [HideInInspector] public int ID;
    [HideInInspector] public int sideID;

    [HideInInspector] public int numMeals;

    [HideInInspector] public Meal meal;
    [HideInInspector] public Meal secondMeal;

    [HideInInspector] public MealsUI UI;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Image health;

    public GameMaster master;
    public MealsManager mealsManager;

    [HideInInspector] public List<Meal> meals;
    [HideInInspector] public ClientState state = ClientState.HUNGRY;

    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        alreadyOrdered = false;
        state = ClientState.HUNGRY;
        anim = GetComponent<Animator>();

        if (transform.position.x > 30)
        {
            target = new Vector3(Random.Range(5.5f, 7.5f), 0, Random.Range(-4f, 4f));
        }

        if (transform.position.x < -30)
        {
            target = new Vector3(Random.Range(-7.5f, -5.5f), 0, Random.Range(-4f, 4f));
        }

        if (transform.position.z > 30)
        {
            target = new Vector3(Random.Range(-4f, 4f), 0, Random.Range(5.5f, 7.5f));
        }

        if (transform.position.z < -30)
        {
            target = new Vector3(Random.Range(-4f, 4f), 0, Random.Range(-7.5f, -5.5f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        HealthUI();

        if (meal == null && secondMeal == null && alreadyOrdered)
        {
            state = ClientState.FULL;
            anim.SetBool("excited", true);
        }

        if (GameMaster.LevelEnded && state == ClientState.HUNGRY)
            anim.SetBool("dying", true);

        if (Vector3.Distance(this.transform.position, target) < 0.001f)
        {
            //anim.SetBool("walking", false);
            return;
        }

        this.transform.position = Vector3.Lerp(this.transform.position, target, speed * Time.deltaTime);

        // si aún no ha pedido, asignar platillos para pedir
        // if hasn't already ordered, assign dishes to order
        if (!alreadyOrdered)
        {
            if (Vector3.Distance(transform.position, target) < 10f)
            {

                Order();

                // si este cliente debe pedir mas de un platillo, asigna un segundo platillo
                // if this client is to order more than one dish, assign a second dish
                if (numMeals > 1)
                {
                    SecondOrder();
                }

            }
        }
    }

    /// <summary>
    /// Asignar un primer platillo para pedir.
    /// Assign a first dish to order.
    /// </summary>
    public void Order()
    {
        alreadyOrdered = true;

        meal = mealsManager.ChooseMeal();

        master.AddMeal(meal, meals);

        UI.FillSlot(meal);
    }


    /// <summary>
    /// Asignar un segundo platillo para pedir.
    /// Assign a second dish to order.
    /// </summary>
    public void SecondOrder()
    {
        secondMeal = mealsManager.ChooseMeal();

        master.AddMeal(secondMeal, meals);

        UI.FillSlot(secondMeal);
    }


    /// <summary>
    /// Configurar el color del cuadrado que flota sobre la cabeza de un cliente.
    /// Representa si el cliente sigue teniendo hambre o no.
    /// Set the color of the square that floats above the client's head.
    /// Represents whether the client is still hungry or not.
    /// </summary>
    public void HealthUI()
    {
        if (state == ClientState.FULL)
        {
            health.color = Color.green;
        }
        else if (state == ClientState.HUNGRY)
        {
            health.color = Color.red;
        }
    }
}
