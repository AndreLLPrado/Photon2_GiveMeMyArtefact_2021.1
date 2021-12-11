using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    float x, z;

    private void Start()
    {
        x = Random.Range(-7.5f, 8f);
        z = Random.Range(-8.5f, 8.5f);
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(x, 10f, z),Quaternion.identity);
    }
}
