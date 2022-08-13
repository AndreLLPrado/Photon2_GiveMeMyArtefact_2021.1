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
    private bool modoCriar;
    private bool modoDestruir;
    [SerializeField] private GameObject wall;
    public float wallAngle;
    public float scale;
    public float creationDistance;

    private bool pauseGame;
    void Start()
    {
        lr_laser = GetComponentInChildren<LineRenderer>();
        bulletTime = true;
        view = GetComponent<PhotonView>();
        modoCriar = false;
        modoCriar = false;
        wallAngle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        pauseGame = GameObject.Find("GameManager").GetComponent<GameManagerScript>().cCaught;
        //if(view.IsMine){
        //    if(modoCriar  && !modoDestruir){
        //        view.RPC("RPC_crateWall", RpcTarget.All);
        //    }
        //    else if(!modoCriar && modoDestruir){
        //        view.RPC("RPC_ShootRaycast", RpcTarget.All);
        //    }
        //    view.RPC("RPC_Reload", RpcTarget.All);
        //}
        if (!pauseGame)
        {
            if (modoCriar && !modoDestruir)
            {
                view.RPC("RPC_crateWall", RpcTarget.All);
            }
            else if (!modoCriar && modoDestruir)
            {
                view.RPC("RPC_ShootRaycast", RpcTarget.All);
            }
            view.RPC("RPC_Reload", RpcTarget.All);
        }
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
                hitInfo.transform.GetComponent<ObjetoDestrutivel>().callTomarDanoRPC(1);
           }
        }
        else{
            lr_laser.SetPosition(1, mira.position);
        }
    }
    [PunRPC]
    void RPC_crateWall(){
        lr_laser.SetPosition(0, transform.position);
        
        RaycastHit hitInfo;
        if(Physics.Raycast(transform.position,mira.position, out hitInfo, creationDistance)){
            Debug.Log("You don't create here, colide to: " + hitInfo.transform.name);
            lr_laser.SetPosition(1, hitInfo.transform.position);
        }else{
            Vector3 pos = new Vector3(mira.position.x, 1, mira.position.z);
            if(Input.GetMouseButtonDown(0)){
                Quaternion angle = new Quaternion(0,wallAngle,0,0);
                PhotonNetwork.Instantiate("wall", pos, angle);
            }
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

    public void setPlayerMode(string mode){
        if(mode != null){
            if(mode.ToLower() == "red"){
                modoDestruir = true;
                modoCriar = false;
            }
            else if(mode.ToLower() == "blue"){
                modoDestruir = false;
                modoCriar = true;
            }
            else{
                Debug.Log("This mode doesn't exist: " + mode.ToLower());
            }
        }
        else{
            Debug.Log("Mode is equal to null");
        }
    }

    //void OnPhotonInstantiate(PhotonMessageInfo info) { 
        
    //}
}
