using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, InputSystem_Actions.IPlayerActions, InputSystem_Actions.IUIActions
{
    public static PlayerController Instance { get; private set; }
    public static InputSystem_Actions InputInstance { get; private set; }

    [SerializeField] private float movementSpeed = 5f; // Erhöht für bessere Bewegung
    [SerializeField] private float cameraSensitivity = 0.5f;
    [SerializeField] private float jumpForce = 5f; // Erhöht für besseren Sprung
    [SerializeField] private Camera mainCamera;

    [Header("Object Interaction")]
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private Transform holdPoint;
    [SerializeField] private LayerMask interactableLayer;

    // Diese Werte werden jetzt vom InteractivableObject selbst gesteuert,
    // aber wir behalten sie hier, um zu sehen, ob wir sie noch brauchen.
    // [SerializeField] private float holdSmoothSpeed = 10f; 
    // [SerializeField] private float maxHoldVelocity = 15f;
    // [SerializeField] private float holdForceMultiplier = 50f; 

    // Optionale Einstellung: Spieler wird schwerer, wenn er Objekt hält
    [SerializeField] private bool makePlayerHeavierWhenHolding = true;
    [SerializeField] private float playerMassWhenHolding = 1000f; // Eine viel höhere Masse, z.B. 100
    private float originalPlayerMass; // Um die ursprüngliche Masse zu speichern


    private bool isJumping = false;
    private Vector2 currentMovementInput;
    private Vector2 currentLookInput;
    private bool isPlayerFreeze;
    private Rigidbody body;

    private GameObject heldObject = null;
    private Rigidbody heldObjectRigidbody = null;
    private FixedJoint currentJoint = null;
    private InteractivableObject currentInteractivableObject = null; // NEU: Referenz auf das InteractivableObject Skript

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        InputInstance = new InputSystem_Actions();
        InputInstance.Player.SetCallbacks(this);
        InputInstance.UI.SetCallbacks(this);
        InputInstance.UI.Disable();
    }

    public void OnDestroy()
    {
        InputInstance?.Dispose();
        if (heldObject != null)
        {
            ReleaseObject(); // Sicherstellen, dass Objekt beim Zerstören losgelassen wird
        }
    }

    public void OnEnable()
    {
        InputInstance.Player.Enable();
    }

    void OnDisable()
    {
        InputInstance.Player.Disable();
        if (heldObject != null)
        {
            ReleaseObject(); // Sicherstellen, dass Objekt beim Deaktivieren losgelassen wird
        }
    }

    public void Start()
    {
        SetCursorState(false);
        mainCamera = GetComponentInChildren<Camera>();
        body = GetComponent<Rigidbody>();
        originalPlayerMass = body.mass; // Speichere die ursprüngliche Masse
    }

    public void Update()
    {
        if (mainCamera == null || isPlayerFreeze) return;

        float mouseX = currentLookInput.x * cameraSensitivity;
        float mouseY = currentLookInput.y * cameraSensitivity;

        float currentCameraPitch = mainCamera.transform.localEulerAngles.x;
        if (currentCameraPitch > 180) currentCameraPitch -= 360;

        float newCameraPitch = currentCameraPitch + -mouseY;

        newCameraPitch = Mathf.Clamp(newCameraPitch, -89f, 89f);

        mainCamera.transform.localEulerAngles = new Vector3(newCameraPitch, 0, 0);
        transform.Rotate(Vector3.up, mouseX);
    }

    public void FixedUpdate()
    {
        // Spielerbewegung
        Vector3 flatMovementDirection = transform.right * currentMovementInput.x + transform.forward * currentMovementInput.y;
        flatMovementDirection.y = 0f;

        if (flatMovementDirection.magnitude > 0.01f)
        {
            Vector3 targetVelocity = flatMovementDirection.normalized * movementSpeed;
            Vector3 velocityChange = targetVelocity - new Vector3(body.linearVelocity.x, 0, body.linearVelocity.z);
            velocityChange.y = 0;
            body.AddForce(velocityChange * body.mass, ForceMode.Force);
        }
        else
        {
            Vector3 currentHorizontalVelocity = new Vector3(body.linearVelocity.x, 0, body.linearVelocity.z);
            body.AddForce(-currentHorizontalVelocity * body.mass * 5f, ForceMode.Force);
        }

        // Bewegung des gehaltenen Objekts - jetzt delegiert an InteractivableObject
        if (currentInteractivableObject != null)
        {
            currentInteractivableObject.MoveTowardsHoldPoint(holdPoint.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isJumping && body.linearVelocity.y < 0.1f)
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                if (contact.point.y < transform.position.y - (GetComponent<Collider>().bounds.extents.y * 0.5f)) 
                {
                    float angle = Vector3.Angle(Vector3.up, contact.normal);
                    if (angle < 45f)
                    {
                        isJumping = false;
                        Debug.Log("Landed!");
                        return;
                    }
                }
            }
        }
    }

    // Diese Methode wird im Editor aufgerufen, wenn das GameObject ausgewählt ist.
