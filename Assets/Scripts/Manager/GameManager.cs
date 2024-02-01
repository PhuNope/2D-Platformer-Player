using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private Transform respawnPoint;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float respawnTime;

    private float resawpnTimeStart;

    private bool respawn;

    private CinemachineVirtualCamera CVC;

    private void Start() {
        CVC = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
    }

    private void Update() {
        CheckRespawn();
    }

    public void Respawn() {
        respawnTime = Time.time;
        respawn = true;
    }

    private void CheckRespawn() {
        if (Time.time >= resawpnTimeStart + respawnTime && respawn) {
            var playTemp = Instantiate(player, respawnPoint);
            CVC.m_Follow = playTemp.transform;
            respawn = false;
        }
    }
}
