using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private bool m_isInCombat = false;
    private static InputManager Instance { get; set; }
    public static bool IsInCombat { get => Instance.m_isInCombat; set => Instance.m_isInCombat = value; }

    [SerializeField]
    KeyCode m_forwardKey = KeyCode.W, m_backKey = KeyCode.S, m_rightKey = KeyCode.D, m_leftKey = KeyCode.A;

    Action OnForwardPressed, OnBackPressed, OnRightPressed, OnLeftPressed;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            InputManager.AllowToTakeInput(false);
        }
        else
        {
            Debug.LogError($"Instance is already present");
            return;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(m_rightKey))
        {
            OnRightPressed();
        }

        if (Input.GetKeyDown(m_leftKey))
        {
            OnLeftPressed();
        }

        if (m_isInCombat)
        {
            return;
        }

        if(Input.GetKeyDown(m_forwardKey))
        {
            OnForwardPressed();
        }

        if(Input.GetKeyDown(m_backKey))
        {
            OnBackPressed();
        }
    }

    public static void SubscribeToOnKeyPressedEvent(Action a_forward, Action a_back, Action a_right, Action a_left)
    {
        Instance.OnForwardPressed += a_forward;
        Instance.OnBackPressed += a_back;
        Instance.OnRightPressed += a_right;
        Instance.OnLeftPressed += a_left;
    }

    public static void UnSubscribeFromOnKeyPressedEvent(Action a_forward, Action a_back, Action a_right, Action a_left)
    {
        Instance.OnForwardPressed -= a_forward;
        Instance.OnBackPressed -= a_back;
        Instance.OnRightPressed -= a_right;
        Instance.OnLeftPressed -= a_left;
    }

    public static void AllowToTakeInput(bool a_allowedToMove)
    {
        Instance.enabled = a_allowedToMove;
    }
}
