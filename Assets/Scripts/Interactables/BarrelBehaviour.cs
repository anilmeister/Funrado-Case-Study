using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelBehaviour : Interactable
{
    [SerializeField]
    private Transform _exitLocation;

    [SerializeField]
    private MeshRenderer _exitPointerRing;

    private bool _isPlayerInside = false;

    void Start()
    {
        _exitPointerRing.enabled = false;
    }

    void Update()
    {
        if (Interacted)
        {
            EnterBarrel();
        }
        if (_isPlayerInside && Input.GetKeyDown(KeyCode.Mouse0))
        {
            ExitBarrel();
        }
    }

    void ExitBarrel()
    {
        _exitPointerRing.enabled = false;
        _isPlayerInside = false;
        _player.transform.position = _exitLocation.position;
        _player.SetActive(true);
    }

    void EnterBarrel()
    {
        _exitPointerRing.enabled = true;
        _isPlayerInside = true;
        Interacted = false;
        _player.SetActive(false);
    }

}
