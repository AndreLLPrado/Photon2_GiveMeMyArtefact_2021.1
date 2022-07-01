using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SelectModeCanvas : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text desc;
    private int modeIndex;
    private PhotonView view;
    private string msg;
    [SerializeField] private GameObject selfPanel;
    private bool confirmed;

    void Start(){
        view = GetComponent<PhotonView>();
        modeIndex = 0;
        msg = "";
    }

    void Update(){
        if(view.IsMine){
            switch (modeIndex)
            {
                case 1://Blue Mode
                    msg = "Capture the Red player\n" +
                            "Create wall to block ways";
                    desc.text = msg;
                break;
                case 2://Red Mode
                    msg = "Run away from the blue player\n" +
                            "Destroy the environment";
                    desc.text = msg;
                break;
                default:
                    msg = "";
                    desc.text = msg;
                break;
            }
        } 
    }
    [PunRPC]
    public void RPC_setMode(int mode){
        //if(view.IsMine){
            modeIndex = mode;
        //}
    }

    public void setMode(int mode){
        if(view.IsMine){
            view.RPC("RPC_setMode", RpcTarget.All, mode);
        }
    }

    [PunRPC]
    public void RPG_confirmMode(){
        //if(view.IsMine){
            selfPanel.SetActive(false);
            confirmed = true;
        //}
    }
    public void confirmMode(){
        if(view.IsMine){
            view.RPC("RPG_confirmMode",RpcTarget.All);
        }
    }

    public int getModeIndex(){return modeIndex;}
    public bool getConfirmed(){return confirmed;}
}
