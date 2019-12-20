using TMPro;
using UnityEngine;

/// <summary>
/// Jugar el minijuego de Estufa.
/// Play stove minigame.
/// </summary>
public class JuegaEstufa : MonoBehaviour
{
    public ChefMovement cheffy;

    public int numWins = 3;
    public int flechaSpeed = 280;
    public float pauseTime = .8f;

    public TextMeshProUGUI counter;

    public GameObject miniJuegoEstufa;
    public GameObject flecha;

    bool goRight;
    bool exito = false;

    int estado;
    private readonly int swinging = 0;
    private readonly int waiting = 1;

    private int currNumWins;
    private float timePassed;

    private Animator anim;

    private AudioSource source;

    public AudioClip right;
    public AudioClip wrong;
    public AudioClip escape;




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
        /////SONIDO DE EMPEZAR MINIJUEGO
        currNumWins = 0;
        estado = swinging;


        //flecha.transform.eulerAngles = new Vector3(0, 0, 48.83f);
        flecha.SetActive(true);
        miniJuegoEstufa.SetActive(true);

        counter = GameObject.FindGameObjectWithTag("CounterE").GetComponent<TextMeshProUGUI>();
        counter.text = numWins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //int myZ = (int)flecha.transform.rotation.eulerAngles.z;
        int myZlocal = (int)flecha.transform.localEulerAngles.z;

        //Debug.Log("myZ is " + myZ);
        //Debug.Log("myZlocal is " + myZlocal);

        if (Input.GetKeyDown("escape"))
        {
            currNumWins = 0;
            exito = false;
            source.PlayOneShot(escape);
            EndJuego();
        }

        //HEMOS GANADO?
        if (currNumWins == numWins)
        {
            currNumWins = 0;
            exito = true;
            EndJuego();
        }

        if (estado == swinging)
        {
            //Debug.Log("swinging");

            //CHECANDO POR ACCION DE USUARIO
            if (Input.GetKeyDown("space"))
            {
                //Debug.Log("pressed space");
                timePassed = Time.time;
                estado = waiting;
                CheckForHit();
            }

            //MOVIMIENTO DE LA FLECHA
            if (myZlocal <= 50 && myZlocal >= -1)
            {
                //Debug.Log("go right! myZ is " + myZlocal);
                goRight = true;
            }

            if (myZlocal <= 257 && myZlocal >= 220)
            {
                //Debug.Log("go left! myZ is " + myZlocal);
                goRight = false;
            }

            if (goRight == true)
            {
                flecha.transform.localEulerAngles = new Vector3(0, 0, myZlocal - (flechaSpeed * Time.deltaTime));
            }

            else
            {
                flecha.transform.localEulerAngles = new Vector3(0, 0, myZlocal + (flechaSpeed * Time.deltaTime));
            }
        }

        if (estado == waiting)
        {
            //Debug.Log("waiting");
            if (Time.time - timePassed < pauseTime)
            {
                //  //
            }
            else
            {
                //Debug.Log("done waiting");
                //reactivateFlecha();
                estado = swinging;
            }
        }

        //  The easy way out.
        if (Input.GetKeyDown("y"))
        {
            currNumWins = 0;
            exito = true;
            EndJuego();
        }
    }

    private void CheckForHit()
    {
        int myZ = (int)flecha.transform.localEulerAngles.z;
        Debug.Log("myZ is " + myZ);

        if ((myZ >= 291 && myZ <= 315) && goRight == true)
        {
            currNumWins += 1;
            source.PlayOneShot(right);
            int miInt = int.Parse(counter.GetParsedText());
            counter.text = (miInt - 1).ToString();
        }
        else if ((myZ >= 283 && myZ <= 307) && goRight == false)
        {
            currNumWins += 1;
            source.PlayOneShot(right);
            int miInt = int.Parse(counter.GetParsedText());
            counter.text = (miInt - 1).ToString();
        }
        else
        {
            //Debug.Log("buen intento, pero NO");
            currNumWins = 0;

            ////////SONIDO DE FALLAR
            source.PlayOneShot(wrong);
            counter.text = numWins.ToString();
        }
    }

    private void EndJuego()
    {
        cheffy.master.EndEstufa(exito);

        flecha.SetActive(false);
        miniJuegoEstufa.SetActive(false);
        anim.SetBool("cooking", false);

    }

    private void ReactivateFlecha()
    {
        flecha.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
}
