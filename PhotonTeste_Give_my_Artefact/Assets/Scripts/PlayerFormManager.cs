using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerFormManager : MonoBehaviourPunCallbacks
{
    float pSpeed, pJumpSpeed;
    PhotonView view;
    [SerializeField] private Material pColor;
    [SerializeField] private Renderer renderer;


    private void Start()
    {
        pSpeed = GetComponent<PlayerMoviment>().speed;
        pJumpSpeed = GetComponent<PlayerMoviment>().jSpeed;
        view = GetComponent<PhotonView>();
        pColor.color = Color.white;
    }

    private void Update()
    {
        if (view.IsMine)
        {
            //set player as wizzard
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                view.RPC("RPC_SetPlayerWizzardForm", RpcTarget.All);
            }

            //set player as cat
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                view.RPC("RPC_SetPlayerCatForm", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void RPC_SetPlayerWizzardForm()
    {
        Debug.Log(view.GetInstanceID().ToString() + "Wizzard Form");
        GetComponent<PlayerMoviment>().speed = pSpeed;
        GetComponent<PlayerMoviment>().jSpeed = pJumpSpeed;
        //pColor.color = Color.blue;
        renderer.material.color = Color.blue;
        gameObject.tag = "Wizzard";
    }

    [PunRPC]
    void RPC_SetPlayerCatForm()
    {
        Debug.Log(view.GetInstanceID().ToString() + "Cat Form");
        GetComponent<PlayerMoviment>().speed = 10;
        GetComponent<PlayerMoviment>().jSpeed = 10;
        //pColor.color = Color.red;
        renderer.material.color = Color.red;
        gameObject.tag = "Cat";
    }
}
