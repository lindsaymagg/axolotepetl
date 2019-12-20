using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Responsable para mover el chef de un lado de la cocina al otro. Checar si un ingrediente fue preparado en la estación correcto.
/// Responsible for moving the chef from one side of the kitchen to another. Checks to see if the ingredient was prepared in the correct station.
/// </summary>
public class ChefMovement : Chef
{
    [Header("Movement Deliver Phase")]
    public MealsUI[] sides;

    [Header("General Movement")]
    public CameraController cam;
    public Transform[] centerPoints;

    [HideInInspector] public Transform target;
    [HideInInspector] public Transform corner;

    [Header("Chef Characters")]
    public GameObject[] cheffies;

    private int rotation = 0;
    private int pointIndex = 0;

    private Transform currentPoint;

    private readonly float waypointRadius = 0.001f;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        atCorrectStation = false;
        currentPoint = centerPoints[pointIndex];

        foreach (GameObject item in cheffies)
        {
            item.SetActive(false);
        }

        cheffies[chefIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            master.CloseRefri();
        }


        //debugging
        if (Input.GetKeyDown("v"))
        {
            clients = FindObjectsOfType<Client>();

            foreach (Client client in clients)
            {
                client.state = Client.ClientState.FULL;
            }
        }

        //solo puede moverse si no está girando a otro lado de la cocina
        if (!cam.rotating && !master.GUI)
        {
            if (chefState == ChefState.COOKING)
            {
                Move();

                if (currentIngredient != null)
                {
                    UpdateStation();
                }
            }

            if (chefState == ChefState.DELIVERING)
            {
                MoveDeliver();

                if (Input.GetKeyDown("down"))
                    Debug.Log("Ya tienes un platillo.");

                if (Input.GetKeyDown("space"))
                {
                    if (SceneManager.GetActiveScene().name == "Level05")
                    {
                        FeedXolotl();
                    }
                    else
                    {
                        Feed(cubeSideIndex);
                    }

                    sides[cubeSideIndex].EmptySlot();
                    master.DeliverPlate(master.mealLists[cubeSideIndex]);
                }
            }
        }
    }
     
    /// <summary>
    /// Checar si el ingrediente fue preparado en su estación adecuada.
    /// Check if the ingredient was prepared at the correct station.
    /// </summary>
    public void UpdateStation()
    {
        if (currentIngredient.state == Ingredient.IngredientState.RAW)
        {
            if (enEstufa && ((currentIngredient.name == "Tortillas") || (currentIngredient.name == "Carne") || (currentIngredient.name == "Bolillo") || (currentIngredient.name == "Pollo") || (currentIngredient.name == "Agua") || (currentIngredient.name == "Totopos")))
            {
                atCorrectStation = true;
            }

            if (enLicuadora && ((currentIngredient.name == "Jitomate") || (currentIngredient.name == "ChileVerde") || (currentIngredient.name == "Crema") || (currentIngredient.name == "Cebolla")))
            {
                atCorrectStation = true;
            }

            if (enMezclar && (currentIngredient.name == "Chocolate" || currentIngredient.name == "Frijoles"))
            {
                atCorrectStation = true;
            }

            if (enMolcajete && ((currentIngredient.name == "Aguacate") || (currentIngredient.name == "Cilantro")))
            {
                atCorrectStation = true;
            }

            if (enPicar && ((currentIngredient.name == "Cebolla") || (currentIngredient.name == "Maiz") || (currentIngredient.name == "Jamon")))
            {
                atCorrectStation = true;
            }

            if (enRallador && ((currentIngredient.name == "Queso") || (currentIngredient.name == "ChileRojo") || (currentIngredient.name == "Maiz")))
            {
                atCorrectStation = true;
            }
        }

        else
        {
            if (enPlatos)
            {
                atCorrectStation = true;
            }
        }
    }


    /// <summary>
    /// Movimiento a la izquierda.
    /// Movement to the left.
    /// </summary>
    public void MoveLeft()
    {
        if (rotation == 0)
        {
            transform.position = Vector3.Lerp(transform.position, corner.position, autoSpeed * Time.deltaTime);
            anim.SetBool("walking", true);

            if (Vector3.Distance(transform.position, corner.position) <= waypointRadius)
            {
                transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                rotation++;
            }
        }
        else if (rotation == 1)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, autoSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) <= waypointRadius)
            {
                transform.eulerAngles = new Vector3(0, transform.rotation.eulerAngles.y + 90, 0);
                rotation++;
            }
        }
        else if (rotation == 2)
        {
            cam.rotating = false;
            rotation = 0;
            anim.SetBool("walking", false);
        }
    }

    public void GetLeftTarget()
    {
        if (pointIndex == 0)
        {
            pointIndex = 3;
        }
        else
        {
            pointIndex--;
        }

        corner = currentPoint.GetComponent<Waypoint>().leftCorner;
        target = centerPoints[pointIndex];
        currentPoint = centerPoints[pointIndex];
    }

    /// <summary>
    /// Movimiento a la derecha.
    /// Movement to the right.
    /// </summary>
    public void MoveRight()
    {
        if (rotation == 0)
        {
            transform.position = Vector3.Lerp(transform.position, corner.position, autoSpeed * Time.deltaTime);
            anim.SetBool("walking", true);

            if (Vector3.Distance(transform.position, corner.position) <= waypointRadius)
            {
                transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                rotation++;
            }
        }
        else if (rotation == 1)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, autoSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) <= waypointRadius)
            {
                transform.eulerAngles = new Vector3(0, transform.rotation.eulerAngles.y - 90, 0);
                rotation++;
            }
        }
        else if (rotation == 2)
        {
            cam.rotating = false;
            rotation = 0;
            anim.SetBool("walking", false);
        }
    }

    public void GetRightTarget()
    {
        if (pointIndex == 3)
        {
            pointIndex = 0;
        }
        else
        {
            pointIndex++;
        }

        corner = currentPoint.GetComponent<Waypoint>().rightCorner;
        target = centerPoints[pointIndex];
        currentPoint = centerPoints[pointIndex];
    }
}