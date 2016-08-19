/**
 * Thinking of making an array with intervals that will be used with WaitForSeconds.
 * It's a sort of random without being random. All of those intervals just need to add up to timeLeft.
 *
 * Let's also flip the hat upside down and then use the spacebar to jump up. The hat needs to collide
 * with the bowling ball at the top of the jump. AKA The timing just needs to be good.
 */

using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public Camera cam;
    private Renderer rend;
    public GameObject ball;
    public float timeLeft;
    public int spawnsLeft;

    private Vector3 limits; // Hat's movement boundaries
    private bool started;
    private float initTime;
    private int initSpawns;

    // Use this for initialization
    void Start()
    {
        // Use on ball game object, otherwise will grab object attached to.
        rend = ball.GetComponent<Renderer>(); // Have to do this to get bounds property

        limits = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));
        limits.x -= rend.bounds.extents.x; // Subtract half of ball width (extents is half of box)

        started = false;
        initTime = 0.0f;
        initSpawns = 0;

        StartCoroutine(Spawn());
    }

    void FixedUpdate()
    {
        // Useful for those as it's smart and returns fixed delta time.
        // Better than using coroutine (inconsistent) or Update (too fast; many DEC's).
        if (started && timeLeft > 0) // Only count down when game is actually running
        {
            timeLeft -= Time.deltaTime;
        }
    }

    IEnumerator Spawn()
    {
        initTime = timeLeft;
        initSpawns = spawnsLeft;
        started = true;

        while (spawnsLeft > 0)
        {
            yield return new WaitForSeconds(Random.Range(1.0f, (timeLeft / spawnsLeft) + 0.1f));

            Vector3 spawnPos = new Vector3(
                Random.Range(-limits.x, limits.x),
                transform.position.y,
                0.0f
            );

            // No rotation
            Quaternion spawnRot = Quaternion.identity;

            Instantiate(ball, spawnPos, spawnRot);

            spawnsLeft--;
        }

        // This is just for consistency
        while (timeLeft > 0)
            yield return new WaitForSeconds(timeLeft);

        started = false;
        timeLeft = initTime;
        spawnsLeft = initSpawns;
    }
}
