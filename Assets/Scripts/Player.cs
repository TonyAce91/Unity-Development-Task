﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

/// <summary>
/// This is the class that controls the character of the player
/// 
/// Code written by Antoine Kenneth Odi in 2019
/// </summary>

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private float m_gravity = -9.8f;
    [SerializeField] private float m_movementSpeed = 80.0f;
    [SerializeField] private float m_turningSpeed = 180.0f;
    [SerializeField] private float m_animationSpeed = 80.0f;
    [SerializeField] private int m_playerLayer = 9;
    [SerializeField] private string m_playerTag = "Player";

    private CharacterController m_controller = null;
    private Animator m_animator = null;

    private Vector3 m_velocity = Vector3.zero;
    private Vector2 m_forwardVector = Vector2.zero;

    [Header("Joystick references for mobile build")]
    [SerializeField] private JoystickInput m_leftJoystick = null;
    [SerializeField] private JoystickInput m_rightJoystick = null;

    [Header("Events")]
    public UnityEvent shootEvent;

    // This functions gets called the moment the script wakes up
    private void Awake()
    {
        // Makes sure that the player doesn't have Rigidbody
        Destroy(GetComponent<Rigidbody>());
        // Makes sure it has character controller and animator
        if (!GetComponent<CharacterController>())
            gameObject.AddComponent<CharacterController>();
        if (!GetComponent<Animator>())
            gameObject.AddComponent<Animator>();

        // Automatic tagging and layering
        gameObject.tag = m_playerTag;
        gameObject.layer = m_playerLayer;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Reference all components needed by the script
        m_controller = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
        m_animator.applyRootMotion = false;

        // Set all children transform to player layer
        foreach (Transform child in transform)
            child.gameObject.layer = m_playerLayer;

        transform.forward = new Vector3(0, 0, 1);

        // Checks if all variables required have been set
        VariableCheck();
    }

    // FixedUpdate is called per time interval therefore frame independent
    void FixedUpdate()
    {
        GroundMovement();
    }


    // Update is called once per frame
    void Update()
    {
        ApplyAnimation();
    }

    // This function ground movement calculations and moves both the character controller and transform accordingly
    private void GroundMovement()
    {
        m_velocity.z = 0;

        // Determines which direction to face depending on whether the right joystick has been engaged or not
        m_forwardVector = (m_rightJoystick.Engaged) ? m_rightJoystick.InputVector : m_leftJoystick.InputVector;

        // Rotates the game object when either of the joystick has been engaged
        if (m_rightJoystick.Engaged || m_leftJoystick.Engaged)
        {
            // Determines the angle of the input from a reference
            float angle = Vector2.SignedAngle(Vector2.up, m_forwardVector);

            // Translate the rotation from euler angles to quaternion
            Quaternion targetRotation = Quaternion.Euler(0, -angle, 0);

            // Rotate the game object from current to target rotation using turning speed
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_turningSpeed * Time.deltaTime);
        }

        // Moves the character according to left joystick input
        m_controller.Move(new Vector3(m_leftJoystick.InputVector.x, m_gravity, m_leftJoystick.InputVector.y) * m_movementSpeed * Time.fixedDeltaTime);
    }

    private void ApplyAnimation()
    {
        m_animator.SetFloat("VelX", Vector2.Dot(m_leftJoystick.InputVector, m_forwardVector) * m_animationSpeed * Time.deltaTime);
        if (m_rightJoystick.Engaged)
        {
            m_animator.SetBool("Fire", true);
            Vector2 rightGroundVector = new Vector2(transform.right.x, transform.right.z);
            //Vector2 rightVector = forwardMagnitude * m_forwardVector
            m_animator.SetFloat("VelY", -Vector2.Dot(m_leftJoystick.InputVector, rightGroundVector) * m_animationSpeed * Time.deltaTime);
        }
        else
        {
            m_animator.SetBool("Fire", false);
        }
    }

    // Fire animation calls this function to invoke events a shot has been made
    // This is more useful for timing slow animation with projectile spawning
    public void Shoot()
    {
        shootEvent.Invoke();
    }

    // This is used to check for any null reference that could break the game
    private void VariableCheck()
    {
#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying)
        {
            bool errorOccurred = false;

            // Checks if any of the joystick needed for this script are not set
            if (m_leftJoystick == null)
            {
                EditorUtility.DisplayDialog("Error", "Left Joystick has not been set", "Exit");
                errorOccurred = true;
            }
            if (m_rightJoystick == null)
            {
                EditorUtility.DisplayDialog("Error", "Right Joystick has not been set", "Exit");
                errorOccurred = true;
            }

            // Turns off the application if any error occurs
            if (errorOccurred)
                EditorApplication.isPlaying = false;
        }
#endif
    }

}
