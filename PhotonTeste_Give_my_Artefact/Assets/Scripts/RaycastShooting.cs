using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RaycastShooting : MonoBehaviourPunCallbacks
{
    public LayerMask layer;
    public Transform mira;
    LineRenderer lr_laser;
    bool bulletTime;
    public float delay;
    float aux;
    PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        lr_laser = GetComponentInChildren<LineRenderer>();
        bulletTime = true;
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine){
            view.RPC("RPC_ShootRaycast", RpcTarget.All);
            view.RPC("RPC_Reload", RpcTarget.All);
        }
        //ShootRaycast();
        //Reload();
    }
    [PunRPC]
    void RPC_ShootRaycast(){
        lr_laser.SetPosition(0, transform.position);
        RaycastHit hitInfo;
        if(Physics.Raycast(transform.position, mira.position, out hitInfo, Mathf.Infinity, layer)){
           lr_laser.SetPosition(1, hitInfo.transform.position);
           Debug.Log("Hited: " + hitInfo.transform.name);
           if(Input.GetMouseButtonDown(0)){
               Debug.Log("Atirou!");
               //hitInfo.transform.GetComponent<ObjetoDestrutivel>().tomarDano(1);
               hitInfo.transform.GetComponent<ObjetoDestrutivel>().callTomarDanoRPC(1);
               //mouseClickToShooting(hitInfo.transform);
           }
        }
        else{
            lr_laser.SetPosition(1, mira.position);
        }
    }

    void mouseClickToShooting(Transform target){
        if(bulletTime){
            Debug.Log("Atirou!");
            //GetComponentInChildren<ParticleController>().ShootAnimationStart();
            //target.GetComponent<ObjetoDestrutivel>().tomarDano(1);
            bulletTime = false;
            aux = delay;
        }
    }
    [PunRPC]
    void RPC_Reload(){
         if(!bulletTime){
            Debug.Log("Recarregando");
            delay -= 1.0f * Time.deltaTime;
            if(delay <= 0.0f){
                bulletTime = true;
                delay = aux;
            }
        }
    }
}
