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

	[Header("Material")]
	[SerializeField] private Material redMaterial;

	private bool isGround;

	private int layerMask;

	private Vector3 targetDirection;

	private void Start()
	{
		if (!isLocalPlayer)
        {
			return;
        }

		if (GameManager.instance.isRed)
        {
			var rend = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();

			Material[] mats = rend.materials;

			mats[0] = redMaterial;

			rend.materials = mats;
		}

		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		layerMask = ~playerLayer.value;
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

		targetDirection = new Vector3(x, 0, z);

		if (!(x == 0 && z == 0))
        {
			rb.velocity = new Vector3(x * speed, rb.velocity.y, z * speed);
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * 10.0f);
		}
	}
}
