using UnityEngine;
using System.Collections;

public class HatController : MonoBehaviour {

    public Camera cam;
    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        // Direct rigidbody2d usage deprecated
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is not called once per frame
    // Called once per physics timestep (can be set in settings)
	void FixedUpdate () {
        Vector3 rawPosition = cam.ScreenToWorldPoint(Input.mousePosition); // Of mouse
        Vector3 targetPosition = new Vector3(rawPosition.x, 0.0f, 0.0f); // Of hat
        rb2d.MovePosition(targetPosition);
	}
}
