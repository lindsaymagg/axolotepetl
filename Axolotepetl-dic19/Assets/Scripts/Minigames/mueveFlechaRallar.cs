using UnityEngine;

public class mueveFlechaRallar : MonoBehaviour
{
    Rigidbody flecha;
    Renderer meshy;
    public float forceSpeed = 800;
    public float gravity = -3;
    public Vector3 startPos = new Vector3(-0.68f, 0.9671009f, -1.049529f);

    public int estado = 0;
    public int jugandoRallador = 1;
    public int jugandoLicuadora = 2;

    float elapsedTime = 0;

    public GameObject lic0;
    public GameObject lic1;
    public GameObject ral0;
    public GameObject ral1;

    private float lastWait;

    private AudioSource source;
    public AudioClip licSon;
    public AudioClip rallarSon;

    [HideInInspector] private bool licuadoraActiva = false;
    [HideInInspector] private bool ralladorActiva = false;

    public float waitTime = .1f;
    // Start is called before the first frame update
    void Start()
    {
        source = GameObject.Find("MainCamera").GetComponent<AudioSource>();

    }

    private void OnEnable()
    {
        //Debug.Log("El estado es " + estado);
        flecha = GetComponent<Rigidbody>();
        meshy = GetComponent<Renderer>();
        flecha.position = startPos;
        meshy.enabled = true;
        licuadoraActiva = false;
        ralladorActiva = false;

        lic0.SetActive(false);
        lic1.SetActive(false);
        ral0.SetActive(false);
        ral1.SetActive(false);

        if (estado == jugandoRallador)
        {
            ral0.SetActive(true);
            ral1.SetActive(false);
            lastWait = Time.time;
        }

        if (estado == jugandoLicuadora)
        {
            lic0.SetActive(true);
            lic1.SetActive(false);
            lastWait = Time.time;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (licuadoraActiva == true)
        {

            if (elapsedTime < waitTime)
            {
                lic1.SetActive(true);
                elapsedTime += Time.deltaTime;
            }
            else
            {
                licuadoraActiva = false;
                lic1.SetActive(false);
            }
        }

        if (ralladorActiva == true)
        {

            if (elapsedTime < waitTime)
            {
                ral1.SetActive(true);
                ral0.SetActive(false);
                elapsedTime += Time.deltaTime;
            }
            else
            {
                ralladorActiva = false;
                ral0.SetActive(true);
                ral1.SetActive(false);
            }
        }

    }

    void FixedUpdate()
    {


        flecha.AddRelativeForce(new Vector3(gravity, 0, 0));

        if (Input.GetKeyDown("space"))
        {

            flecha.AddRelativeForce(forceSpeed * Time.deltaTime, 0, 0);


            if (estado == jugandoLicuadora)
            {
                licuadoraActiva = true;
                elapsedTime = 0f;
                source.PlayOneShot(licSon, .2f);

            }


            if (estado == jugandoRallador)
            {
                ralladorActiva = true;
                elapsedTime = 0f;
                source.PlayOneShot(rallarSon, .5f);

            }
        }

        if (estado == jugandoLicuadora && licuadoraActiva == false)
        {
            lic0.SetActive(true);
            lic1.SetActive(false);
        }

        if (estado == jugandoRallador && ralladorActiva == false)
        {
            ral0.SetActive(true);
            ral1.SetActive(false);
        }
    }

}