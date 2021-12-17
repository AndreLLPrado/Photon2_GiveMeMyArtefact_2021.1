using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SceneManager : MonoBehaviour
{
    public void BackToMenu()
    {
        LoadScene("Lobby");
    }

    public void Credits()
    {
        LoadScene("Credits");
    }

    public void HowToPlay()
    {
        LoadScene("HowToPlay");
    }

    public static void LoadScene(string scene)
    {
        //SceneManager.LoadScene(scene);
        PhotonNetwork.LoadLevel(scene);
    }
}
