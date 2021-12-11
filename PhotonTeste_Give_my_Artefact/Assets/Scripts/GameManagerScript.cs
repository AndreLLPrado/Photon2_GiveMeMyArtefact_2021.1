using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameManagerScript : MonoBehaviourPunCallbacks, IPunObservable
{
    public bool endTime;
    public bool cCaught;
    public bool playerStop;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject panel;
    [SerializeField] Text winTxt;
    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        panel.SetActive(false);
    }
    private void Update()
    {
        endTime = GameObject.Find("Canvas").GetComponent<Timer>().eTime;


        if (endTime || cCaught)
        {
            panel.SetActive(true);
            //view.RPC("RPC_GameOver", RpcTarget.All);
            GameOver();
        }
    
    }

    //[PunRPC]
    void GameOver()
    {
        playerStop = true;
        //cat wins
        if (endTime && !cCaught)
        {
            winTxt.color = Color.red;
            winTxt.text = "Red wins!";
        }
        //wizzard wins
        if (!endTime && cCaught)
        {
            winTxt.color = Color.blue;
            winTxt.text = "Blue wins!";
        }
        //in game
        if (!endTime && !cCaught)
        {
            winTxt.color = Color.black;
            winTxt.text = "In progress!";
        }

        //if (endTime && cCaught)
        //{
        //    winTxt.color = Color.white;
        //    winTxt.text = "DRAW!";
        //}
    }

    public void RestartGame()
    {
        playerStop = true;
        cCaught = false;
        GameObject.Find("Canvas").GetComponent<Timer>().rTimer = true;
        panel.SetActive(false);
        playerStop = false;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(cCaught);
        }
        else
        {
            cCaught = (bool)stream.ReceiveNext();
        }
    }
}
