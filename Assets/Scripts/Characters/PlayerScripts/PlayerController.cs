using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : Character
{
    [SerializeField]
    private float _movementSpeed = 1f;

    [SerializeField]
    private int _startingLevel;

    [SerializeField] 
    private Joystick _joystick;

    [SerializeField]
    private GameManager _gameManager;



    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        Level = _startingLevel;
    }

    void Update()
    {
        DisplayLevel();
    }

    private void FixedUpdate()
    {
        Move(new Vector3(_joystick.Horizontal, 0, _joystick.Vertical),_movementSpeed);
    }
    public void LevelUp(int levelAmount)
    {
        Level += levelAmount;
    }
    public override void Die()
    {
        _isDead = true;
        _animator.SetBool("isDead", true);
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        StartCoroutine("RestartAfterDeathAnim");
    }

    IEnumerator RestartAfterDeathAnim()
    {
        yield return new WaitForSeconds(2f);
        _gameManager.OnDeathUIPPopUp();
    }
}
