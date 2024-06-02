using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Character : MonoBehaviour
{
    public int Level { get; protected set; }

    protected Rigidbody _rigidBody;
    protected Animator _animator;

    [SerializeField]
    protected float _rotationSpeed = 0.1f;

    protected Camera mainCamera;

    private TMP_Text _levelText;

    protected bool _isDead = false;

    private Character _collidedGameObject;

    private int _attackTrigger = Animator.StringToHash("Attack");


    protected virtual void Awake()
    {
        _levelText = GetComponentInChildren<TMP_Text>();
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        if (_rigidBody == null)
        {
            Debug.LogError("Rigidbody component is missing on " + gameObject.name);
        }
        if (_levelText == null)
        {
            Debug.LogError("Level text mesh component is missing on " + gameObject.name);
        }
        if (_animator == null)
        {
            Debug.LogError("Animator component is missing on " + gameObject.name);
        }
    }

    public virtual void Move(Vector3 direction,float _movementSpeed)
    {
        if (!_isDead)
        {
            if (direction.x != 0 || direction.z != 0)
            {
                _rigidBody.velocity = new Vector3(direction.x * _movementSpeed, 0, direction.z * _movementSpeed);
                _animator.SetBool("isIdle", false);
                _animator.SetBool("isRunning", true);
                Quaternion rotationGoal;
                rotationGoal = Quaternion.LookRotation(_rigidBody.velocity);
                transform.rotation = Quaternion.Lerp(transform.rotation,rotationGoal, _rotationSpeed*Time.deltaTime);
            }
            else
            {
                _rigidBody.velocity = Vector3.zero;
                _animator.SetBool("isIdle", true);
                _animator.SetBool("isRunning", false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Attack(other);
    }

    private void Attack(Collider collidedObject)
    {
        try
        {
            _collidedGameObject = collidedObject.gameObject.GetComponentInParent<Character>();
        }
        catch
        {

        }

        if (_collidedGameObject != null && Level > _collidedGameObject.Level && !_collidedGameObject._isDead && !collidedObject.CompareTag(gameObject.tag))
        {
            _animator.SetTrigger(_attackTrigger);
            _collidedGameObject.Die();
        }
    }

    public virtual void DisplayLevel()
    {
        _levelText.text = "Lv. " + Level.ToString();
        _levelText.transform.rotation = Quaternion.LookRotation(Vector3.down);
    }

    public abstract void Die();
}
