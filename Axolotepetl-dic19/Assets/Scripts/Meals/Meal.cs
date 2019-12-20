using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Meal", menuName = "Meal")]
public class Meal : ScriptableObject
{
    public new string name;

    public Sprite sprite;

    public Ingredient[] requiredIngredients = new Ingredient[0];

    public bool IsCompleted(List<Ingredient> ingredients)
    {
        for (int i = 0; i < requiredIngredients.Length; i++)
        {
            if (!ingredients.Contains(requiredIngredients[i]))
                return false;
        }

        return true;
    }
}