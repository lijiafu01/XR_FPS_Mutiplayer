using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script requires you to have setup your animator with 3 parameters, "InputMagnitude", "InputX", "InputZ"
//With a blend tree to control the inputmagnitude and allow blending between animations.
//Also you need to shoose Firepoint, targets > 1, Aim image from canvas and 2 target markers and camera.
[RequireComponent(typeof(CharacterController))]
public class HS_LowPolyController : MonoBehaviour
{
    public float velocity = 9;
    public float mouseSensitiviti = 800f;
    float xRotation = 0f;
    [Space]

    public float InputX;
    public float InputZ;
    public Vector3 desiredMoveDirection;
    public float desiredRotationSpeed = 0.1f;
    public float Speed;
    public float allowPlayerRotation = 0.1f;
    public Camera cam;
    public CharacterController controller;
    public bool isGrounded;

    private float verticalVel;
    private Vector3 moveVector;

    [Space]
    [Header("Effects")]
    public ParticleSystem MuzzleFlash;
    public LayerMask collidingLayer = ~0; //Target marker can only collide with scene layer

    [Space]
    [Header("Shooting")]
    public float fireRate = 0.1f;
    public GameObject weapon;
    private float fireCountdown = 0f;

    void Start()
    {
        controller = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        if (cam)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
            Cursor.visible = true;

        InputMagnitude();
        RotateCamera();

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out hit, Mathf.Infinity, collidingLayer, QueryTriggerInteraction.Collide))
        {
            var distanceToPoint = Vector3.Distance(hit.point, weapon.transform.position);
            if (distanceToPoint > 2)
               weapon.transform.LookAt(hit.point);
        }

        if (Input.GetMouseButton(0))
        {

            if (fireCountdown <= 0f)
            {
                MuzzleFlash.Play();
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.GetComponent("HS_ReceiveHit") != null)
                    {
                        hit.collider.gameObject.GetComponent<HS_ReceiveHit>().ReceiveHit(hit);
                    }
                }
                fireCountdown = 0;
                fireCountdown += fireRate;
            }
        }
        fireCountdown -= Time.deltaTime;

        //If you don't need the character grounded then get rid of this part.
        isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            verticalVel = 0;
        }
        else
        {
            verticalVel -= 1f * Time.deltaTime;
        }
        moveVector = new Vector3(0, verticalVel, 0);
        controller.Move(moveVector);
    }

    void InputMagnitude()
    {
        //Calculate Input Vectors
        InputX = Input.GetAxisRaw("Horizontal");
        InputZ = Input.GetAxisRaw("Vertical");

        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        //Movement vector
        desiredMoveDirection = forward * InputZ + right * InputX;

        //Character diagonal movement faster fix
        desiredMoveDirection.Normalize();
        controller.Move(desiredMoveDirection * Time.deltaTime * velocity);
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitiviti * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitiviti * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
