using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This is the class that controls projectiles behaviour
/// 
/// Code written by Antoine Kenneth Odi in 2019
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float m_lifetime = 2f;
    [SerializeField] private int m_damage = 10;
    [SerializeField] private Rigidbody m_body;
    [HideInInspector] public ProjectileSpawner spawnerReference = null;
    [SerializeField] private int m_projectileLayer = 10;

    private float m_currentHealth = 0;


    // This functions gets called the moment the script wakes up
    private void Awake()
    {
        // Makes sure that projectile has Rigidbody
        if (!GetComponent<Rigidbody>())
            gameObject.AddComponent<Rigidbody>();

        // Automatic layering
        gameObject.layer = m_projectileLayer;
    }

    // Initialises the projectile when it gets activated
    public void Initialise(Vector3 direction, float speed)
    {
        // Adds force to the rigidbody which uses Unity's physics engine
        // direction needs to be normalised so that it doesn't go over the max speed
        m_body.AddForce(direction.normalized * speed);
    }

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

    // This function gets called whenever the projectile hits a collider
    private void OnTriggerEnter(Collider other)
    {
        GameObject objectHit = other.gameObject;

        if (objectHit.tag == "Damageable")
        { }
    }
}
