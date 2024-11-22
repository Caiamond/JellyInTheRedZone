using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float Size = 5;
    public float FuseTime = 3;
    private float effectTime = .5f;
    private bool hasExplosionStarted = false;

    [SerializeField]
    private GameObject dangerZone;
    [SerializeField]
    private GameObject dangerFill;
    [SerializeField]
    private ParticleSystem explosion;
    private float timeElasped = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //dangerZone.transform.localScale = Vector2.one * Size;
    }

    

    // Update is called once per frame
    void Update()
    {
        timeElasped += Time.deltaTime;
        dangerZone.transform.localScale = Vector2.one * 1;
        dangerFill.transform.localScale = Vector2.one * Mathf.Lerp(0, 1, timeElasped/FuseTime);

        if (timeElasped >= FuseTime && !hasExplosionStarted)
        {
            hasExplosionStarted = true;
            Debug.Log("BOOM!!!!!!!!!!!!!!!");
            explosion.Play();
            dangerFill.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
            dangerZone.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);

            if (GameObject.FindGameObjectWithTag("Player") && gameObject.GetComponent<CircleCollider2D>().IsTouching(GameObject.FindGameObjectWithTag("Player").GetComponent<CircleCollider2D>()))
            {
                Debug.Log("yo got hit by bomb bruh.");
                GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>().Health -= 7;
            }


        }

        if (timeElasped > FuseTime + effectTime)
        {
            Destroy(gameObject);
        }
    }
}
