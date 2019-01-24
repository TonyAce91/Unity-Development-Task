using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

/// <summary>
/// This is the class that handles the input from virtual joystick used in mobile games
/// 
/// Code written by Antoine Kenneth Odi in 2019
/// Adapted from scripts in the Joystick Pack from Unity Asset Store to suit the current project
/// Adaptation is done for project's efficiency
/// </summary>

public class JoystickInput : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    [Header ("Sprite references")]
    [SerializeField] private RectTransform m_background = null;
    [SerializeField] private RectTransform m_handle = null;

    private Camera m_camera = new Camera();
    private Vector2 m_screenPos = Vector2.zero;
    private float m_outerRadius = 0f;

    #region Properties
    public Vector2 InputVector { get; private set; }
    public bool Engaged { get; private set; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Transforms the joystick's world space position to screen space position
        m_screenPos = RectTransformUtility.WorldToScreenPoint(m_camera, transform.position);

        // Creates a radius variable for the circle joystick
        // This is only accurate the width and length are equal
        // and the circle touches the edge of the square made by its anchor points
        m_outerRadius = m_background.sizeDelta.x / 2f;

        VariableCheck();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Ratio of the direction in reference to the outer circle
        Vector2 directionRatio = (eventData.position - m_screenPos) / m_outerRadius;

        // Checks if pointer has been drag out of the joystick's limit therefore clamp it when needed
        // SqrMagnitude is chosen for efficiency compare to magnitude 
        InputVector = (directionRatio.sqrMagnitude > 1) ? directionRatio.normalized : directionRatio;

        // limits the joystick's centre point inside the outer ring
        m_handle.anchoredPosition = InputVector * m_outerRadius * 0.5f;
    }

    // This is called when the pointer touches this script's gameObject
    public void OnPointerDown(PointerEventData eventData)
    {
        // Used for joystick engagement detection
        Engaged = true;
        
        // Initialises the position for the current touch
        OnDrag(eventData);
    }

    // This is called when the pointer that triggered OnPointerDown is released
    public void OnPointerUp(PointerEventData eventData)
    {
        // Used for joystick disengagement detection
        Engaged = false;

        // Resets Input Vector and joystick position
        InputVector = Vector2.zero;
        m_handle.anchoredPosition = Vector2.zero;
    }

    // This is used to check for any null reference that could break the game
    // This serves as a warning system for the developer. (This is not in the Joystick pack scripts)
    private void VariableCheck()
    {
#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying)
        {
            bool errorOccurred = false;

            // Checks if any of the sprite needed for this script are not set
            if (m_background == null)
            {
                EditorUtility.DisplayDialog("Error", "One of the joystick's background is not set", "Exit");
                errorOccurred = true;
            }
            if (m_handle == null)
            {
                EditorUtility.DisplayDialog("Error", "One of the joystick's handle is not set", "Exit");
                errorOccurred = true;
            }

            // Turns off the application if any error occurs
            if (errorOccurred)
                EditorApplication.isPlaying = false;
        }
#endif
    }
}
