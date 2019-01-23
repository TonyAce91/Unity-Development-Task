using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_levelClutterPrefabs = new List<GameObject>();
    [SerializeField] private GameObject m_ground = null;
    [SerializeField] private int numberOfClutter = 10;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
