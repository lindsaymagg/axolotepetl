using UnityEngine;

public class IngredientsRefri : MonoBehaviour
{
    public Ingredient agua;
    public Ingredient aguacate;
    public Ingredient bolillo;
    public Ingredient carne;
    public Ingredient cebolla;
    public Ingredient chileRojo;
    public Ingredient chileVerde;
    public Ingredient chocolate;
    public Ingredient cilantro;
    public Ingredient elote;
    public Ingredient frijoles;
    public Ingredient jamon;
    public Ingredient crema;
    public Ingredient pollo;
    public Ingredient queso;
    public Ingredient tomate;
    public Ingredient tortillas;
    public Ingredient totopos;

    private IngredientsManager ingredientsManager;

    // Start is called before the first frame update
    void Start()
    {
        ingredientsManager = IngredientsManager.instance;

        agua.state = Ingredient.IngredientState.RAW;
        aguacate.state = Ingredient.IngredientState.RAW;
        bolillo.state = Ingredient.IngredientState.RAW;
        carne.state = Ingredient.IngredientState.RAW;
        cebolla.state = Ingredient.IngredientState.RAW;
        chileRojo.state = Ingredient.IngredientState.RAW;
        chileVerde.state = Ingredient.IngredientState.RAW;
        chocolate.state = Ingredient.IngredientState.RAW;
        cilantro.state = Ingredient.IngredientState.RAW;
        elote.state = Ingredient.IngredientState.RAW;
        frijoles.state = Ingredient.IngredientState.RAW;
        jamon.state = Ingredient.IngredientState.RAW;
        crema.state = Ingredient.IngredientState.RAW;
        pollo.state = Ingredient.IngredientState.RAW;
        queso.state = Ingredient.IngredientState.RAW;
        tomate.state = Ingredient.IngredientState.RAW;
        tortillas.state = Ingredient.IngredientState.RAW;
        totopos.state = Ingredient.IngredientState.RAW;
    }

    #region Getting Ingredients

    public void GetWater()
    {
        Debug.Log("Agua");
        ingredientsManager.SelectIngredient(agua);
    }

    public void GetAvocado()
    {
        Debug.Log("Aguacate");
        ingredientsManager.SelectIngredient(aguacate);
    }

    public void GetBolillo()
    {
        Debug.Log("Bolillo");
        ingredientsManager.SelectIngredient(bolillo);
    }

    public void GetBeef()
    {
        Debug.Log("Res");
        ingredientsManager.SelectIngredient(carne);
    }

    public void GetOnion()
    {
        Debug.Log("Cebolla");
        ingredientsManager.SelectIngredient(cebolla);
    }

    public void GetRedChile()
    {
        Debug.Log("Chile Rojo");
        ingredientsManager.SelectIngredient(chileRojo);
    }

    public void GetGreenChile()
    {
        Debug.Log("Chile Verde");
        ingredientsManager.SelectIngredient(chileVerde);
    }

    public void GetChocolate()
    {
        Debug.Log("Chocolate");
        ingredientsManager.SelectIngredient(chocolate);
    }

    public void GetCoriander()
    {
        Debug.Log("Cilantro");
        ingredientsManager.SelectIngredient(cilantro);
    }

    public void GetCorn()
    {
        Debug.Log("Elote");
        ingredientsManager.SelectIngredient(elote);
    }

    public void GetBeans()
    {
        Debug.Log("Frijoles");
        ingredientsManager.SelectIngredient(frijoles);
    }

    public void GetHam()
    {
        Debug.Log("Jamon");
        ingredientsManager.SelectIngredient(jamon);
    }

    public void GetCrema()
    {
        Debug.Log("Crema");
        ingredientsManager.SelectIngredient(crema);
    }

    public void GetChicken()
    {
        Debug.Log("Pollo");
        ingredientsManager.SelectIngredient(pollo);
    }

    public void GetCheese()
    {
        Debug.Log("Queso");
        ingredientsManager.SelectIngredient(queso);
    }

    public void GetTomato()
    {
        Debug.Log("Tomate");
        ingredientsManager.SelectIngredient(tomate);
    }

    public void GetTortillas()
    {
        Debug.Log("Tortillas");
        ingredientsManager.SelectIngredient(tortillas);
    }

    public void GetTotopos()
    {
        Debug.Log("Totopos");
        ingredientsManager.SelectIngredient(totopos);
    }

    #endregion

}