private void OnDrawGizmosSelected()
{
    // Zeichne eine Kugel an der HoldPoint Position
    if (holdPoint != null)
    {
        Gizmos.color = Color.yellow; // Wähle eine Farbe für dein Gizmo
        Gizmos.DrawSphere(holdPoint.position, 0.1f); // Zeichne eine kleine Kugel (Radius 0.1m)

        // Optional: Zeichne eine Linie vom Kamera-Ursprung zum HoldPoint
        if (mainCamera != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(mainCamera.transform.position, holdPoint.position);
        }
    }

    // Optional: Zeichne den Raycast für die Interaktion
    if (mainCamera != null)
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * interactionDistance);
    }
}

    // ---------- Helping Methods --------------------------

    private void SetCursorState(bool showAndUnlock)
    {
        Cursor.visible = showAndUnlock;
        Cursor.lockState = showAndUnlock ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void ReleaseObject()
    {
        if (heldObject == null) return;
        
        if (currentJoint != null)
        {
            Destroy(currentJoint);
            currentJoint = null;
        }

        if (currentInteractivableObject != null) // NEU: Informiere das InteractivableObject
        {
            currentInteractivableObject.OnRelease();
        }

        if (makePlayerHeavierWhenHolding) // NEU: Setze die Spielermasse zurück
        {
            body.mass = originalPlayerMass;
        }

        heldObject = null;
        heldObjectRigidbody = null;
        currentInteractivableObject = null; // NEU: Referenz löschen
        Debug.Log("Released object.");
    }

    private void TryGrabObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, interactionDistance, interactableLayer))
        {
            Debug.Log("Raycast hit:" + hit.collider.name);
            // NEU: Versuche, das InteractivableObject Skript zu bekommen
            InteractivableObject grabbable = hit.collider.GetComponent<InteractivableObject>();

            if (grabbable != null && grabbable.GetComponent<Rigidbody>() != null)
            {
                heldObject = hit.collider.gameObject;
                heldObjectRigidbody = grabbable.GetComponent<Rigidbody>(); // Rigidbody vom InteractivableObject
                currentInteractivableObject = grabbable; // Speichere die Referenz

                if (makePlayerHeavierWhenHolding) // NEU: Erhöhe die Masse des Spielers
                {
                    body.mass = playerMassWhenHolding;
                }
                
                // Informiere das InteractivableObject, dass es gegrabbt wird
                currentInteractivableObject.OnGrab();

                currentJoint = heldObject.AddComponent<FixedJoint>();
                currentJoint.connectedBody = body;
                
                Debug.Log("Grabbed object: " + heldObject.name);
            }
        }
        else
        {
            Debug.Log("No interactable object found.");
        }
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
        if (context.started)
        {
            Debug.Log("Player Interact input received");

            if (heldObject == null)
            {
                TryGrabObject();
            }
            else
            {
                ReleaseObject();
            }
        }
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
        currentLookInput = context.ReadValue<Vector2>();
    }

    public void OnMiddleClick(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
    }

    //------------- UI Actions ---------------------------

    public void OnNavigate(InputAction.CallbackContext context) { throw new NotImplementedException(); }
    public void OnNext(InputAction.CallbackContext context) { throw new NotImplementedException(); }
    public void OnPoint(InputAction.CallbackContext context) { throw new NotImplementedException(); }
    public void OnPrevious(InputAction.CallbackContext context) { throw new NotImplementedException(); }
    public void OnRightClick(InputAction.CallbackContext context) { throw new NotImplementedException(); }
    public void OnScrollWheel(InputAction.CallbackContext context) { throw new NotImplementedException(); }
    public void OnSprint(InputAction.CallbackContext context) { throw new NotImplementedException(); }
    public void OnSubmit(InputAction.CallbackContext context) { throw new NotImplementedException(); }
    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context) { throw new NotImplementedException(); }
    public void OnTrackedDevicePosition(InputAction.CallbackContext context) { throw new NotImplementedException(); }
}