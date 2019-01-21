using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{

    [SerializeField] private float m_movementSpeed = 80.0f;
    [SerializeField] private float m_turningSpeed = 180.0f;
    [SerializeField] private int m_playerLayer = 9;

    private CharacterController m_controller = null;
    private Animator m_animator = null;

    private Vector3 m_velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        // Reference all components needed by the script
        m_controller = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();

        // Set all children transform to player layer
        foreach (Transform child in transform)
            child.gameObject.layer = m_playerLayer;
    }

    // FixedUpdate is called per time interval therefore time frame independent
    void FixedUpdate()
    {
        //// Reads user inputs
        //float groundVertical = Input
    }


        // Update is called once per frame
        void Update()
    {
        
    }
}
