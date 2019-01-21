using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
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

    // FixedUpdate is called per time interval therefore time frame independent
    void FixedUpdate()
    {
        Vector3 targetPos = m_player.transform.position + m_relativePosition;

        // Move the camera position to the target position smoothly
        transform.position = targetPos;
        //transform.position = Vector3.Lerp (transform.position, targetPos, m_cameraSpeed * Time.fixedDeltaTime);
    }
}
