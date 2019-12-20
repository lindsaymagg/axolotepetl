using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Crear una imagen de un ingrediente o un platillo que flota sobre la cabeza del chef.
/// Creates an image an ingredient or a dish to float over the head of the chef.
/// </summary>
public class IngredientUI : MonoBehaviour
{
    public Chef chef;

    public Image icon;

    // el canvas que mostrará la imagen
    // the canvas that will show the image
    public GameObject canvas;

    //current dish being held by chef 
    private Meal plateToServe;

    //current ingredient being held by chef 
    private Ingredient current;

    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(chef.transform.position.x, transform.position.y, chef.transform.position.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, chef.transform.eulerAngles.y, chef.transform.eulerAngles.z);

        if (current != null || plateToServe != null)
        {
            canvas.SetActive(true);

            if (current != null)
            {
                if (current.state == Ingredient.IngredientState.COOKED)
                {
                    icon.color = Color.cyan;
                }
                else
                {
                    icon.color = Color.white;
                }
            }
            else
            {
                icon.color = Color.white;
                return;
            }
        }
        else
        {
            Hide();
        }
    }

    /// <summary>
    /// Configurar ingrediente.
    /// Set ingredient.
    /// </summary>
    /// <param name="item"></param>
    public void SetIngredient(Ingredient item)
    {
        current = item;
        icon.sprite = item.sprite;
    }

    /// <summary>
    /// Quitar ingrediente.
    /// Remove ingredient.
    /// </summary>
    public void RemoveIngredient()
    {
        current = null;
    }

    /// <summary>
    /// Configurar platillo.
    /// Set dish.
    /// </summary>
    /// <param name="meal"></param>
    public void SetMeal(Meal meal)
    {
        plateToServe = meal;
        icon.sprite = meal.sprite;
    }

    /// <summary>
    /// Quitar platillo.
    /// Remove dish.
    /// </summary>
    public void RemoveMeal()
    {
        plateToServe = null;
    }

    /// <summary>
    /// Esconder imagen.
    /// Hide image.
    /// </summary>
    public void Hide()
    {
        canvas.SetActive(false);
    }
}
