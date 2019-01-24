using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{

    public GameObject projectilePrefab = null;
    private List<GameObject> listOfProjectiles = new List<GameObject>();
    [SerializeField] private int m_maximumNumberOfSpawns = 20;
    public int numberOfSpawns = 0;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < m_maximumNumberOfSpawns; i++)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectileInstance.GetComponent<Projectile>().spawnerReference = this;
            projectileInstance.SetActive(false);
            listOfProjectiles.Add(projectileInstance);
        }
    }

    public void FireProjectile(float speed)
    {
        if (numberOfSpawns > m_maximumNumberOfSpawns)
            return;

        foreach (GameObject projectile in listOfProjectiles)
        {
            if (!projectile.activeSelf)
            {
                projectile.SetActive(true);
                projectile.transform.position = transform.position;
                projectile.GetComponent<Projectile>().Initialise(transform.right, speed);
                numberOfSpawns++;
                break;
            }
        }
    }
}
