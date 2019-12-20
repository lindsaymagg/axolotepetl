using UnityEngine;
using UnityEngine.UI;

public class Recetario : MonoBehaviour
{
   /* public Sprite Page1;
    public Sprite Page2;
    public Sprite Page3;
    public Sprite Page4;
    public Sprite Page5;
    public Sprite Page6;
    public Sprite Page7;
    public Sprite Page8;
    public Sprite Page9;
    public Sprite Page10;*/

    public Sprite[] Pages;
    public int pageIndex;

    public Image nextPage;

    public GameMaster master;
    public GameObject recetas;

    private void OnEnable()
    {
        pageIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OpenBook();
        }
        nextPage.sprite = Pages[pageIndex];
    }


    public void OpenBook()
    {
        recetas.SetActive(!recetas.activeSelf);
        master.GUI = recetas.activeSelf;
    }

    public void Forward()
    {
        pageIndex++; 

    }

    public void Back() {
        pageIndex--; 
    }
}
