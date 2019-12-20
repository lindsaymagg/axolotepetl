using UnityEngine;

public class MealsManager : MonoBehaviour
{
    public float[] mealsProbs;
    public Meal[] mealsInLevel;

    private float var;

    public Meal ChooseMeal()
    {
        Meal order;

        var = Choose(mealsProbs);
        //Debug.Log("var: " + var);

        order = mealsInLevel[(int)var];
        return order;
    }

    public float Choose(float[] probs)
    {
        float total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;
        //Debug.Log("randomPoint: " + randomPoint);

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }

        return probs.Length - 1;
    }
}
