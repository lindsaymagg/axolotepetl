using System.Collections.Generic;
using UnityEngine;

public class Axolotl : MonoBehaviour
{
    public static Meal[] finalMeals;
    public FinalMealManager finalManager;

    public GameMaster master;

    [HideInInspector] public int orderIndex;

    public float speed;
    public bool alreadyOrdered;

    [HideInInspector] public int numMeals;

    [HideInInspector] public MealsUI UI;
    [HideInInspector] public Animator anim;
    [HideInInspector] public List<Meal> meals;

    public static bool waveSuccesful;

    private int mealIndex;

  

    // Start is called before the first frame update
    private void Awake()
    {
        mealIndex = 0;
        finalMeals = new Meal[6];

        waveSuccesful = false;
        alreadyOrdered = false;

        anim = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (AxolotlTime.nextWaveLoading)
        {
            Destroy(this.gameObject, 2.5f);
            return;
        }

        if (waveSuccesful)
        {
            AxolotlTime.nextWaveLoading = true;
            _ = StartCoroutine(master.NextWave());
            return;
        }

        if (AxolotlSatisfied() && alreadyOrdered)
        {
            waveSuccesful = true;
            Debug.Log("Satisfied");
            anim.SetBool("satisfied", true);
      
        }

        if (!alreadyOrdered)
        {
            for (int i = 0; i < numMeals; i++)
            {
                AxolotlOrder(mealIndex);
                mealIndex++;
            }

            alreadyOrdered = true;
            mealIndex = 0;
        }
    }

    public void AxolotlOrder(int index)
    {
        finalMeals[index] = finalManager.ChooseFinalMeal(orderIndex);

        master.AddMeal(finalMeals[index], meals);

        UI.FillSlot(finalMeals[index]);
    }

    public bool AxolotlSatisfied()
    {
        for (int i = 0; i < finalMeals.Length; i++)
        {
            if (finalMeals[i] != null)
            {
                return false;
            }
        }

        Debug.Log("true");
        return true;
    }
}