using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MagicCasting : MonoBehaviourPunCallbacks
{
    Vector3 aim;
    PhotonView view;
    [SerializeField] LayerMask hitableLayer;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (view.IsMine)
        {
            if(gameObject.tag == "Cat")
            {
                view.RPC("RPC_AimSystem", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void RPC_AimSystem()
    {
        aim = new Vector3(Input.mousePosition.x, 0f, Input.mousePosition.y);

        if(Physics.Raycast(transform.position, aim, out RaycastHit hit, 100f, hitableLayer))
        {
            if (Input.GetMouseButtonDown(0))
            {

            }
        }
    }
}
