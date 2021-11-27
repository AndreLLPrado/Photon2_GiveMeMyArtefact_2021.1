using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCollisions : MonoBehaviourPunCallbacks
{
    PhotonView view;
    string pTag;
    string oTag;
    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if(view.IsMine)
            pTag = gameObject.tag;
    }

    [PunRPC]
    void RPC_VerifyTag(string otherTag)
    {
        if(pTag == otherTag)
        {
            Debug.Log("Players has same tag");
        }

        if(pTag != otherTag)
        {
            //Player wins
            if(pTag == "Wizzard" && otherTag == "Cat")
            {
                Debug.Log("you're Wizzard");
            }

            //Player loses
            else if(pTag == "Cat" && otherTag == "Wizzard")
            {
                Debug.Log("You're Cat");
            }

            //Invalid collision
            else {
                Debug.Log("Invalid Collision, you're " + pTag);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collide with " + collision.gameObject.name.ToString());
        //oTag = collision.gameObject.tag;
        //view.RPC("RPC_VerifyTag", RpcTarget.All, oTag);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (view.IsMine)
        {
            if (hit.gameObject.tag == "Wizzard" || hit.gameObject.tag == "Cat" || hit.gameObject.tag == "Player")
            {
                oTag = hit.gameObject.tag;
                Debug.Log("Collide with " + hit.gameObject.tag.ToString());
                view.RPC("RPC_VerifyTag", RpcTarget.All, oTag);
            }

        }
    }
}
