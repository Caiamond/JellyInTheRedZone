using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;


enum DifficultyThreshold //3000 = 1min
{
    Blue = 0, // F (터짐)
    Green =  1500, // C (뉴비?)
    Gray = 3000, // B (고수)
    Black = 6000, // A (혹시 샌즈세요???)
    White = 9000, // S (썩은물)
    Purple = 12000, // SS (쿄시키 무라사키)
    RGB = 15000, // SSS (프로 게이머)
    Progamer = 25000 // 완전하이퍼개잘함미친회피의신갓오브갓그는누구인가충무공마제스티말도안돼뉴네오젤리게임의신
}




public class GameHandler : MonoBehaviour
{

    public GameObject Bomb;
    public GameObject Jelly;

    public GameObject Bombs;
    public GameObject Enemys;

    public TextMeshProUGUI PointText;

    public int Points = 0;

    public float MaxBombCD = 2f;
    public float MinBombCD = 0.4f;

    public float MaxJelCD = 15f;
    public float MinJelCD = 5f;

    private float nextBombCD = 0f;
    private float nextJelCD = 0f;
    private float timeElasped = 0f;
    private float timeElaspedFromLastBomb = 0f;
    private float timeElaspedFromLastEnemy = 0f;

    private DifficultyThreshold diff = DifficultyThreshold.Blue;

    // Start is called before the first frame update
    void Start()
    {
        nextBombCD = Random.Range(MinBombCD, MaxBombCD);
        nextJelCD = Random.Range(MinJelCD, MaxJelCD);
    }

    // Update is called once per frame
    void Update()
    {
        timeElasped += Time.deltaTime;
        timeElaspedFromLastBomb += Time.deltaTime;
        timeElaspedFromLastEnemy += Time.deltaTime;

        if (timeElaspedFromLastBomb >= nextBombCD)
        {
            SpawnBomb();
            nextBombCD = Random.Range(MinBombCD, MaxBombCD);
            timeElaspedFromLastBomb = 0;
        }

        if (timeElaspedFromLastEnemy >= nextJelCD)
        {
            SpawnJelly();
            nextJelCD = Random.Range(MinJelCD, MaxJelCD);
            timeElaspedFromLastEnemy = 0;
        }
    }

    void FixedUpdate()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player && player.GetComponent<HealthComponent>().Health > 0)
        {
            Points += 1;
            diff = GetDifficulty();
            PointText.text = Points.ToString();
            PointText.color = GetColorByDifficultyThreshold(diff);
        }
    }

    DifficultyThreshold GetDifficulty()
    {
        switch (Points)
        {
            case < (int)DifficultyThreshold.Green:
                return DifficultyThreshold.Blue;
            case < (int)DifficultyThreshold.Gray:
                MaxBombCD = 0.75f;
                MinBombCD = 0.3f;
                return DifficultyThreshold.Green;
            case < (int)DifficultyThreshold.Black:
                MaxJelCD = 6f;
                MinJelCD = 4f;
                return DifficultyThreshold.Gray;
            case < (int)DifficultyThreshold.White:
                MaxBombCD = 0.5f;
                MinBombCD = 0.2f;
                return DifficultyThreshold.Black;
            case < (int)DifficultyThreshold.Purple:
                MaxJelCD = 5f;
                MinJelCD = 4f;
                return DifficultyThreshold.White;
            case < (int)DifficultyThreshold.RGB:
                MaxBombCD = 0.4f;
                MinBombCD = 0.15f;
                return DifficultyThreshold.Purple;
            case < (int)DifficultyThreshold.Progamer:
                MaxJelCD = 3.25f;
                MinJelCD = 2.25f;
                MaxBombCD = 0.2f;
                MinBombCD = 0.15f;
                return DifficultyThreshold.RGB;
            case >= (int)DifficultyThreshold.Progamer:
                MaxJelCD = 2f;
                MinJelCD = 1f;
                return DifficultyThreshold.Progamer;
        }
    }

    Color GetColorByDifficultyThreshold(DifficultyThreshold diff)
    {
        return diff switch
        {
            DifficultyThreshold.Blue => Color.blue,
            DifficultyThreshold.Green => Color.green,
            DifficultyThreshold.Gray => Color.grey,
            DifficultyThreshold.Black => Color.black,
            DifficultyThreshold.White => Color.white,
            DifficultyThreshold.Purple => new Color(255, 0, 255),
            DifficultyThreshold.RGB => Color.red,
            DifficultyThreshold.Progamer => new Color(Random.Range(0, 256), Random.Range(0, 256), Random.Range(0, 256)),
            _ => Color.blue,
        };
    }

    void SpawnBomb()
    {
        GameObject newBomb = Instantiate(Bomb, new Vector2(Random.Range(-12, 12), Random.Range(-4.5f, 8)), Quaternion.identity, Bombs.transform);
        newBomb.transform.localScale = Vector2.one * Random.Range(3f, 10f);
        
    }

    void SpawnJelly()
    {
        GameObject newJelly = Instantiate(Jelly, new Vector2(Random.Range(-12, 12), Random.Range(-4.5f, 8)), Quaternion.identity, Enemys.transform);
    }

    void AddPoints(int point)
    {
        Points += point;
    }
}
