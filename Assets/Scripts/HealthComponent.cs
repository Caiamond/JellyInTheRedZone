using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealthComponent : MonoBehaviour
{
    public float MaxHealth = 25f;
    public float Health;

    

    

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            if (gameObject.CompareTag("Player"))
            {
                gameObject.GetComponent<PlayerInput>().isDead = true;
                Gamepad.current?.ResetHaptics();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
