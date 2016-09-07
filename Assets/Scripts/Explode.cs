using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {
    public GameObject explosion;
    public ParticleSystem[] effects;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hat")
        {
            Instantiate(explosion, transform.position, transform.rotation);

            // If we don't do this, all of the particle effects will disappear along with
            // collision.gameObject. We also want to stop the effect from emitting and destroy it after a second.
            foreach (var effect in effects)
            {
                effect.transform.parent = null;
                effect.Stop();
                Destroy(effect.gameObject, 1.0f);
            }

            Destroy(gameObject);
        }
    }
}
