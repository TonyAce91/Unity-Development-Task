using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "", menuName = "Projectiles")]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float m_speed = 10f;
    [SerializeField] private float m_lifetime = 2f;
    [SerializeField] private int m_damage = 10;
    [SerializeField] private Rigidbody m_body;
    [HideInInspector] public ProjectileSpawner spawnerReference = null;

    private float m_currentHealth = 0;

    // FixedUpdate is called per time interval therefore frame independent
    void FixedUpdate()
    {
        // Lifetime determine's the projectile range
        m_currentHealth -= Time.fixedDeltaTime;

        // Disables the projectile for object pooling purposes
        if (m_currentHealth <= 0)
        {
            gameObject.SetActive(false);
            spawnerReference.numberOfSpawns--;

            // Resets the projectile's lifetime and velocity
            m_currentHealth = m_lifetime;
            m_body.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject objectHit = other.gameObject;

        if (objectHit.tag == "Damageable")
        { }
    }

    public void Initialise(Vector3 direction, float speed)
    {
        // Adds force to the rigidbody which uses Unity's physics engine
        // direction needs to be normalised so that it doesn't go over the max speed
        m_body.AddForce(direction.normalized * speed);
    }
}
