using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour, InputSystem_Actions.IPlayerActions, InputSystem_Actions.IUIActions
{
    public static PlayerController Instance { get; private set; }

    public static InputSystem_Actions InputInstance { get; private set; }

    [SerializeField] private float movementSpeed = 0.5f;
    [SerializeField] private float cameraSensitivity = 0.5f;
    [SerializeField] private float jumpForce = 0.5f;
    [SerializeField] private Camera mainCamera;

    private bool isJumping = false;
    private Vector2 currentMovementInput;
    private Vector2 currentLookInput;
    private bool isPlayerFreeze;

    private Rigidbody body; 

    public void Awake()
    {
        // Setup Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Set Actions
        InputInstance = new InputSystem_Actions();
        InputInstance.Player.SetCallbacks(this);
        InputInstance.UI.SetCallbacks(this);
        InputInstance.UI.Disable();
    }

    public void OnDestroy()
    {
        InputInstance?.Dispose();
    }

    public void OnEnable()
    {
        InputInstance.Player.Enable();
    }

    void OnDisable()
    {
        InputInstance.Player.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        SetCursorState(false);
        mainCamera = GetComponentInChildren<Camera>();
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (mainCamera == null || isPlayerFreeze) return;

        //Look around 
        float mouseX = currentLookInput.x * cameraSensitivity;
        float mouseY = currentLookInput.y * cameraSensitivity;

        float currentCameraPitch = mainCamera.transform.localEulerAngles.x;
        if (currentCameraPitch > 180) currentCameraPitch -= 360;

        float newCameraPitch = currentCameraPitch + -mouseY;

        newCameraPitch = Mathf.Clamp(newCameraPitch, -89f, 89f);

        mainCamera.transform.localEulerAngles = new Vector3(newCameraPitch, 0, 0);
        transform.Rotate(Vector3.up, mouseX);

        //Movement 
            Vector3 movementDirection = transform.right * currentMovementInput.x + transform.forward * currentMovementInput.y;
        movementDirection.y = 0f;

        body.MovePosition(transform.position + movementDirection * movementSpeed);
    }

    // ---------- Helping Methods --------------------------

    private void SetCursorState(bool showAndUnlock)
    {
        Cursor.visible = showAndUnlock;
        Cursor.lockState = showAndUnlock ? CursorLockMode.None : CursorLockMode.Locked;
    }

    //----------- Player Actions ---------------------------

    public void OnAttack(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started) Debug.Log("Player Interact input received");
        throw new NotImplementedException();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started) Debug.Log("Player Jump input received");
        if (context.performed && !isJumping)
        {
            isJumping = true;
            body.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.started) Debug.Log("Player Look input received");
        currentLookInput = context.ReadValue<Vector2>();
    }

    public void OnMiddleClick(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started) Debug.Log("Player Move input received");
        currentMovementInput = context.ReadValue<Vector2>();
    }

    //------------- UI Actions ---------------------------

    public void OnNavigate(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }
}
