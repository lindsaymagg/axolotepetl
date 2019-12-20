using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, COUNTING };

    [System.Serializable]
    public class Group
    {
        public int count;

        public Client prefab;
        public MealsUI UI;
        public SpawnPoints spawn;

        [HideInInspector] public int numOrders;
        [HideInInspector] public Transform[] spawnPoints;
        [HideInInspector] public int spawnIndex = 0;
    }

    public Client client;
    public GameMaster master;

    public Group[] groups;
    public static int groupIndex;

    [HideInInspector] public int clientID;
    [HideInInspector] public int cubeSideID;
    [HideInInspector] public int listIndex = 0;

    public float timeBetweenGroups;

    public List<Meal>[] mealLists = new List<Meal>[4];

    public Text countdownText;
    public GameObject nextGroupText;
    public GameObject spawnCountdownText;

    public readonly float delay = 1.5f;
    [HideInInspector] public float countdown;

    [HideInInspector] public SpawnState state = SpawnState.COUNTING;

    // Start is called before the first frame update
    void Start()
    {
        clientID = 0;
        cubeSideID = 0;
        countdown = 2.5f;

        groupIndex = 0;

        nextGroupText.SetActive(false);
        spawnCountdownText.SetActive(false);

        for (int i = 0; i < mealLists.Length; i++)
        {
            mealLists[i] = master.mealLists[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (groupIndex + 1 > groups.Length)
        {
            nextGroupText.SetActive(false);
            spawnCountdownText.SetActive(false);
            return;
        }

        if (countdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                clientID = 0;
                _ = StartCoroutine(Spawn(groups[groupIndex]));
                nextGroupText.SetActive(false);
                spawnCountdownText.SetActive(false);
            }
        }
        else
        {
            countdown -= Time.deltaTime;

            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

            countdownText.text = Mathf.Floor(countdown).ToString();
        }
    }

    public IEnumerator Spawn(Group g)
    {
        state = SpawnState.SPAWNING;

        g.spawnPoints = new Transform[4];

        g.spawn.ChooseSpawnPoints();
        g.numOrders = Random.Range(g.count, g.count + 2);

        for (int i = 0; i < g.count; i++)
        {
            g.spawnPoints[i] = g.spawn.spawns[i];

            if (g.numOrders <= g.count)
            {
                SpawnClient(g.prefab, g.spawnPoints[g.spawnIndex], 1, g.UI, mealLists[listIndex], clientID, cubeSideID);
            }
            else if (g.numOrders > g.count)
            {
                SpawnClient(g.prefab, g.spawnPoints[g.spawnIndex], 2, g.UI, mealLists[listIndex], clientID, cubeSideID);
            }

            g.numOrders--;
            g.spawnIndex++;
            clientID++;

            yield return new WaitForSeconds(delay);
        }

        listIndex++;
        cubeSideID++;

        state = SpawnState.COUNTING;
        countdown = timeBetweenGroups;

        yield return new WaitForSeconds(2f);

        groupIndex++;

        g.spawn.RemoveSpawnPoints();
        nextGroupText.SetActive(true);
        spawnCountdownText.SetActive(true);
    }

    /// <summary>
    /// Hacer spawn de un cliente. Inicializar valores del cliente: lado de la cocina (sideID), ID,
    /// número de meals para pedir
    /// Spawns a client. Sets values: sideID, ID
    /// </summary>
    /// <param name="c"></param> Client prefab
    /// <param name="sp"></param> posición de un spawn point / position of a spawn point
    /// <param name="orders"></param> número de platillos para pedir / number of dishes to order
    /// <param name="ui"></param>
    /// <param name="meals"></param> lista de opciones de platillos para pedir / list of options of dishes to order
    /// <param name="id"></param> ID de cliente, client's ID
    /// <param name="side"></param>
    private void SpawnClient(Client c, Transform sp, int orders, MealsUI ui, List<Meal> meals, int id, int side)
    {
        client = Instantiate(c, sp.transform.position, sp.transform.rotation);

        client.ID = id;
        client.sideID = side;

        client.UI = ui;
        client.meals = meals;
        client.numMeals = orders;
    }
}