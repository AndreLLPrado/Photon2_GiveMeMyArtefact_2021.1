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

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (view.IsMine) 
        { 
            MovePlayer();

            //if (transform.position.y <= -10f)
            //{
            //    StartCoroutine("RespawnPlayer");
            //    RespawnPlayer();
            //}
        }
    }

    IEnumerable RespawnPlayer()
    {
        cc.enabled = false;
        transform.position = new Vector3(0f, 10f, 0f);
        yield return new WaitForSeconds(1);
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
