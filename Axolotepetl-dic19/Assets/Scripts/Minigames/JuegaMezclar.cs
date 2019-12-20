using TMPro;
using UnityEngine;

public class JuegaMezclar : MonoBehaviour
{
    public ChefMovement cheffy;

    public int maxSecuencia = 5;
    public int winsNeeded = 3;
    public float pausaEntreFallos = 1.25f;
    public float pausaSecuencia = .85f;
    public float pausaInicio = 3f;

    public GameObject teclas;
    public GameObject teclaA;
    public GameObject teclaS;
    public GameObject teclaW;
    public GameObject teclaD;

    public GameObject mez0;
    public GameObject mezA;
    public GameObject mezS;
    public GameObject mezW;
    public GameObject mezD;

    private int currNumWins;
    private int currBoton;
    private int generating = 0;

    private bool exito = false;

    private Animator anim;

    public TextMeshProUGUI counter;

    //states
    private int waiting = 1;
    private int validating = 2;
    private int state;

    int k;
    int[] miSecuencia;
    int validator;
    private float waitTime;
    private float waitTime2;
    private bool gameEnded = false;

    public AudioClip escape;
    public AudioClip button;
    public AudioClip wrong;
    public AudioClip generate;

    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        state = waiting;
        miSecuencia = new int[maxSecuencia];
        k = 0;
        waitTime = pausaInicio;
        validator = 0;
        source = GameObject.Find("MainCamera").GetComponent<AudioSource>();

        if (cheffy.chefIndex == 0)
            anim = GameObject.Find("ChefMujer").GetComponent<Animator>();

