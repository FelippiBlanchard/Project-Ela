using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _squatSpeed;

    private Vector2 _currentDirection;
    private bool _movingX;
    private bool _movingY;
    private bool _jumping;
    private bool _interacting;

    private State _currentState;

    private Rigidbody2D _rigidbody;

    private void Update()
    {
        //Bloqueia input caso esteja
        if (_jumping) return;
        if (_interacting) return;

        //Se interagir, parar tudo e interagir
        if (Input.GetKey(KeyCode.F))
        {
            SetState(State.Interacting);
            return;
        }

        //Setar eixo de movimentação
        SetDirectionInputAxis();

        //Se não tiver direção nenhuma, não tenta andar nem correr
        if(!_movingX && !_movingY)
        {
            SetState(State.Idle);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Run();
                SetState(State.Running);
            }
            else 
            {
                Walk();
                SetState(State.Walking);
            }
        }

        //Parado ou andando, pode agachar
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Squat();
            SetState(State.Squating);
        }

        //Parado ou andando, pode pular
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            SetState(State.Jumping);
        }

        SetAnimation(_currentState);

    }


    private void Walk()
    {
        _rigidbody.MovePosition(_rigidbody.position + _currentDirection * _walkSpeed * Time.deltaTime);
    }

    private void Run()
    {
        _rigidbody.MovePosition(_rigidbody.position + _currentDirection * _runSpeed * Time.deltaTime);
    }

    private void Jump()
    {

    }

    private void Squat()
    {
        _rigidbody.MovePosition(_rigidbody.position + _currentDirection * _squatSpeed * Time.deltaTime);
    }

    private void SetDirectionInputAxis()
    {
        //Caso já esteja se movendo em um eixo, não olha o input do outro eixo
        if (!_movingY)
        {
            float inputX = Input.GetAxis("Horizontal");
            _currentDirection.x = inputX;
            _movingX = inputX != 0;
        }
        if (!_movingX)
        {
            float inputY = Input.GetAxis("Vertical");
            _currentDirection.y = inputY;
            _movingY = inputY != 0;
        }
    }

    private void SetAnimation(State state)
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Running:
                break;
            case State.Walking:
                break;
            case State.Squating:
                break;
            case State.Jumping:
                break;
            case State.Interacting:
                break;
        }
    }

    private void SetState(State state)
    {
        _currentState = state;
    }

    private enum State
    {
        Idle,
        Walking,
        Running,
        Squating,
        Jumping,
        Interacting
    }
}
