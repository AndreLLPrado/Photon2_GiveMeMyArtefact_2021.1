using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Timer : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] Text timer;
    public float time;
    private float aux;
    public bool sTimer;
    public bool rTimer;
    public bool eTime;

    PhotonView view;


    private void Start()
    {
        view = GetComponent<PhotonView>();
        aux = time;
        timer.text = time.ToString() + " s";

        //float t = time;
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    Hashtable ht = new Hashtable() { { "Time", timer } };
        //    PhotonNetwork.CurrentRoom.SetCustomProperties(ht);
        //}
        //else
        //{
        //    t = (float)PhotonNetwork.CurrentRoom.CustomProperties["Time"];
        //}
    }

    private void Update()
    {
        if (view.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                rTimer = true;
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                sTimer = true;
            }

            if (sTimer && !eTime)
            {
                view.RPC("RPC_StartTimer",RpcTarget.All);
            }

            if (rTimer)
            {
                view.RPC("RPC_ResetTimer", RpcTarget.All);
            }

            if (eTime)
            {
                time = 0.0f;
                timer.text = time.ToString() + " s";
            }
        }
    }

    [PunRPC]
    void RPC_StartTimer()
    {
        time -= 1 * Time.deltaTime;
        //time -= 1 * PhotonNetwork.Time;

        timer.text = time.ToString("F2") + " s";
        if(time <= 0)
        {
            eTime = true;
        }
    }

    [PunRPC]
    void RPC_ResetTimer()
    {
        sTimer = false;
        rTimer = false;
        eTime = false;
        time = aux;
        timer.text = time.ToString() + " s";
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(time);
        }
        else
        {
            time = (float)stream.ReceiveNext();
        }
    }
}
