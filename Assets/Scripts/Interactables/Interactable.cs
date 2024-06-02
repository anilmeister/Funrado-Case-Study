using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public bool Interacted { get; protected set; }
    protected GameObject _player;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            _player = other.gameObject;
            Interacted = true;
        }
    }
}
