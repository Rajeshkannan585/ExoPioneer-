using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 20f;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime); // bullet auto destroy
    }

    void OnCollisionEnter(Collision collision)
    {
        // If bullet hits enemy
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            Destroy(collision.gameObject); // simple: destroy enemy
            Debug.Log("Enemy hit! -" + damage + " HP");
        }

        Destroy(gameObject); // destroy bullet
    }
}
