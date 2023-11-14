using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Invaders_Manager _invadersManager;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Invaders")) return;
        _player.gameObject.SetActive(false);
        _invadersManager.IsGamePlaying = false;
    }
}
