using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class HealthComponent : MonoBehaviour
{
    public float MaxHealth = 25f;
    public float Health;
    public GameObject HealthPickupObject;
    private float overHealthDecrease = 0;

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Health > MaxHealth)
        {
            if ((overHealthDecrease += Time.deltaTime) >= 1)
            {
                overHealthDecrease = 0;
                Health--;
            }
        }
        if (Health <= 0)
        {
            if (gameObject.CompareTag("Player"))
            {
                gameObject.GetComponent<PlayerInput>().isDead = true;
                Gamepad.current?.ResetHaptics();
            }
            else
            {
                var rng = Random.Range(0, 5);
                Debug.Log(rng);
                if (rng == 0)
                {
                    GameObject newPickup = Instantiate(HealthPickupObject, gameObject.transform.position, Quaternion.identity);
                    newPickup.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-4f, 4f), UnityEngine.Random.Range(-4f, 4f)).normalized * UnityEngine.Random.Range(0f, 2f);
                }
                Destroy(gameObject);
            }
        }
    }
}
