using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    public float damage = 50f;
    public float lifetime = 5f;
    public GameObject explosionEffect;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Animal"))
        {
            collision.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            Debug.Log("🔥 Hit target: " + collision.gameObject.name);
        }

        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
