using UnityEngine;

public class JuegaEmplatado : MonoBehaviour
{
    public GameMaster master;
    public ChefMovement cheffy;

    public GameObject quesadillaImagen;
    public GameObject sopaDeTomateImagen;
    public GameObject guacamoleImagen;
    public GameObject sincronizadasImagen;
    public GameObject sopaDeTortillaImagen;
    public GameObject flautasImagen;
    public GameObject gorditasImagen;
    public GameObject tacosImagen;
    public GameObject molletesImagen;
    public GameObject chilesEnNogadaImagen;
    public GameObject tostadasImagen;
    public GameObject enchiladasImagen;
    public GameObject moleImagen;
    public GameObject chilaquilesImagen;
    public GameObject pozoleImagen;

    public Meal mealToPlate;

    public AudioClip win;
    public AudioClip loss;

    private Animator anim;
    private AudioSource source;

    private bool exito = false;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();

        if (cheffy.chefIndex == 0)
            anim = GameObject.Find("ChefMujer").GetComponent<Animator>();

        if (cheffy.chefIndex == 1)
            anim = GameObject.Find("ChefHombre").GetComponent<Animator>();
    }

    private void OnEnable()
    {
        //SONIDO DE EMPEZAR MINIJUEGO
        //CHECA SCRIPT DE ROTACION

        Debug.Log("Empieza EMPLATADO.");
        quesadillaImagen.SetActive(false);
        sopaDeTomateImagen.SetActive(false);
        guacamoleImagen.SetActive(false);
        sincronizadasImagen.SetActive(false);
        sopaDeTortillaImagen.SetActive(false);
        flautasImagen.SetActive(false);
        molletesImagen.SetActive(false);
        tacosImagen.SetActive(false);
        gorditasImagen.SetActive(false);
        chilesEnNogadaImagen.SetActive(false);
        tostadasImagen.SetActive(false);
        enchiladasImagen.SetActive(false);
        moleImagen.SetActive(false);
        pozoleImagen.SetActive(false);
        chilaquilesImagen.SetActive(false);

        mealToPlate = cheffy.mealToDeliver;

        Debug.Log(mealToPlate);

        if (mealToPlate.name == "Sincronizadas")
        {
            sincronizadasImagen.SetActive(true);
        }

        if (mealToPlate.name == "Quesadillas")
        {
            quesadillaImagen.SetActive(true);
        }

        if (mealToPlate.name == "Sopatomate")
        {
            sopaDeTomateImagen.SetActive(true);
        }

        if (mealToPlate.name == "Guacamole")
        {
            guacamoleImagen.SetActive(true);
        }

        if (mealToPlate.name == "Sopatortilla")
        {
            sopaDeTortillaImagen.SetActive(true);
        }

        if (mealToPlate.name == "Flautas")
        {
            flautasImagen.SetActive(true);
        }

        if (mealToPlate.name == "Gorditas")
        {
            gorditasImagen.SetActive(true);
        }

        if (mealToPlate.name == "Tacos")
        {
            tacosImagen.SetActive(true);
        }

        if (mealToPlate.name == "Molletes")
        {
            molletesImagen.SetActive(true);
        }
        if (mealToPlate.name == "ChilesEnNogada")
        {
            chilesEnNogadaImagen.SetActive(true);
        }
        if (mealToPlate.name == "Enchiladas")
        {
            enchiladasImagen.SetActive(true);
        }
        if (mealToPlate.name == "Tostadas")
        {
            tostadasImagen.SetActive(true);
        }
        if (mealToPlate.name == "Mole")
        {
            moleImagen.SetActive(true);
        }
        if (mealToPlate.name == "Chilaquiles")
        {
            chilaquilesImagen.SetActive(true);
        }
        if (mealToPlate.name == "Pozole")
        {
            pozoleImagen.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (mealToPlate != null)
        {
            if (Input.GetKeyDown("escape"))
            {
                DoNothing();
            }

            //  The easy way out.
            if (Input.GetKeyDown("y"))
            {
                exito = true;
                EndJuego(exito);
            }
        }
        else
        {
            DoNothing();
            Debug.Log("Eso no es un platillo.");
        }
    }

    public void EndJuego(bool exitoo)
    {
        quesadillaImagen.SetActive(false);
        sopaDeTomateImagen.SetActive(false);
        guacamoleImagen.SetActive(false);
        sincronizadasImagen.SetActive(false);
        sopaDeTortillaImagen.SetActive(false);
        flautasImagen.SetActive(false);
        molletesImagen.SetActive(false);
        tacosImagen.SetActive(false);
        gorditasImagen.SetActive(false);
        chilesEnNogadaImagen.SetActive(false);
        tostadasImagen.SetActive(false);
        enchiladasImagen.SetActive(false);
        moleImagen.SetActive(false);
        pozoleImagen.SetActive(false);
        chilaquilesImagen.SetActive(false);
        cheffy.master.EndEmplatado(exitoo, mealToPlate);
        source.PlayOneShot(win);
        anim.SetBool("cooking", false);
    }

    private void DoNothing()
    {
        EndJuego(false);
    }
}
