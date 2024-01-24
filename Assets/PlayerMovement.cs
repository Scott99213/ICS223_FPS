using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private float speed = 5f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float horizInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(vertInput, 0, horizInput) * speed * Time.deltaTime * 100;

        rb.velocity = movement;
    }

    // Update is called once per frame
    void Update()
    {
        //float horizInput = Input.GetAxis("Horizontal");
        //float vertInput = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(horizInput, 0, vertInput) * speed * Time.deltaTime;

        //transform.Translate(movement);


    }
}
