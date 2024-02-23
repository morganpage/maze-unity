using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] Animator _animator;
    private Vector3 _userInput;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = _animator.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _userInput = new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"),0);
        _animator.SetBool("walk",_userInput.sqrMagnitude > 0);
        if(_userInput.x == 0) return;
        _spriteRenderer.flipX = _userInput.x < 0;
    }

    void FixedUpdate(){
        _rigidbody2D.MovePosition(transform.position + (_userInput.normalized * _speed * Time.deltaTime));
    }

}
