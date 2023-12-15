using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerControl_CineTest : NetworkBehaviour
{
	private Animator animator;
	private Rigidbody rb;

	[Header("Movement")]
	public float speed = 5.0f;
	public float rotationSpeed = 10.0f;
	public float jumpPower = 5.0f;

	[Header("Camera")]
	[SerializeField] private Transform CameraTransform;

	[Header("IsGround")]
	public LayerMask playerLayer;

	[Header("Material")]
	[SerializeField] private Material redMaterial;

	private bool isGround;
	private int layerMask;


	private Vector3 moveDirection;
	private float x;
	private float z;

	private void Start()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		layerMask = ~playerLayer.value;
	}

    private void Update()
    {
		MyInput();

		if (Input.GetKeyDown(KeyCode.Space) && isGround)
		{
			animator.SetTrigger("Jump");
			rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
		}

		isGround = Physics.OverlapSphere(transform.position, 0.05f, layerMask).Length > 0;
	}

	private void MyInput()
    {
		z = Input.GetAxis("Vertical");
		x = Input.GetAxis("Horizontal");
	}

    private void FixedUpdate()
	{
		Move();
		Rotate();
		float movement = Mathf.Abs(x) + Mathf.Abs(z);
		animator.SetFloat("Speed", movement);
	}

	private void Move()
    {
        moveDirection = CameraTransform.forward * z;
        moveDirection += CameraTransform.right * x;
		moveDirection *= speed * Time.deltaTime;
		moveDirection.y = rb.velocity.y;

        if (!(x == 0 && z == 0))
        {
			//rb.MovePosition(rb.position + moveDirection);
			rb.velocity = moveDirection;
        }


        /*		float myX = Input.GetAxis("Horizontal");
                float myZ = Input.GetAxis("Vertical");

                Vector3 newDirection = CameraTransform.forward * myZ;
                newDirection += CameraTransform.right * myX;


                if (!(x == 0 && z == 0))
                {
                    rb.velocity = newDirection * speed * Time.deltaTime;
                }*/

    }


	private void Rotate()
	{
		if (!(x == 0 && z == 0))
		{
			Vector3 direction = new Vector3(moveDirection.x, 0, moveDirection.z);
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
		}
	}






    private void OnApplicationFocus(bool focus)
    {
        if(focus)
        {
			Cursor.lockState = CursorLockMode.Locked;
        }
		else
        {
			Cursor.lockState = CursorLockMode.None;
		}
    }

}
