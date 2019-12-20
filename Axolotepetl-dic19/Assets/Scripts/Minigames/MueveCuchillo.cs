using UnityEngine;

public class MueveCuchillo : MonoBehaviour
{

    float startPos = 71.67f;
    float lowPoint = 24.1f;
    public float speed = -1.5f;
    private bool goingDown = true;
    private int estado;
    private int moviendose = 0;
    private int ya = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        transform.position = new Vector3(transform.position.x, startPos, transform.position.z);
        goingDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (estado == moviendose)
        {
            if (goingDown == true)
            {
                if (transform.position.y >= lowPoint)
                {
                    transform.Translate(new Vector3(0, speed, 0) * Time.deltaTime);
                }
                else
                {
                    goingDown = false;
                }
            }

            if (goingDown == false)
            {
                if (transform.position.y < startPos)
                {
                    transform.Translate(new Vector3(0, speed, 0) * Time.deltaTime);
                }
                if (transform.position.y >= startPos)
                {
                    estado = ya;
                }
            }
        }

        if (estado == ya)
        {
            Debug.Log("done");
        }


    }
}