        if (cheffy.chefIndex == 1)
            anim = GameObject.Find("ChefHombre").GetComponent<Animator>();
    }

    private void OnEnable()
    {
        //SONIDO DE EMPEZAR MINIJUEGO

        Debug.Log("Empieza MEZCLAR.");
        currNumWins = 0;

        mez0.SetActive(true);
        mezA.SetActive(false);
        mezS.SetActive(false);
        mezD.SetActive(false);
        mezW.SetActive(false);

        teclas.SetActive(true);

        exito = false;
        gameEnded = false;

        counter = GameObject.FindGameObjectWithTag("CounterMez").GetComponent<TextMeshProUGUI>();
        counter.text = winsNeeded.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            currNumWins = 0;
            exito = false;
            gameEnded = true;
            Invoke("EndJuego", .5f);
            source.PlayOneShot(escape);
        }

        if (gameEnded == false)
        {
            if (currNumWins == winsNeeded)
            {
                //Debug.Log("won game");
                exito = true;
                EndJuego();

            }

            //GENERATING
            if (state == generating)
            {
                if (Time.time - waitTime2 > pausaEntreFallos)
                {
                    miSecuencia[k] = Random.Range(0, 4);
                    //Debug.Log(k + "th element is " + miSecuencia[k]);
                    waitTime = Time.time;
                    currBoton = miSecuencia[k];
                    ActivaBoton();
                    state = waiting;
                    if (k < maxSecuencia)
                    {
                        k++;
                    }
                }
            }

            //WAITING
            if (state == waiting)
            {
                if (Time.time - waitTime > pausaSecuencia)
                {
                    //Debug.Log("YEAH");
                    if (k == maxSecuencia)
                    {
                        DesactivaBoton();
                        k = 0;
                        //printSequence();
                        validator = 0;
                        state = validating;
                        waitTime2 = Time.time;
                    }
                    else
                    {
                        DesactivaBoton();
                        state = generating;
                    }
                }
            }

            //VALIDATING
            if (state == validating)
            {
                //Debug.Log("validator is: "+validator);
                if (validator == miSecuencia.Length)
                {
                    currNumWins++;
                    int miInt = int.Parse(counter.GetParsedText());
                    counter.text = (miInt - 1).ToString();
                    //Debug.Log("Has ganado " + currNumWins + " veces!");
                    waitTime2 = Time.time;
                    state = waiting;
                    validator = 0;
                }

                int nextBoton = miSecuencia[validator];

                if (Input.GetKeyDown("a"))
                {
                    if (nextBoton == 0)
                    {
                        currBoton = 0;
                        ActivaBoton();
                        validator++;
                        ///SONIDO - PRESIONAR TECLA
                        source.PlayOneShot(button);
                        DesactivaBoton();
                    }
                    else
                    {
                        ///SONIDO - FALLAR
                        ///
                        //Debug.Log("A - wrong");
                        validator = 0;
                        waitTime2 = Time.time;
                        counter.text = winsNeeded.ToString();
                        state = waiting;
                        currNumWins = 0;
                        source.PlayOneShot(wrong);
                    }
                }

                if (Input.GetKey("w"))
                {
                    currBoton = 1;
                    ActivaBoton();
                }

                else if (Input.GetKey("a"))
                {
                    currBoton = 0;
                    ActivaBoton();
                }

                else if (Input.GetKey("s"))
                {
                    currBoton = 2;
                    ActivaBoton();
                }

                else if (Input.GetKey("d"))
                {
                    currBoton = 3;
                    ActivaBoton();
                }


                else
                {
                    DesactivaBoton();
                }

                if (Input.GetKeyDown("w"))
                {
                    if (nextBoton == 1)
                    {
                        currBoton = 1;
                        validator++;
                        ///SONIDO - PRESIONAR TECLA
                        source.PlayOneShot(button);
                        DesactivaBoton();
                    }
                    else
                    {
                        ///SONIDO - FALLAR
                        ///
                        //Debug.Log("W - wrong");
                        validator = 0;
                        waitTime2 = Time.time;
                        counter.text = winsNeeded.ToString();
                        state = waiting;
                        currNumWins = 0;
                        source.PlayOneShot(wrong);
                    }
                }

                if (Input.GetKeyDown("s"))
                {
                    if (nextBoton == 2)
                    {
                        currBoton = 2;
                        ActivaBoton();
                        validator++;
                        ///SONIDO - PRESIONAR TECLA
                        source.PlayOneShot(button);
                        DesactivaBoton();
                    }
                    else
                    {
                        ///SONIDO - FALLAR
                        //Debug.Log("S - wrong");
                        validator = 0;
                        waitTime2 = Time.time;
                        counter.text = winsNeeded.ToString();
                        state = waiting;
                        currNumWins = 0;
                        source.PlayOneShot(wrong);
                    }
                }

                if (Input.GetKeyDown("d"))
                {
                    if (nextBoton == 3)
                    {
                        currBoton = 3;
                        ActivaBoton();
                        validator++;
                        ///SONIDO - PRESIONAR TECLA
                        source.PlayOneShot(button);
                        DesactivaBoton();
                    }
                    else
                    {
                        ///SONIDO - FALLAR
                        ///
                        //Debug.Log("D - wrong");
                        validator = 0;
                        waitTime2 = Time.time;
                        counter.text = winsNeeded.ToString();
                        state = waiting;
                        currNumWins = 0;
                        source.PlayOneShot(wrong);
                    }
                }
            }
        }

        //  The easy way out.
        if (Input.GetKeyDown("y"))
        {
            exito = true;
            EndJuego();
        }
    }

    private void PrintSequence()
    {
        for (int i = 0; i < miSecuencia.Length; i++)
        {
            print(miSecuencia[i]);
        }
    }

    private void ActivaBoton()
    {
        //Debug.Log("activando " + currBoton);

        if (state == generating)
        {
            /////SONIDO DE ACTIVAR BOTON (puede sonar como luz??)
            source.PlayOneShot(generate);
        }

        //if A
        if (currBoton == 0)
        {
            mezA.SetActive(true);
            teclaA.SetActive(true);
        }

        //if W
        if (currBoton == 1)
        {
            mezW.SetActive(true);
            teclaW.SetActive(true);
        }

        //if S
        if (currBoton == 2)
        {
            mezS.SetActive(true);
            teclaS.SetActive(true);
        }

        //if D
        if (currBoton == 3)
        {
            mezD.SetActive(true);
            teclaD.SetActive(true);
        }
    }

    private void DesactivaBoton()
    {
        //Debug.Log("desactivado");
        teclaA.SetActive(false);
        teclaS.SetActive(false);
        teclaW.SetActive(false);
        teclaD.SetActive(false);

        mez0.SetActive(true);
        mezA.SetActive(false);
        mezS.SetActive(false);
        mezW.SetActive(false);
        mezD.SetActive(false);
    }

    private void EndJuego()
    {
        cheffy.master.EndMezclar(exito);

        teclas.SetActive(false);

        teclaA.SetActive(false);
        teclaD.SetActive(false);
        teclaW.SetActive(false);
        teclaS.SetActive(false);

        anim.SetBool("cooking", false);
    }
}
