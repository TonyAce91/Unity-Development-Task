using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "", menuName = "Projectiles")]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float m_speed = 10f;
    public Vector3 direction = Vector3.zero;
    [SerializeField] private float m_lifetime = 2f;
    [SerializeField] private int m_damage = 10;
    [SerializeField] private Rigidbody m_body;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // FixedUpdate is called per time interval therefore frame independent
    void FixedUpdate()
    {
        // Adds force to the rigidbody which uses Unity's physics engine
        // direction needs to be normalised so that it doesn't go over the max speed
        m_body.AddForce(direction.normalized * m_speed);

        // Lifetime determine's the projectile range
        m_lifetime -= Time.fixedDeltaTime;

        // Disables the projectile for object pooling purposes
        if (m_lifetime <= 0)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject objectHit = other.gameObject;

        if (objectHit.tag == "Damageable")
        { }
    }
}
