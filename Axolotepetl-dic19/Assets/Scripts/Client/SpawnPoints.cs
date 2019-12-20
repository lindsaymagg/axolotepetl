using UnityEngine;


/// <summary>
/// Crear spawn points.
/// Create spawn points.
/// </summary>
public class SpawnPoints : MonoBehaviour
{
    public Transform[] spawns;

    private float x;
    private float y;
    private float z;

    /// <summary>
    /// Crear cuatro spawn points en una posición elegida aleatoriamente (dentro de un rango).
    /// Create four spawn points at position randomly chosen (within a certain range).
    /// </summary>
    public void ChooseSpawnPoints()
    {
        spawns = new Transform[4];

        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;

        for (int i = 0; i < spawns.Length; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            if (transform.position.x == 0)
            {
                cube.transform.position = new Vector3(Random.Range(x - 3.5f, x - 2.5f), y, z);
                x += 2;
            }

            if (transform.position.z == 0)
            {
                cube.transform.position = new Vector3(x, y, Random.Range(z - 3.5f, z - 2.5f));
                z += 2;
            }

            cube.GetComponent<Renderer>().enabled = false;
            cube.transform.localScale = new Vector3(0.5f, 0.1f, 0.5f);
            cube.transform.rotation = transform.rotation;
            spawns[i] = cube.transform;
        }
    }

    /// <summary>
    /// Eliminar los spawn points.
    /// Remove spawn points.
    /// </summary>
    public void RemoveSpawnPoints()
    {
        for (int i = 0; i < spawns.Length; i++)
        {
            Destroy(spawns[i].gameObject);
        }
    }
}
