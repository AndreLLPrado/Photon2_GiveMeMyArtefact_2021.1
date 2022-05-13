using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AimColorControl : MonoBehaviourPunCallbacks
{
    [SerializeField] private Renderer renderer;

    public void setColor(Color color){
        renderer.material.color = color;
    }
}
