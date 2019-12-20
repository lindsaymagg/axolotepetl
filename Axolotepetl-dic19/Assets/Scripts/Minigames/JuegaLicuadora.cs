using TMPro;
using UnityEngine;

public class JuegaLicuadora : MonoBehaviour
{
    //funciona bien...no toques por favor!
    //si hay fallas, me avisas

    public ChefMovement cheffy;
    Renderer flechaMesh;
    public float pausa = 1.5f;

    public GameObject barra;
    public GameObject flecha;
    public GameObject showTimer;
    public GameObject walls;

    public mueveFlechaRallar movimientoDeFlecha;
    public Vector3 startPos = new Vector3(-0.68f, 0.9671009f, -1.049529f);
    public MashingTime timer;

    private bool exito = false;

    private Animator anim;

    private AudioSource source;
    public AudioClip exit;


    // Start is called before the first frame update
    void Start()
    {
        if (cheffy.chefIndex == 0)
            anim = GameObject.Find("ChefMujer").GetComponent<Animator>();

        if (cheffy.chefIndex == 1)
            anim = GameObject.Find("ChefHombre").GetComponent<Animator>();

        source = GameObject.Find("MainCamera").GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        //SONIDO DE EMPEZAR MINIJUEGO
        //CHECA SCRIPT DE MUEVE FLECHARALLAR

        Debug.Log("Empieza LICUADORA.");
        //don't know if this works but let's try
        flechaMesh = flecha.GetComponent<Renderer>();
        movimientoDeFlecha.estado = movimientoDeFlecha.jugandoLicuadora;
        barra.SetActive(true);
        showTimer.SetActive(true);
        flecha.SetActive(true);
        walls.SetActive(true);

        exito = false;
        movimientoDeFlecha.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        float miY = flecha.transform.position.y;

        if (exito == false)
        {
            if (miY >= 1.65f && miY <= 2.01)
            {
                timer.enabled = true;
                showTimer.SetActive(true);
            }

            else
            {
                showTimer.SetActive(false);
                timer.enabled = false;
            }

            if (showTimer.GetComponent<TextMeshProUGUI>().text == "0")
            {
                timer.enabled = false;
                showTimer.GetComponent<TextMeshProUGUI>().text = " ";
                showTimer.SetActive(false);
                exito = true;
                EndJuego();

            }

            if (Input.GetKeyDown("escape"))
            {
                timer.enabled = false;
                source.PlayOneShot(exit);
                showTimer.SetActive(false);
                exito = false;
                EndJuego();

            }
        }

        //  The easy way out.
        if (Input.GetKeyDown("y"))
        {
            timer.enabled = false;
            showTimer.GetComponent<TextMeshProUGUI>().text = " ";

            exito = true;


            EndJuego();
        }
    }

    private void EndJuego()
    {
        barra.SetActive(false);
        showTimer.GetComponent<TextMeshProUGUI>().text = " ";
        showTimer.SetActive(false);
        walls.SetActive(false);
        flechaMesh.enabled = false;
        flecha.transform.position = startPos;
        flecha.SetActive(false);
        timer.enabled = false;
        movimientoDeFlecha.enabled = false;

        anim.SetBool("cooking", false);
        cheffy.master.EndLicuadora(exito);


    }
}
