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
	public float jumpPower = 5.0f;

	[Header("IsGround")]
	public LayerMask playerLayer;
	private bool isGround;

	[Header("Material")]
	[SerializeField] private Material redMaterial;

	[Header("Color")]
	[SyncVar(hook = nameof(OnColorChanged))]
	public bool isRed = false;

	private int layerMask;

	private Vector3 rotateDirection;
	private Vector3 moveDirection;

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

		float z = Input.GetAxis("Vertical");
		float x = Input.GetAxis("Horizontal");

		float movement = Mathf.Abs(x) + Mathf.Abs(z);
		animator.SetFloat("Speed", movement);

		rotateDirection = new Vector3(x, 0, z);

		moveDirection = Camera.main.transform.forward * z * speed + Camera.main.transform.right * x * speed;

		if (!(x == 0 && z == 0))
        {
			rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(rotateDirection), Time.deltaTime * 10.0f);
		}
	}
}
