using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    public bool UseController = false;
    public bool isDead = false;
    private MovementComponent movementComponent;
    private WeaponHandler weaponHandler;
    private Weapon weapon;

    private Vector3 mouseWorldPos;
    private Vector2 direction = Vector2.zero;
    private Vector2 aimDirection = Vector2.zero;
    private Vector2 previousMouseStopPosition = Vector2.zero;
    
    private void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    private void OnAim(InputValue value)
    {
        aimDirection = value.Get<Vector2>();
    }

    private void OnShoot(InputValue value)
    {
        if (isDead) {return;}
        if (weaponHandler.Attack(mouseWorldPos) && Gamepad.current != null)
        {
            StartCoroutine(PlayHaptic(.35f, .35f, 0.1f));
        }
    }

    private void OnController(InputValue value)
    {
        UseController = true;
    }

    private void OnRestart(InputValue value)
    {
        if (isDead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    IEnumerator PlayHaptic(float low, float high, float time)
    {
        Gamepad.current.SetMotorSpeeds(low, high);
        yield return new WaitForSeconds(time);
        Gamepad.current.ResetHaptics();
    }
    
    void Start()
    {
        if (movementComponent == null)
        {
            movementComponent = GetComponent<MovementComponent>();
        }
        if (weaponHandler == null)
        {
            weaponHandler = GetComponent<WeaponHandler>();
        }
        if (weapon == null)
        {
            weapon = gameObject.GetComponent<Weapon>();
        }
    }
    void Update()
    {
        /*
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction.y += -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction.x += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction.x += -1;
        }
        */

        if (isDead) {return;}

        movementComponent.Direction = direction;

        if (UseController)
        {
            Vector2 targetPosition = Vector2.zero;
            float biggestDot = 0;
            if (aimDirection != Vector2.zero)
            {
                mouseWorldPos = transform.position + (Vector3)aimDirection;
            }
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                var currentDot = Vector2.Dot(aimDirection, (enemy.transform.position - transform.position).normalized);
                if (biggestDot < currentDot)
                {
                    biggestDot = currentDot;
                    targetPosition = enemy.transform.position;
                }
            }

            if (targetPosition != Vector2.zero && biggestDot > 0.9)
            {
                mouseWorldPos = targetPosition;
            }
        }
        else
        {
            mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f; // zero z
            previousMouseStopPosition = mouseWorldPos;
        }
        

        if (mouseWorldPos.x < transform.position.x)
        {
            movementComponent.isFacingRight = false;
        }
        else if (mouseWorldPos.x > transform.position.x)
        {
            movementComponent.isFacingRight = true;
        }

        if (movementComponent.isFacingRight)
        {
            movementComponent.Sprite.flipX = false;
        }
        else
        {
            movementComponent.Sprite.flipX = true;
        }

        weapon.UpdateLocalPosition(gameObject.transform.position, mouseWorldPos);
    }


}