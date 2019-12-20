using UnityEngine;

public class Rotacion : MonoBehaviour
{
    public bool stopRotacion = false;
    public GanaEmplatado ganaEmplatado;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        stopRotacion = false;

        int myInt = Random.Range(1, 4);
        if (myInt == 1)
        {
            transform.Rotate(0f, 0f, 90f);


        }
        if (myInt == 2)
        {
            transform.Rotate(0f, 0f, 180f);

        }
        if (myInt == 3)
        {
            transform.Rotate(0f, 0f, 270f);

        }

    }

    // Update is called once per frame
    void Update()
    {
        //stopRotacion = ganaEmplatado.alreadyWon;
        //Debug.Log(stopRotacion);

    }

    private void OnMouseDown()
    {
        if (stopRotacion == false)
        {
            transform.Rotate(0f, 0f, 90f);
            ///SONIDO DE ROTACION
        }
    }
}
