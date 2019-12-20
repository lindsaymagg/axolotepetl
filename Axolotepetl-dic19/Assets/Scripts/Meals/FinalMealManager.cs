using UnityEngine;

public class FinalMealManager : MealsManager
{
    [Header("First Order")]
    public float[] firstProbs;
    public Meal[] firstMeals;

    [Header("Second Order")]
    public float[] secondProbs;
    public Meal[] secondMeals;

    [Header("Third Order")]
    public float[] thirdProbs;
    public Meal[] thirdMeals;

    [Header("Fourth Order")]
    public float[] fourthProbs;
    public Meal[] fourthMeals;

    [Header("Fifth Order")]
    public float[] fifthProbs;
    public Meal[] fifthMeals;

    [Header("Sixth Order")]
    public float[] sixthProbs;
    public Meal[] sixthMeals;

    private float finalMealIndex;

    public Meal ChooseFinalMeal(int index)
    {
        Meal order;

        if (index == 0)
        {
            finalMealIndex = Choose(firstProbs);
            order = firstMeals[(int)finalMealIndex];
            return order;
        }

        if (index == 1)
        {
            finalMealIndex = Choose(secondProbs);
            order = secondMeals[(int)finalMealIndex];
            return order;
        }

        if (index == 2)
        {
            finalMealIndex = Choose(thirdProbs);
            order = thirdMeals[(int)finalMealIndex];
            return order;
        }

        if (index == 3)
        {
            finalMealIndex = Choose(fourthProbs);
            order = fourthMeals[(int)finalMealIndex];
            return order;
        }

        if (index == 4)
        {
            finalMealIndex = Choose(fifthProbs);
            order = fifthMeals[(int)finalMealIndex];
            return order;
        }

        if (index == 5)
        {
            finalMealIndex = Choose(sixthProbs);
            order = sixthMeals[(int)finalMealIndex];
            return order;
        }

        return null;
    }
}
