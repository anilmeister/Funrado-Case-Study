using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Character
{
    [SerializeField]
    private float _movementSpeed = 1f;

    [SerializeField]
    private Transform[] _patrolPoints;

    [SerializeField]
    private int _currentPatrolIndex = 0;

    [SerializeField]
    private bool _isPatroling = true;

    [SerializeField]
    private bool _isWaiting = false;

    [SerializeField]
    private int _startingLevel;

    [SerializeField]
    private GameObject _deathEffect;

    private CapsuleCollider _capsuleCollider;

    [SerializeField]
    private float _patrolPointDistanceTolerance = 0.1f;

    [SerializeField]
    private float _patrolPointWaitDuration = 1f;

    void Start()
    {
        Level = _startingLevel;
        _capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
    }


    void Update()
    {
        DisplayLevel();
    }
    private void FixedUpdate()
    {
        if (_isPatroling && !_animator.GetBool("isDead"))
        {
            Patrol();
        }
    }

    void Patrol()
    {
        float distanceToPoint = Vector3.Distance(transform.position, _patrolPoints[_currentPatrolIndex].position);
        if (_isWaiting)
        {
            Move(Vector3.zero, _movementSpeed);
            return;
        }
        if (distanceToPoint < _patrolPointDistanceTolerance)
        {
            _currentPatrolIndex++;
            _isWaiting = true;
            StartCoroutine("PatrolAwait");
            if (_currentPatrolIndex >= _patrolPoints.Length)
            {
                _currentPatrolIndex = 0;
            }
        }
        else
        {
            float speedModifier = Mathf.Clamp(distanceToPoint / 0.5f, 0.5f, 1.0f);
            Vector3 direction = (_patrolPoints[_currentPatrolIndex].position - transform.position).normalized;
            Move(direction, _movementSpeed * speedModifier);

        }

    }
    IEnumerator PatrolAwait()
    {
        yield return new WaitForSeconds(_patrolPointWaitDuration);
        _isWaiting = false;
    }

    public override void Die()
    {
        _isDead = true;
        _animator.SetBool("isDead", true);
        StartCoroutine("DeathHandler");        
    }

    IEnumerator DeathHandler()
    {
        _capsuleCollider.enabled = false;
        yield return new WaitForSeconds(2f);
        Instantiate(_deathEffect, transform.position,Quaternion.identity,transform);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
