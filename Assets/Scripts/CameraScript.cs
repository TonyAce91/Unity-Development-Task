using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script controls the camera movement in game
/// 
/// Code written by Antoine Kenneth Odi in 2019
/// </summary>
public class CameraScript : MonoBehaviour
{
    private GameObject m_player;
    [SerializeField] private Vector3 m_relativePosition = Vector3.zero;
    [SerializeField] private float m_cameraSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        // Finds the game object with player script on it
        m_player = FindObjectOfType<Player>().gameObject;

        // Sets the current camera position as the relative position if relative position have not been set or too small 
        if (m_relativePosition.sqrMagnitude <= 0.1f)
            m_relativePosition = transform.position;

    }

    // LateUpdate is called once per frame after Update function is called
    void LateUpdate()
    {
        Vector3 targetPos = m_player.transform.position + m_relativePosition;

        // Move the camera position to the target position smoothly
        transform.position = targetPos;
    }
}
