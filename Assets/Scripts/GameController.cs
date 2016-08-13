using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public Camera cam;
    private Renderer rend;
    public GameObject ball;

    private Vector3 limits; // Hat's movement boundaries

    // Use this for initialization
    void Start()
    {
        // Use on ball game object, otherwise will grab object attached to.
        rend = ball.GetComponent<Renderer>(); // Have to do this to get bounds property

        limits = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));
        limits.x -= rend.bounds.extents.x; // Subtract half of ball width (extents is half of box)

        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        // Give player moment to prepare
        yield return new WaitForSeconds(2.0f);

        while (true)
        {
            Vector3 spawnPos = new Vector3(
                Random.Range(-limits.x, limits.x),
                transform.position.y,
                0.0f
            );

            // No rotation
            Quaternion spawnRot = Quaternion.identity;

            Instantiate(ball, spawnPos, spawnRot);

            yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        }
    }
}
