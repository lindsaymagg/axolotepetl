using UnityEngine;

public class GanaEmplatado : MonoBehaviour
{
    public GameObject imagen0;
    public GameObject imagen1;
    public GameObject imagen2;
    public GameObject imagen3;
    public GameObject imagen4;
    public GameObject imagen5;
    public GameObject imagen6;
    public GameObject imagen7;
    public GameObject imagen8;

    public GameObject ganoText;

    public JuegaEmplatado juegaEmplatado;

    public bool exito = false;
    public bool alreadyWon = false;

    private void OnEnable()
    {
        //ganoText.SetActive(false);
        //exito = false;
        alreadyWon = false;
        exito = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (alreadyWon == false && ((int)imagen0.transform.localEulerAngles.z == 0) && ((int)imagen1.transform.localEulerAngles.z == 0) && ((int)imagen2.transform.localEulerAngles.z == 0) && ((int)imagen3.transform.localEulerAngles.z == 0) && ((int)imagen4.transform.localEulerAngles.z == 0) && ((int)imagen5.transform.localEulerAngles.z == 0) && ((int)imagen6.transform.localEulerAngles.z == 0) && ((int)imagen7.transform.localEulerAngles.z == 0) && ((int)imagen8.transform.localEulerAngles.z == 0))
        {
            //ganoText.SetActive(true);

            exito = true;
            alreadyWon = true;

            EndJuego();
        }

        //  The easy way out.
        if (Input.GetKeyDown("y"))
        {
            //ganoText.SetActive(true);

            exito = true;
            alreadyWon = true;

            EndJuego();
        }
    }

    private void EndJuego()
    {
        //Debug.Log("ganaste emplatado!");
        juegaEmplatado.EndJuego(exito);
    }
}
