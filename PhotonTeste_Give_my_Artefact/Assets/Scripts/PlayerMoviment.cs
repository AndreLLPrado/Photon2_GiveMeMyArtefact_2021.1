using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMoviment : MonoBehaviour
{
    CharacterController cc;
    Vector3 move;
    PhotonView view;

    public float speed;
    public float jSpeed;
    public float grativy;

    public bool pStop;
    bool posRes = false;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (view.IsMine) 
        {
            pStop = GameObject.Find("GameManager").GetComponent<GameManagerScript>().playerStop;

            if (!pStop)
            {
                if(GetComponent<PlayerFormManager>().confirmedMode){
                    MovePlayer();
                    posRes = false;
                }
            }

            if(pStop)
                GamerOverRespawner(pStop);
            else
            {
                GamerOverRespawner(pStop);
            }

            if (transform.position.y <= -10f)
            {
                RespawnPlayer();
            }
        }
    }

    void GamerOverRespawner(bool p)
    {
        float x, z;
        
        cc.enabled = !p;
        if (p && !posRes)
        {
            x = Random.Range(-7.5f, 8f);
            z = Random.Range(-8.5f, 8.5f);
            transform.position = new Vector3(x, 10f, z);
            posRes = true;
        }
    }
    void RespawnPlayer()
    {
        cc.enabled = false;
        transform.position = new Vector3(0f, 10f, 0f);
        cc.enabled = true;
    }
    private void MovePlayer()
    {
        if (cc.isGrounded)
        {
            move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            move = transform.TransformDirection(move);
            move *= speed;

            if (Input.GetButton("Jump"))
            {
                move.y = jSpeed;
            }
        }

            move.y -= grativy * Time.deltaTime;

            cc.Move(move * Time.deltaTime);
    }
}
