using TMPro;
using UnityEngine;

public class GHboton : MonoBehaviour
{
    public float speed = -1.5f;
    public int numWinsNeeded = 10;

    private readonly double zMax = -1.65;
    private readonly double zMin = -1.79;
    private readonly double zSpace = -.9;

    float miY;
    float miZ;
    bool failHit;
    int miInt;

    public TextMeshProUGUI counter;
    public JuegaPicar picarScript;

    private AudioSource source;
    public AudioClip right;
    public AudioClip wrong;

    // Start is called before the first frame update
    void Start()
    {
        counter = GameObject.FindGameObjectWithTag("Counter").GetComponent<TextMeshProUGUI>();
        source = GameObject.Find("MainCamera").GetComponent<AudioSource>();
        picarScript = GameObject.Find("PicarMJ").GetComponent<JuegaPicar>();
        source.enabled = true;
        failHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (counter.text == "0" || counter.text == " 0 ")
        {
            Destroy(gameObject);
        }

        miY = transform.position.y;
        miZ = transform.position.z;

        transform.Translate(new Vector3(0, 0, speed) * Time.deltaTime);

        if (picarScript.estado == picarScript.noChop)
        {
            //if user presses O
            if (Input.GetKeyDown("o") && (miY > 1.50) && (miZ <= zSpace))
            {
                if (miZ <= zMax && miZ >= zMin && failHit == false)
                {
                    picarScript.estado = picarScript.chopO;
                    picarScript.goDownO = true;
                    Debug.Log("chop O");
                    Debug.Log("estado is " + picarScript.estado);

                    source.PlayOneShot(right);
                    Destroy(gameObject);
                    CambiaCounter();
                }
                else
                {
                    GetComponent<Renderer>().material.color = Color.black;
                    failHit = true;
                    ReempezarCounter();
                    source.PlayOneShot(wrong);
                }
            }

            //if user presses k
            if (Input.GetKeyDown("k") && (miY < 1.50) && (miY > 1.35) && (miZ <= zSpace))
            {
                if (miZ <= zMax && miZ >= zMin && failHit == false)
                {
                    //mueve cuchillo K

                    picarScript.estado = picarScript.chopK;
                    picarScript.goDownK = true;
                    source.PlayOneShot(right);
                    Destroy(gameObject);
                    CambiaCounter();
                }
                else
                {
                    GetComponent<Renderer>().material.color = Color.black;
                    failHit = true;
                    ReempezarCounter();
                    source.PlayOneShot(wrong);
                }
            }

            //if user presses m
            if (Input.GetKeyDown("m") && (miY < 1.3) && (miZ <= zSpace))
            {
                if (miZ <= zMax && miZ >= zMin && failHit == false)
                {

                    //mueve cuchillo M
                    picarScript.estado = picarScript.chopM;
                    picarScript.goDownM = true;
                    source.PlayOneShot(right);
                    Destroy(gameObject);
                    CambiaCounter();


                }
                else
                {
                    GetComponent<Renderer>().material.color = Color.black;
                    failHit = true;
                    ReempezarCounter();
                    source.PlayOneShot(wrong);
                }
            }
        }

        //if I go rly far, destroy me
        if (miZ <= -6)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other is null)
            throw new System.ArgumentNullException(nameof(other));

        //Debug.Log("hit");
        ReempezarCounter();
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void CambiaCounter()
    {
        //SONIDO DE "CHOP" DE UN CUCHILLO
        miInt = int.Parse(counter.GetParsedText());
        counter.text = (miInt - 1).ToString();
    }

    private void ReempezarCounter()
    {
        ///SONIDO DE FALLAR
        counter.text = numWinsNeeded.ToString();
    }
}