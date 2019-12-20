using UnityEngine;
using UnityEngine.UI;

public class MealSlot : MonoBehaviour
{
    public Meal meal;
    public Image icon;

    public void SetMeal(Meal item)
    {
        meal = item;
        icon.sprite = meal.sprite;
        icon.color = Color.white;
    }

    public void RemoveMeal()
    {
        meal = null;
        icon.sprite = null;
        icon.color = Color.clear;
    }
}
