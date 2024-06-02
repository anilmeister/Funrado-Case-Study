using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    private PlayerController _playerRef;

    [SerializeField]
    private int _levelAmount;

    private void OnTriggerEnter(Collider other)
    {
        try
        {
            _playerRef = other.gameObject.GetComponent<PlayerController>();
        }
        catch
        {

        }
        if (other.gameObject.CompareTag("Player"))
        {
            _playerRef.LevelUp(_levelAmount);
            Destroy(gameObject);
        }
    }
}
