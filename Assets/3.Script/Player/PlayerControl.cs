using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerControl : NetworkBehaviour
{
	private Animator animator;
	private Rigidbody rb;

	[Header("Movement")]
	public float speed = 5.0f;
	public float rotationSpeed = 10.0f;
	public float jumpPower = 5.0f;

	[Header("IsGround")]
	public LayerMask playerLayer;
	private bool isGround;

	[Header("Material")]
	[SerializeField] private Material redMaterial;

	[Header("Camera")]
	[SerializeField] private Transform CameraTransform;

	[Header("Color")]
	[SyncVar(hook = nameof(OnColorChanged))]
	public bool isRed = false;

	private int layerMask;

	//private Vector3 rotateDirection;

	private Vector3 moveDirection;
	private float x;
	private float z;

	private void OnColorChanged(bool _old, bool _new)
	{
		isRed = _new;

		PlayerControl[] pcs = FindObjectsOfType<PlayerControl>();

		foreach (PlayerControl pc in pcs)
		{
			if (pc.isRed)
			{
				var rend = pc.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
				Material[] mats = rend.materials;
				mats[0] = redMaterial;
				rend.materials = mats;
			}
		}
	}

	public void ChangeColor(bool red)
	{
		ChangeColor_Command(red);
	}

	[Command]
	public void ChangeColor_Command(bool red)
	{
		isRed = red;
	}

	private void Start()
	{
		if (isLocalPlayer)
        {
			animator = GetComponent<Animator>();
			rb = GetComponent<Rigidbody>();
			layerMask = ~playerLayer.value;
			CameraTransform = Camera.main.transform;
		}

		// RoomPlayerClient 오브젝트 비활성화
		RoomPlayerControl[] rpcs = FindObjectsOfType<RoomPlayerControl>();
		foreach (RoomPlayerControl rpc in rpcs)
		{
			rpc.gameObject.SetActive(false);
		}

		// 클라이언트가 생성되어 Start가 호출될 때 마다 isRed값 바꾸고 OnColorChanged에서 머티리얼도 변경
		ChangeColor(GameManager.instance.isRed);
	}

    private void Update()
    {
		if (!isLocalPlayer)
		{
			return;
		}

		MyInput();

		if (Input.GetKeyDown(KeyCode.Space) && isGround)
		{
			animator.SetTrigger("Jump");
			rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
		}

		isGround = Physics.OverlapSphere(transform.position, 0.05f, layerMask).Length > 0;
	}

    private void FixedUpdate()
	{
		if (!isLocalPlayer)
		{
			return;
		}

		Move();
		Rotate();
		float movement = Mathf.Abs(x) + Mathf.Abs(z);
		animator.SetFloat("Speed", movement);
	}
	private void MyInput()
	{
		z = Input.GetAxis("Vertical");
		x = Input.GetAxis("Horizontal");
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
		if (focus)
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
		else
		{
			Cursor.lockState = CursorLockMode.None;
		}
	}
}
