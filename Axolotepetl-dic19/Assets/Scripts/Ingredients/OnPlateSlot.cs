using UnityEngine;
using UnityEngine.UI;

public class OnPlateSlot : MonoBehaviour
{
    public Image icon;

    private Ingredient ingredient;

    public void SetIngredient(Ingredient ing)
    {
        ingredient = ing;
        icon.sprite = ingredient.sprite;
        icon.color = Color.white;
    }

    public void RemoveIngredient()
    {
        ingredient = null;
        icon.sprite = null;
        icon.color = Color.clear;
    }
}
