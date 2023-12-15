using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RoomPlayerControl : NetworkRoomPlayer
{
	[Header("Color")]
	[SyncVar(hook = nameof(OnColorChanged))]
	public bool isRed = false;

    [Header("Material")]
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material blueMaterial;

    public override void OnStartLocalPlayer()
    {
        // set room position -> �� �� �ϸ� index�� 0���� �ٲ��� ���� ���¶� ���� �ð� �� ȣ��.
        StartCoroutine(WaitForIndexChange());
    }

	private void OnColorChanged(bool _old, bool _new)
	{
		isRed = _new;

        RoomPlayerControl[] rpcs = FindObjectsOfType<RoomPlayerControl>();

        foreach (RoomPlayerControl rpc in rpcs)
        {
            if (rpc.isRed)
            {
                var rend = rpc.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
                Material[] mats = rend.materials;
                mats[0] = redMaterial;
                rend.materials = mats;
            }
            else
            {
                var rend = rpc.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
                Material[] mats = rend.materials;
                mats[0] = blueMaterial;
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

    private IEnumerator WaitForIndexChange()
    {
        yield return new WaitForSeconds(0.025f);
        GameManager.instance.SetRoomPosition(gameObject, index);
    }
}
