using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
    public Camera cam;
    private Renderer rend;
    public GameObject[] balls; // Now holding ball and bomb
    public float timeLeft;
    public Text timerText;
    public GameObject gameOverText;
    public GameObject restartButton;
    public GameObject splashScreen;
    public GameObject startButton;

    private Vector3 limits; // Hat's movement boundaries
    private bool started;
    private float initTime;
    public HatController hc;

    // Use this for initialization
    void Start()
    {
        // Use on ball game object, otherwise will grab object attached to.
        rend = balls[0].GetComponent<Renderer>(); // Have to do this to get bounds property

        limits = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));
        limits.x -= rend.bounds.extents.x; // Subtract half of ball width (extents is half of box)

        started = false;
        initTime = 0.0f;

		UpdateTimerText();
    }

    void FixedUpdate()
    {
        // Useful for those as it's smart and returns fixed delta time.
        // Better than using coroutine (inconsistent) or Update (too fast; many DEC's).
        if (started && timeLeft > 0) // Only count down when game is actually running
        {
            timeLeft -= Time.deltaTime;
            UpdateTimerText();
        }
    }

    public void StartGame()
    {
        splashScreen.SetActive(false);
        startButton.SetActive(false);

        hc.ToggleControl(true);
        StartCoroutine(Spawn());
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

            Instantiate(balls[Random.Range(0, balls.Length)], spawnPos, spawnRot);

            yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        }

        yield return new WaitForSeconds(2.0f);
        gameOverText.SetActive(true);
        hc.ToggleControl(false);
        yield return new WaitForSeconds(2.0f);
        restartButton.SetActive(true);

        // This is just for consistency
        started = false;
        timeLeft = initTime;
    }

    void UpdateTimerText()
    {
        timerText.text = "Time Left:\n" + Mathf.RoundToInt(timeLeft);
    }
}
