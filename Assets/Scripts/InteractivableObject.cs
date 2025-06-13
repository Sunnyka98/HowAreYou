using UnityEngine;

[RequireComponent(typeof(Rigidbody))] // Stellt sicher, dass ein Rigidbody vorhanden ist
public class InteractivableObject : MonoBehaviour
{
    [Header("Grabbing Properties")]
    [SerializeField] private float holdSmoothSpeed = 25f; // Wie schnell das Objekt zum HoldPoint zieht
    [SerializeField] private float maxHoldVelocity = 30f; // Maximale Geschwindigkeit des gezogenen Objekts
    [SerializeField] private float holdForceMultiplier = 300f; // Stärke der Anziehungskraft

    [Header("Damping when Held")]
    [SerializeField] private float heldLinearDamping = 5f; // Dämpfung, wenn gehalten
    [SerializeField] private float heldAngularDamping = 5f; // Winkel-Dämpfung, wenn gehalten

    private Rigidbody rb;
    private float originalLinearDamping;
    private float originalAngularDamping;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("GrabbableObject requires a Rigidbody component on " + gameObject.name);
            enabled = false; // Deaktiviere das Skript, wenn kein Rigidbody vorhanden ist
        }

        // Stelle sicher, dass die Rotation des Objekts beim Halten nicht unnötig zittert
        // rb.freezeRotation = true; // Optional: Wenn Objekte nicht rotieren sollen, wenn sie gehalten werden
    }

    public void OnGrab()
    {
        // Speichere die ursprünglichen Dämpfungswerte
        originalLinearDamping = rb.linearDamping;
        originalAngularDamping = rb.angularDamping;

        // Setze die Dämpfungswerte für das Halten
        rb.linearDamping = heldLinearDamping;
        rb.angularDamping = heldAngularDamping;

        // Optional: Setze die Geschwindigkeit auf Null, um ein initiales "Zucken" zu vermeiden
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void OnRelease()
    {
        // Setze Geschwindigkeit und Rotationsgeschwindigkeit auf Null, um Wegschleudern zu verhindern
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Setze die Dämpfungswerte auf die ursprünglichen zurück
        rb.linearDamping = originalLinearDamping;
        rb.angularDamping = originalAngularDamping;
    }

    // Diese Methode wird vom PlayerController in FixedUpdate aufgerufen
    public void MoveTowardsHoldPoint(Vector3 targetHoldPosition)
    {
        Vector3 displacement = targetHoldPosition - rb.position;

        // Debug-Visualisierung des HoldPoint und des Objekts
        Debug.DrawLine(rb.position, targetHoldPosition, Color.green); // Grün für das Objekt

        // Optional: Wenn Objekt zu nah ist, sofort stoppen um Zittern zu vermeiden
        if (displacement.magnitude < 0.05f && rb.linearVelocity.magnitude < 0.1f) // Schwellenwerte anpassen
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        else
        {
            Vector3 currentVelocity = rb.linearVelocity;
            Vector3 desiredVelocity = displacement * holdSmoothSpeed;

            // Begrenze die Geschwindigkeit
            if (desiredVelocity.magnitude > maxHoldVelocity)
            {
                desiredVelocity = desiredVelocity.normalized * maxHoldVelocity;
            }

            // Wende Kraft an, um die gewünschte Geschwindigkeit zu erreichen
            Vector3 velocityChange = desiredVelocity - currentVelocity;
            rb.AddForce(velocityChange * rb.mass * holdForceMultiplier, ForceMode.Force);
        }
    }
}