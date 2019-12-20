using UnityEngine;

public class MealsUI : MonoBehaviour
{
    public Chef cheffy;

    private int slotIndex;
    private MealSlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        slotIndex = 0;
        slots = GetComponentsInChildren<MealSlot>();
    }

    public void FillSlot(Meal meal)
    {
        slots[slotIndex].SetMeal(meal);
        slotIndex++;
    }

    public void EmptySlot()
    {
        Debug.Log(this);

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null)
            {
                if (slots[i].meal == cheffy.mealToDeliver)
                {
                    Debug.Log("si");

                    slots[i].RemoveMeal();
                    break;
                }
                else
                {
                    Debug.Log("no " + slots[i].meal);
                }
            }
        }
    }

    public void CleanAllSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null)
            {
                slots[i].RemoveMeal();
            }
        }
    }
}
