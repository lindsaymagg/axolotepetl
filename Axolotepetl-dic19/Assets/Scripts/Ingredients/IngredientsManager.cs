using UnityEngine;

public class IngredientsManager : MonoBehaviour
{
    #region Singleton

    public static IngredientsManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Manager in scene!");
        }
        instance = this;
    }

    #endregion

    public IngredientUI UI;
    public ChefMovement cheffy;

    private Meal mealToDeliver;
    private Ingredient ingredientToUse;

    public bool CanPlay()
    { return ingredientToUse != null; }

    public void SelectIngredient(Ingredient ingredient)
    {
        ingredientToUse = ingredient;

        //cheffy.transform.eulerAngles = new Vector3(0, cheffy.transform.rotation.eulerAngles.y - 180, 0);
        cheffy.currentIngredient = ingredientToUse;
        cheffy.master.CloseRefri();

        UI.SetIngredient(ingredientToUse);
    }

    public void GetMeal(Meal meal)
    {
        mealToDeliver = meal;
        cheffy.mealToDeliver = mealToDeliver;
        UI.SetMeal(mealToDeliver);
    }

    public void ResetIngredient()
    {
        ingredientToUse = null;
        UI.RemoveIngredient();
    }

    public void ResetMeal()
    {
        mealToDeliver = null;
        UI.RemoveMeal();
    }
}

