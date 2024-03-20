using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour
{

    [SerializeField] private CharacterController charController;
    private float speed = 9.0f;
    private float gravity = -9.8f;
    private float pushForce = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizInput, 0, vertInput);
        // Clamp magnitude to limit diagonal movement
        movement = Vector3.ClampMagnitude(movement, 1.0f);
        // take speed into account
        movement *= speed;
        // apply gravity 
        movement.y = gravity;
        // make movement processor independent
        movement *= Time.deltaTime;
        // Convert local to global coordinates
        movement = transform.TransformDirection(movement);
        charController.Move(movement);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        // does it have a rigidbody and is Physics enabled?
        if (body != null && !body.isKinematic)
        {
            body.velocity = hit.moveDirection * pushForce;
        }
    }
}
