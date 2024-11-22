using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SocialPlatforms.Impl;
using System.Drawing;

public class UICheckTestScript : MonoBehaviour
{
    public HealthComponent Target;
    public Image Image;
    public TextMeshProUGUI Text;
    public GameObject ShowOnGameOver;
    public TextMeshProUGUI Flavor;
    public TextMeshProUGUI Rank;
    DifficultyThreshold GetDifficulty(int Points)
    {
        switch (Points)
        {
            case < (int)DifficultyThreshold.Green:
                return DifficultyThreshold.Blue;
            case < (int)DifficultyThreshold.Gray:
                return DifficultyThreshold.Green;
            case < (int)DifficultyThreshold.Black:
                return DifficultyThreshold.Gray;
            case < (int)DifficultyThreshold.White:
                return DifficultyThreshold.Black;
            case < (int)DifficultyThreshold.Purple:
                return DifficultyThreshold.White;
            case < (int)DifficultyThreshold.RGB:
                return DifficultyThreshold.Purple;
            case < (int)DifficultyThreshold.Progamer:
                return DifficultyThreshold.RGB;
            case >= (int)DifficultyThreshold.Progamer:
                return DifficultyThreshold.Progamer;
        }
    }

    String GetText(DifficultyThreshold diff)
    {
        return diff switch
        {
            DifficultyThreshold.Blue => "F,터짐",
            DifficultyThreshold.Green => "C,뉴비?",
            DifficultyThreshold.Gray => "B,고수",
            DifficultyThreshold.Black => "A,혹시 샌즈세요???",
            DifficultyThreshold.White => "S,썩은물",
            DifficultyThreshold.Purple => "SS,쿄시키 무라사키",
            DifficultyThreshold.RGB => "SSS,프로 게이머",
            DifficultyThreshold.Progamer => "완전하이퍼개잘함미친회피의신갓오브갓그는누구인가충무공마제스티말도안돼뉴네오젤리게임의신,미안하다 이거 보여주려고 어그로 끌었다",
            _ => "언더테일,아시는구나",
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;

        if (!Target)
        {
            Target = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Image.fillAmount = Target.Health / Target.MaxHealth;
        Text.text = Target.Health + " / " + Target.MaxHealth;

        if (Target.Health <= 0)
        {
            ShowOnGameOver.SetActive(true);
            string[] words = GetText(GetDifficulty(GameObject.FindObjectOfType<GameHandler>().Points)).Split(',');
            Rank.text = words[0];
            Flavor.text = words[1];
        }
    }
}
