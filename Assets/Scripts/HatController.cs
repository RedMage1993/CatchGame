using UnityEngine;
using System.Collections;

public class HatController : MonoBehaviour {
    public Camera cam;
    private Rigidbody2D rb2d;
    private Renderer rend;

    private Vector3 limits; // Hat's movement boundaries

    // Use this for initialization
    void Start ()
    {
        // Direct rigidbody2d usage deprecated
        rb2d = GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>(); // Have to do this to get bounds property

        limits = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));
        limits.x -= rend.bounds.extents.x; // Subtract half of hat width (extents is half of box)
    }

    // FixedUpdate is not called once per frame
    // Called once per physics timestep (can be set in settings)
    void FixedUpdate ()
    {
        // Issue where clamping doesn't working on Maximize on Play
        Vector3 worldPos = cam.ScreenToWorldPoint(Input.mousePosition); // Of mouse
        worldPos.x = Mathf.Clamp(worldPos.x, -limits.x, limits.x);

        Vector3 targetPos = new Vector3(worldPos.x, 0.0f, 0.0f); // Of hat

        rb2d.MovePosition(targetPos);
    }
}
