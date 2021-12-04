using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DestructibleObjects : MonoBehaviourPunCallbacks, IPunObservable
{
    public int life;
    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    [PunRPC]
    void RPC_TakeDamage()
    {
        life--;
        if(life <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "FireBall")
        {
            view.RPC("RPC_TakeDamage", RpcTarget.All);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(life);
        }
        else
        {
            life = (int)stream.ReceiveNext();
        }
    }
}
