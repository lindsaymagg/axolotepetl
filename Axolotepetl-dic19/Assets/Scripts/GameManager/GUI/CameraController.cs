using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed;
    public bool rotating = false;

    public ChefMovement cheffy;

    private int value;
    private Quaternion targetRotation;

    void Start()
    {
        targetRotation = this.transform.rotation;
    }

    void Update()
    {
        if (GameMaster.LevelEnded || GameMaster.GameEnded)
        {
            this.enabled = false;
            return;
        }

        if (cheffy.master.GUI)
        {
            return;
        }

        if (rotating)
        {
            if (value > 0)
            {
                cheffy.MoveLeft();
            }
            else
            {
                cheffy.MoveRight();
            }
        }

        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (!rotating)
        {
            if (Input.GetKeyDown("a"))
            {
                cheffy.GetLeftTarget();
                targetRotation *= Quaternion.AngleAxis(-90, Vector3.up);
                cheffy.transform.LookAt(new Vector3(cheffy.target.position.x, this.transform.position.y, cheffy.target.position.z));

                value = 1;
                rotating = true;
            }

            if (Input.GetKeyDown("d"))
            {
                cheffy.GetRightTarget();
                targetRotation *= Quaternion.AngleAxis(90, Vector3.up);
                cheffy.transform.LookAt(new Vector3(cheffy.target.position.x, this.transform.position.y, cheffy.target.position.z));

                value = 0;
                rotating = true;
            }
        }
    }
}
