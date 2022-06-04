using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    private float sensitivity = 3f;
    private Vector3 velosity = Vector3.zero;
    private Rigidbody rb;
    private Vector3 rotation = Vector3.zero;
    private float CameraUpDownRotation = 0f;
    private float CurrentCameraUPDownRotation = 0f;
    [SerializeField]
    GameObject FPSCamera;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMovement = Input.GetAxisRaw("Horizontal");
        float zMovement = Input.GetAxisRaw("Vertical");


        Vector3 movementHorizotal = transform.right * xMovement;
        Vector3 movementVertical = transform.forward * zMovement;

        Vector3 finalMovement = ( movementHorizotal + movementVertical) * speed;

        Move(finalMovement);


        float yRotation = Input.GetAxis("Mouse X");
        Vector3 rotationVector = new Vector3(0, yRotation, 0)*sensitivity;

        Rotate(rotationVector);

        float cameraUpDownRotation = Input.GetAxis("Mouse Y") * sensitivity;

        RotateCamera(cameraUpDownRotation);
    }

    private void FixedUpdate()
    {
        if(velosity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velosity * Time.fixedDeltaTime);
        }

        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        if(FPSCamera != null)
        {
            CurrentCameraUPDownRotation -= CameraUpDownRotation;
            CurrentCameraUPDownRotation = Mathf.Clamp(CurrentCameraUPDownRotation, -85, 45);

            FPSCamera.transform.localEulerAngles = new Vector3(CurrentCameraUPDownRotation, 0, 0);
        }
    }
    private void Rotate(Vector3 rotationVector)
    {
        rotation = rotationVector;
    }

    private void Move(Vector3 movementVelosity)
    {
        velosity = movementVelosity;
    }

    private void RotateCamera(float cameraUpDownRotation)
    {
        CameraUpDownRotation = cameraUpDownRotation;
    }
}
