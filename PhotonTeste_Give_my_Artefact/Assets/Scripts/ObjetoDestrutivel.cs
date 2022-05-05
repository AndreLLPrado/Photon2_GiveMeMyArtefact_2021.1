using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObjetoDestrutivel : MonoBehaviourPunCallbacks
{
    public int life;
    [SerializeField] private Renderer renderer;
    [SerializeField] private Material color;
    PhotonView view;

    void Start(){
        view = GetComponent<PhotonView>();
        color.color = Color.gray;
        view.RPC("RPC_setColor", RpcTarget.All, life);
    }

    [PunRPC]
    void RPC_tomarDano(int d){
        if(life > 0){
            life -= d;
            view.RPC("RPC_setColor",RpcTarget.All, life);
        }
        else{
            Destroy(gameObject);
        }
    }

    public void callTomarDanoRPC(int d){
        view.RPC("RPC_tomarDano", RpcTarget.All, d);
    }

    [PunRPC]
    void RPC_setColor(int c){
        if(c > 3){// life igual 4 pra cima
            renderer.material.color = Color.blue;
        }
        else if(c > 2){// life igual 3 pra cima
            renderer.material.color = Color.green;
        }
        else if(c > 1){// life igual 2
            renderer.material.color = Color.yellow;
        }
        else if(c > 0){// life igual 1
            renderer.material.color = Color.red;
        }
        else{// life igual 0
            renderer.material.color = Color.black;
        }
    }
    public int getLife(){return life;}
}
