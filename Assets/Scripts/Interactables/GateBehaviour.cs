using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBehaviour : Interactable
{
    [SerializeField]
    private GameObject _gate;
    [SerializeField]
    private Transform _endLocation;
    [SerializeField]
    private float _gateSpeed = 1;
    private bool _isOpen = false;

    [SerializeField]
    private List<Material> _buttonMat;
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material = _buttonMat[0];
    }

    void FixedUpdate()
    {
        if (Interacted && !_isOpen && DistanceToEndLocation()>1)
        {
            gameObject.GetComponent<MeshRenderer>().material = _buttonMat[1];
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        _gate.transform.position = Vector3.Lerp(_gate.transform.position, _endLocation.position, _gateSpeed * Time.deltaTime);
    }

    private float DistanceToEndLocation()
    {
        return Vector3.Distance(_gate.transform.position, _endLocation.position);
    }
}
