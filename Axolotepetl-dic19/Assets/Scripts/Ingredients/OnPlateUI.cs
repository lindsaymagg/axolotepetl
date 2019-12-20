using UnityEngine;

public class OnPlateUI : MonoBehaviour
{
    private int slotIndex;
    private OnPlateSlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        slotIndex = 0;
        slots = GetComponentsInChildren<OnPlateSlot>();
    }

    public void FillSlot(Ingredient ingredient)
    {
        slots[slotIndex].SetIngredient(ingredient);
        slotIndex++;
    }

    public void EmptySlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveIngredient();
        }

        slotIndex = 0;
    }
}
