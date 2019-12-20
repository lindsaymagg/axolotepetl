using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Ingredient")]
public class Ingredient : ScriptableObject
{
    public enum IngredientState { RAW, COOKED };

    public new string name;

    public Sprite sprite;

    public IngredientState state;

    public void CookedIngredient()
    {
        state = IngredientState.COOKED;
    }
}