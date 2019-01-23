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
    private Vector2 m_faceVector = Vector2.zero;

    [Header("Joystick references for mobile build")]
    [SerializeField] private JoystickInput m_leftJoystick = null;
    [SerializeField] private JoystickInput m_rightJoystick = null;

    // Start is called before the first frame update
    void Start()
    {
        // Reference all components needed by the script
        m_controller = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
        m_animator.applyRootMotion = true;

        // Set all children transform to player layer
        foreach (Transform child in transform)
            child.gameObject.layer = m_playerLayer;

        transform.forward = new Vector3(0, 0, 1);
    }

    // FixedUpdate is called per time interval therefore frame independent
    void FixedUpdate()
    {
        //// Reads user inputs
        //float groundVertical = Input
        //Touch touch = Input.GetTouch(0);
        //touch.phase = TouchPhase.Began;
        //if(touch.fingerId == 1)
        //{ }
        FaceDirection();
        //Debug.Log("Face Direction: " + transform.forward);
        ApplyAnimation(m_leftJoystick.InputVector);
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void FaceDirection()
    {
        m_velocity.z = 0;

        m_faceVector = (m_rightJoystick.Engaged) ? m_rightJoystick.InputVector : m_leftJoystick.InputVector;

        if (!m_rightJoystick.Engaged && !m_leftJoystick.Engaged)
            return;

        float angle = Vector2.SignedAngle(Vector2.up, m_faceVector);
        Quaternion rotation = Quaternion.Euler(0, -angle, 0);
        transform.eulerAngles = new Vector3(0, -angle, 0);

    }

    private void ApplyAnimation(Vector2 inputVector)
    {
        //transform.forward
        if (m_rightJoystick.Engaged)
            m_animator.SetTrigger("Fire");
        //if (m_leftJoystick.Engaged && m_rightJoystick.Engaged)
        //{
            m_animator.SetFloat("VelX", Vector2.Dot(m_leftJoystick.InputVector, m_faceVector) * m_movementSpeed * Time.fixedDeltaTime);
        //}
        //else
        //    m_animator.SetFloat("VelX", m_leftJoystick.InputVector.magnitude * m_movementSpeed * Time.fixedDeltaTime);
        //m_animator.SetFloat("VelX", inputVector.x /*Vector3.Dot(transform.forward, ) */* m_movementSpeed * Time.fixedDeltaTime);
        //m_animator.SetFloat("VelY", inputVector.y * m_movementSpeed * Time.fixedDeltaTime);
    }

    
}
