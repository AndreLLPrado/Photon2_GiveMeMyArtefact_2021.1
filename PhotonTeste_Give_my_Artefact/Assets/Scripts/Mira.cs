using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Mira : MonoBehaviourPunCallbacks
{
    [SerializeField] private Vector3 offSet;
    PhotonView view;
    private void Start(){
        view = GetComponentInParent<PhotonView>();
    }
    private void Update() {
        if(view.IsMine){
            //offSet = transform.position - MouseWorldPositon();
            transform.position = MouseWorldPositon() + offSet;
        }
    }
    private void OnMouseDown(){
        offSet = transform.position - MouseWorldPositon();
    }
    // private void OnMouseDrag() {
    //     transform.position = MouseWorldPositon() + offSet;
    // }

    Vector3 MouseWorldPositon(){
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}
