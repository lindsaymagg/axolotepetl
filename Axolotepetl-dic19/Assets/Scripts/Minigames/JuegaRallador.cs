using TMPro;
using UnityEngine;

public class JuegaRallador : MonoBehaviour
{
    //funciona bien...no toques por favor!
    //si hay fallas, me avisas

    public ChefMovement cheffy;

    public float pausa = 1.5f;

    public GameObject barra;
    public GameObject flecha;
    public GameObject showTimer;
    public GameObject walls;

    Renderer flechaMesh;
    public mueveFlechaRallar movimientoDeFlecha;
    public Vector3 startPos = new Vector3(-0.68f, 0.9671009f, -1.049529f);
    public MashingTime timer;

    private bool exito = false;

    private Animator anim;

    private AudioSource source;
    public AudioClip win;
    public AudioClip exit;


    // Start is called before the first frame update
    void Start()
    {
        source = GameObject.Find("MainCamera").GetComponent<AudioSource>();

        if (cheffy.chefIndex == 0)
            anim = GameObject.Find("ChefMujer").GetComponent<Animator>();

        if (cheffy.chefIndex == 1)
            anim = GameObject.Find("ChefHombre").GetComponent<Animator>();
    }

    private void OnEnable()
    {
        //SONIDO DE EMPEZAR MINIJUEGO
        //CHECA SCRIPT DE MUEVE FLECHARALLAR

        //anim.SetBool("cooking", true);

        flechaMesh = flecha.GetComponent<Renderer>();

        Debug.Log("Empieza RALLAR.");

        movimientoDeFlecha.enabled = true;
        movimientoDeFlecha.estado = movimientoDeFlecha.jugandoRallador;

        barra.SetActive(true);
        showTimer.SetActive(true);
        flecha.SetActive(true);
        walls.SetActive(true);

        exito = false;
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
                showTimer.GetComponent<TextMeshProUGUI>().text = "0";
                showTimer.SetActive(false);
                exito = true;
                source.PlayOneShot(win);
                EndJuego();
            }

            if (Input.GetKeyDown("escape"))
            {
                timer.enabled = false;
                exito = false;
                showTimer.SetActive(false);
                source.PlayOneShot(exit);
                EndJuego();
            }
        }

        //  The easy way out.
        if (Input.GetKeyDown("y"))
        {
            showTimer.GetComponent<TextMeshProUGUI>().text = " ";
            showTimer.SetActive(false);

            exito = true;
            source.PlayOneShot(win);

            EndJuego();
        }
    }

    private void EndJuego()
    {
        timer.enabled = false;
        barra.SetActive(false);
        flechaMesh.enabled = false;
        flecha.transform.position = startPos;
        flecha.SetActive(false);
        movimientoDeFlecha.enabled = false;
        walls.SetActive(false);
        showTimer.GetComponent<TextMeshProUGUI>().text = " ";
        cheffy.master.EndRallar(exito);
        anim.SetBool("cooking", false);
    }
}
