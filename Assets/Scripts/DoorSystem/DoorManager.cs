using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField]
    private string requiredKeyColor;

    [SerializeField]
    private GameObject leftDoor, rightDoor;

    [SerializeField]
    private GameObject _boundBox;
    private BoxCollider _boundBoxCollider;
    
    private HingeJoint leftHinge, rightHinge;

    private PlayerInventory _playerInventory;

    [SerializeField]
    private int _hingeLimitMaxVal,_hingeLimitMinVal;
    private void Start()
    {
        _boundBoxCollider = _boundBox.GetComponent<BoxCollider>();
        leftHinge = leftDoor.GetComponent<HingeJoint>();
        rightHinge = rightDoor.GetComponent<HingeJoint>();
        LockDoor();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInventory = other.GetComponent<PlayerInventory>();

            if (_playerInventory != null)
            {
                if (_playerInventory.HasKey(requiredKeyColor))
                {
                    _playerInventory.RemoveKey(requiredKeyColor);
                    UnlockDoor();
                }

            }
        }
    }

    void LockDoor()
    {
        if (leftHinge != null)
        {
            JointLimits limits = leftHinge.limits;
            limits.min = 0;
            limits.max = 0;
            leftHinge.limits = limits;
        }
        if (rightHinge != null)
        {
            JointLimits limits = rightHinge.limits;
            limits.min = 0;
            limits.max = 0;
            rightHinge.limits = limits;
        }

    }

    void UnlockDoor()
    {
        
        _boundBoxCollider.gameObject.SetActive(false);
        if (leftHinge != null)
        {
            JointLimits limits = leftHinge.limits;
            limits.min = _hingeLimitMinVal;
            limits.max = _hingeLimitMaxVal;
            leftHinge.limits = limits;
        }

        if (rightHinge != null)
        {
            JointLimits limits = rightHinge.limits;
            limits.min = _hingeLimitMinVal;
            limits.max = _hingeLimitMaxVal;
            rightHinge.limits = limits;
        }

    }
}
