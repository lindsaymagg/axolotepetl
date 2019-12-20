using TMPro;
using UnityEngine;

public class JuegaPicar : MonoBehaviour
{
    public ChefMovement cheffy;

    public float speed;
    public float knifeSpeed = 1f;
    public float spawnTime = .6f;

    public GHboton boton;

    public TextMeshProUGUI counter;

    public GameObject guitarra;
    public GameObject prefabNota;
    public GameObject picarDetector;

    private float lastSpawn;
    private bool exito = false;

    private Vector3 posicionRojo;
    private Vector3 posicionAzul;
    private Vector3 posicionVerde;

    private Animator anim;


    private AudioSource source;

    public AudioClip escape;
    public AudioClip victory;
    public AudioClip button;
    public AudioClip wrong;

    [HideInInspector] public int estado;
    [HideInInspector] public readonly int noChop = 0;
    [HideInInspector] public readonly int chopO = 1;
    [HideInInspector] public readonly int chopK = 2;
    [HideInInspector] public readonly int chopM = 3;

    [HideInInspector] public bool goDownO = false;
    [HideInInspector] public bool goDownK = false;
    [HideInInspector] public bool goDownM = false;

    public GameObject cuchilloO;
    public GameObject cuchilloK;
    public GameObject cuchilloM;

    private readonly float startY = 71.67f;

    // Start is called before the first frame update
    void Start()
    {
        source = GameObject.Find("MainCamera").GetComponent<AudioSource>();

        if (cheffy.chefIndex == 0)
            anim = GameObject.Find("ChefMujer").GetComponent<Animator>();

        if (cheffy.chefIndex == 1)
            anim = GameObject.Find("ChefHombre").GetComponent<Animator>();

        cuchilloO.transform.localPosition = new Vector3(cuchilloO.transform.localPosition.x, startY, cuchilloO.transform.localPosition.z);
        cuchilloK.transform.localPosition = new Vector3(cuchilloK.transform.localPosition.x, startY, cuchilloK.transform.localPosition.z);
        cuchilloM.transform.localPosition = new Vector3(cuchilloM.transform.localPosition.x, startY, cuchilloM.transform.localPosition.z);
    }

    void OnEnable()
    {
        ////SONIDO DE EMPEZAR MINIJUEGO

        ///CHECA SCRIPT DE GHBOTON!!!
        ///
        counter = GameObject.FindGameObjectWithTag("Counter").GetComponent<TextMeshProUGUI>();

        guitarra.SetActive(true);
        boton.SetSpeed(speed);
        picarDetector.SetActive(true);
        exito = false;

        posicionRojo = new Vector3(1.05f, 1.53f, 1.58f);
        posicionVerde = new Vector3(0.95f, 1.39f, 1.58f);
        posicionAzul = new Vector3(0.84f, 1.26f, 1.58f);

        speed = (float)-1.5;

        counter.text = "10";
        lastSpawn = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            exito = false;
            counter.text = " 0 ";
            EndJuego1();
            source.PlayOneShot(escape);
            anim.SetBool("cooking", false);

        }

        if (counter.text == "0")
        {
            exito = true;
            counter.text = "0";
            EndJuego1();


            source.PlayOneShot(victory);
        }

        if (exito != true)
        {
            boton.SetSpeed(speed);
            if (Time.time - lastSpawn >= spawnTime)
            {
                lastSpawn = Time.time;
                int spawnInt = Random.Range(0, 3);
                Spawn(spawnInt);
            }
        }

        //  The easy way out.
        if (Input.GetKeyDown("y"))
        {

            exito = true;
            counter.text = "0";
            EndJuego1();
        }

        //Debug.Log(cuchilloO.transform.localPosition.y);


        //CUCHILLO O
        if (cuchilloO.transform.localPosition.y <= 25)
        {
            goDownO = false;
        }

        if (estado == chopO)
        {
            //Debug.Log("chopping");
            if (goDownO == true)
            {
                cuchilloO.transform.Translate(new Vector3(-1 * knifeSpeed, -1 * knifeSpeed, 0) * Time.deltaTime, Space.Self);
            }

            else
            {
                //Debug.Log("go Up");
                if (goDownO == false && cuchilloO.transform.localPosition.y >= 71)
                {
                    //Debug.Log("stop chopping");
                    estado = noChop;
                    cuchilloO.transform.localPosition = new Vector3(cuchilloO.transform.localPosition.x, startY, cuchilloO.transform.localPosition.z);
                }
                else
                {
                    cuchilloO.transform.Translate(new Vector3(knifeSpeed, knifeSpeed, 0) * Time.deltaTime);
                }
            }
        }

        //CUCHILLO K
        if (cuchilloK.transform.localPosition.y <= 25)
        {
            goDownK = false;
        }

        if (estado == chopK)
        {
            //Debug.Log("chopping");
            if (goDownK == true)
            {
                cuchilloK.transform.Translate(new Vector3(-1 * knifeSpeed, -1 * knifeSpeed, 0) * Time.deltaTime, Space.Self);
            }

            else
            {
                //Debug.Log("go Up");
                if (goDownK == false && cuchilloK.transform.localPosition.y >= 71)
                {
                    //Debug.Log("stop chopping");
                    estado = noChop;
                    cuchilloK.transform.localPosition = new Vector3(cuchilloK.transform.localPosition.x, startY, cuchilloK.transform.localPosition.z);
                }
                else
                {
                    cuchilloK.transform.Translate(new Vector3(knifeSpeed, knifeSpeed, 0) * Time.deltaTime);
                }
            }
        }

        //CUCHILLO M
        if (cuchilloM.transform.localPosition.y <= 25)
        {
            goDownM = false;
        }

        if (estado == chopM)
        {
            //Debug.Log("chopping");
            if (goDownM == true)
            {
                cuchilloM.transform.Translate(new Vector3(-1 * knifeSpeed, -1 * knifeSpeed, 0) * Time.deltaTime, Space.Self);
            }

            else
            {
                //Debug.Log("go Up");
                if (goDownM == false && cuchilloM.transform.localPosition.y >= 71)
                {
                    //Debug.Log("stop chopping");
                    estado = noChop;
                    cuchilloM.transform.localPosition = new Vector3(cuchilloM.transform.localPosition.x, startY, cuchilloM.transform.localPosition.z);
                }
                else
                {
                    cuchilloM.transform.Translate(new Vector3(knifeSpeed, knifeSpeed, 0) * Time.deltaTime);
                }
            }
        }


    }

    private void Spawn(int color)
    {
        //rojo
        if (color == 0)
        {
            GameObject miObjeto = Instantiate(prefabNota, posicionRojo, prefabNota.transform.rotation);
            miObjeto.GetComponent<Renderer>().material.color = Color.red;
        }

        //verde
        if (color == 1)
        {
            GameObject miObjeto2 = Instantiate(prefabNota, posicionVerde, prefabNota.transform.rotation);
            miObjeto2.GetComponent<Renderer>().material.color = Color.green;

        }

        //azul
        if (color == 2)
        {
            GameObject miObjeto3 = Instantiate(prefabNota, posicionAzul, prefabNota.transform.rotation);
            miObjeto3.GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    private void EndJuego1()
    {
        picarDetector.SetActive(false);
        guitarra.SetActive(false);
        cheffy.master.EndPicar(exito);
        anim.SetBool("cooking", false);
    }
}
