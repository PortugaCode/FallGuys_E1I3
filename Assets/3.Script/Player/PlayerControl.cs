using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

	// [UI]
	private GameObject canvas;
	public int currentRank = 0;
	public string userName = string.Empty;

	[Header("Command")]
	public bool isDev = false;
	public bool isGoal = false;

	[Header("Index")]
	[SyncVar(hook = nameof(OnIndexChanged))]
	public int myIndex = -1;

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

	private void OnIndexChanged(int _old, int _new)
    {
		myIndex = _new;

		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

		foreach(GameObject player in players)
        {
			player.transform.GetChild(4).GetComponent<TextMeshPro>().text = GameManager.instance.userNames[player.GetComponent<PlayerControl>().myIndex];
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

	public void ChangeIndex(int index)
    {
		ChangeIndex_Command(index);
    }

	[Command]
	public void ChangeIndex_Command(int index)
    {
		myIndex = index;
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

		// 클라이언트가 들어올 때 이름 업데이트하라고 보냄
		ChangeIndex(GameManager.instance.localIndex);

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
		else if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0.0f)
		{
			rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.45f, rb.velocity.z);
		}

		if (!isDev)
        {
			isGround = Physics.OverlapSphere(transform.position, 0.2f, layerMask).Length > 0;
		}
		else
        {
			isGround = true;
        }
		
		animator.SetBool("Ground", isGround);
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

	[Client]
	public void Goal(int index)
    {
		Goal_Command(index, isRed);
	}

	[Command]
	private void Goal_Command(int index, bool isRed)
    {
		Goal_RPC(index, isRed);
    }

	[ClientRpc]
	private void Goal_RPC(int index, bool isRed)
    {
		if (canvas == null)
		{
			canvas = GameObject.FindGameObjectWithTag("Canvas");
		}

		GameObject ui = canvas.transform.GetChild(currentRank).gameObject;
		ui.SetActive(true);
		ui.GetComponent<Text>().text = $"{currentRank + 1}등 - {GameManager.instance.userNames[index]}";

		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

		if (isRed)
        {
			GameManager.instance.redScore += (10 - currentRank);
        }
		else
        {
			GameManager.instance.blueScore += (10 - currentRank);
		}

		int playerCount = 0;

		foreach(GameObject player in players)
        {
			player.GetComponent<PlayerControl>().currentRank += 1;
			playerCount += 1;
		}

		if (currentRank == playerCount)
        {
			GameObject end = canvas.transform.GetChild(9).gameObject;
			end.SetActive(true);
			end.GetComponent<Text>().text = $"<게임종료>\n레드팀 : {GameManager.instance.redScore}점\n블루팀 : {GameManager.instance.blueScore}점";
		}
	}
}
