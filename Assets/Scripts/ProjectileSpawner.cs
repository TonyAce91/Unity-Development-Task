using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This is the class that spawns projectiles
/// 
/// Code written by Antoine Kenneth Odi in 2019
/// </summary>

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab = null;
    [SerializeField] private int m_maximumNumberOfSpawns = 20;
    private List<GameObject> listOfProjectiles = new List<GameObject>();
    [HideInInspector] public int numberOfSpawns = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Object pooling determines how many bullets will be instantiated
        for (int i = 0; i < m_maximumNumberOfSpawns; i++)
        {
            // Instantiate projectile, give it a reference to this script then deactivate
            GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectileInstance.GetComponent<Projectile>().spawnerReference = this;
            projectileInstance.SetActive(false);

            // Adds the new instance to the pool
            listOfProjectiles.Add(projectileInstance);
        }
    }

    // This is called whenever a fire animation has occurred
    public void FireProjectile(float speed)
    {
        // Stops spawning when there's no more objects in the pool
        if (numberOfSpawns > m_maximumNumberOfSpawns)
            return;

        // Checks the pool for a deactivated projectile then activate it
        foreach (GameObject projectile in listOfProjectiles)
        {
            if (!projectile.activeSelf)
            {
                projectile.SetActive(true);

                // Move the projectile to its current position then initialise its direction and speed
                projectile.transform.position = transform.position;
                projectile.GetComponent<Projectile>().Initialise(transform.right, speed);
                numberOfSpawns++;
                break;
            }
        }
    }
}
