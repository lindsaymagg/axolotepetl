using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxolotlSpawner : GroupSpawner
{
    public Axolotl xolotl;
    public Axolotl prefab;
    public FinalMealManager mealManager;

    public static int randomIndex;

    private int orders;
    private int xolotlIndex;

    private static int numXolotl;
    private static int maxXolotl;

    // Start is called before the first frame update
    void Start()
    {
        clientID = 0;
        cubeSideID = 0;
        countdown = 2.5f;

        groupIndex = 0;
        xolotlIndex = 0;

        numXolotl = 1;
        maxXolotl = 6;

        _ = StartCoroutine(AxolotlSpawn(0));
    }

    // Update is called once per frame
    void Update()
    {
        if (numXolotl >= maxXolotl && Axolotl.waveSuccesful)
            _ = StartCoroutine(master.GameWon());
    }

    public IEnumerator AxolotlSpawn(int n)
    {
        Debug.Log("Starting the spawning process");
        groups[n].spawnPoints = new Transform[4];
        groups[n].spawn.ChooseSpawnPoints();

        for (int i = 0; i < 4; i++)
        {
            groups[n].spawnPoints[i] = groups[n].spawn.spawns[i];
        }

        if (numXolotl < 4)
        {
            orders = Random.Range(2, 4);
        }
        else
        {
            orders = numXolotl == 4 ? Random.Range(3, 5) : 6;
        }

        yield return new WaitForSeconds(2.5f);

        Debug.Log(numXolotl);
        SpawnAxolotl(prefab, groups[n].spawnPoints[Random.Range(0, 4)], orders, groups[n].UI, master.mealLists[randomIndex], xolotlIndex);

        yield return new WaitForSeconds(delay);

        xolotlIndex++;
        numXolotl++;

        yield return new WaitForSeconds(1f);

        groups[n].spawn.RemoveSpawnPoints();
    }

    private void SpawnAxolotl(Axolotl c, Transform sp, int orders, MealsUI ui, List<Meal> meals, int index)
    {
        xolotl = Instantiate(c, sp.transform.position, sp.transform.rotation);

        xolotl.UI = ui;
        xolotl.meals = meals;
        xolotl.numMeals = orders;
        xolotl.orderIndex = index;
    }
}