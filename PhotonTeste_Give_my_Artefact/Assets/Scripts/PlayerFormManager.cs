using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerFormManager : MonoBehaviourPunCallbacks, IPunObservable
{
    float pSpeed, pJumpSpeed;
    int formIndex;
    PhotonView view;
    [SerializeField] private Material pColor;
    [SerializeField] private Renderer renderer;
    private RaycastShooting mode;
    private AimColorControl aimColor;


    private void Start()
    {
        pSpeed = GetComponent<PlayerMoviment>().speed;
        pJumpSpeed = GetComponent<PlayerMoviment>().jSpeed;
        view = GetComponent<PhotonView>();
        pColor.color = Color.white;
        mode = GetComponent<RaycastShooting>();
        aimColor = GetComponentInChildren<AimColorControl>();

        //view.RPC("RPC_RandomizeForm", RpcTarget.All);
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
        mode.setPlayerMode("blue");
        aimColor.setColor(Color.blue);
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
        aimColor.setColor(Color.red);
        mode.setPlayerMode("red");
        renderer.material.color = Color.red;
        gameObject.tag = "Cat";
    }

    [PunRPC]
    void RPC_RandomizeForm()
    {
        if(formIndex == 0)
        {
            formIndex = Random.Range(1, 3);
        }

        //wizzard index
        if(formIndex == 1)
        {
            Debug.Log("Index from set to Wizzard");
        }

        //cat index
        if (formIndex == 2)
        {
            Debug.Log("Index from set to Cat");
        }
        //wrong index
        else
        {
            Debug.Log("anything is wrong!");
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //local
        if (stream.IsWriting)
        {
            stream.SendNext(formIndex);

            //if(formIndex == 1)
            //{

            //}

            //if(formIndex == 2)
            //{

            //}
        }
        //client
        else
        {
            formIndex = (int)stream.ReceiveNext();

            //if (formIndex == 1)
            //{

            //}

            //if (formIndex == 2)
            //{

            //}
        }
    }
}
