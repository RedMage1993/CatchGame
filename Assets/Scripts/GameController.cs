/**
 * TODO: 
 * Come up with better way to time or count balls/chances left. I think it could be better. Right now, there's
 * not really a sure way to say that every player of the game has an equal chance at getting a certain score.
 * 
 * We can create a public var for number of balls/chances. Use the spawn delay to calculate the max amount of time
 * required for it (later will have to take bombs into account). If the time is less than the timeLeft var, then
 * decide which to use over the other.
 */

using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public Camera cam;
    private Renderer rend;
    public GameObject ball;
    public float timeLeft;

    private Vector3 limits; // Hat's movement boundaries
    private bool started;
    private float initTime;

    // Use this for initialization
    void Start()
    {
        // Use on ball game object, otherwise will grab object attached to.
        rend = ball.GetComponent<Renderer>(); // Have to do this to get bounds property

        limits = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));
        limits.x -= rend.bounds.extents.x; // Subtract half of ball width (extents is half of box)

        started = false;
        initTime = 0.0f;

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
        // Give player moment to prepare
        yield return new WaitForSeconds(2.0f);

        initTime = timeLeft;
        started = true;

        while (timeLeft > 0)
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

        // This is just for consistency
        started = false;
        timeLeft = initTime;
    }
}
