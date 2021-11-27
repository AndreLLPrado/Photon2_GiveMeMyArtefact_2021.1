using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerDirectionPointer : MonoBehaviour
{
    Vector3 direction;
    public LayerMask mask;
    public float distance;
    LineRenderer lr_pointer;
    PhotonView view;

    private void Start()
    {
        lr_pointer = GetComponentInChildren<LineRenderer>();
        view = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (view.IsMine)
        {
            lr_pointer.SetPosition(0, transform.position);
            direction = new Vector3(0f, 1f, 0f);

            if (Input.GetButton("Horizontal"))
            {
                direction.x = Input.GetAxis("Horizontal");
            }
            else
            {
                direction.x = transform.localPosition.x;
            }

            if (Input.GetButton("Vertical"))
            {
                direction.z = Input.GetAxis("Vertical");
            }
            else
            {
                direction.z = transform.localPosition.z;
            }
            //direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            direction = transform.TransformDirection(direction);
            direction *= distance;
            lr_pointer.SetPosition(1, direction);
            //RaycastHit hit;
            //if(Physics.Raycast(transform.position, direction, out hit, distance, mask))
            //{
            //    lr_pointer.SetPosition(1, direction * distance);
            //}
        }
    }
}
