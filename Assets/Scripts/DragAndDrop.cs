using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private InputAction mouseClick;
    [SerializeField] private float mouseDragPhysicsSpeed = 10f;
    [SerializeField] private float mouseDragSpeed = .1f;

    private Camera mainCamera;
    private Vector3 velocity = Vector3.zero; private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += MousePressed;
    }

    private void OnDisable()
    {

    }

    private void MousePressed(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                StartCoroutine(DragUpdate(hit.collider.gameObject));
            }
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        clickedObject.TryGetComponent<Rigidbody>(out var rb);
        while (mouseClick.ReadValue<float>() != 0)
        {
            float initialDistance = Vector3.Distance(clickedObject.transform.position, mainCamera.transform.position
            );
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (rb != null)
            {
                Vector3 direction = ray.GetPoint(initialDistance) - clickedObject.transform.position;
                rb.linearVelocity = direction * mouseDragPhysicsSpeed;
                yield return waitForFixedUpdate;
            }
            else
            {
                clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, ray.GetPoint(initialDistance), ref velocity, mouseDragSpeed);
                yield return null;
            }

        }
    }
}