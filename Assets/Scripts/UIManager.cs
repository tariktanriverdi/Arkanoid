using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text TargetText;
    public Text ScoreText;
    public Text LivesText;
    public int Score { get; set; }
    private void Awake(){
        Brick.OnBrickDestruction += OnBrickDestruction;
        BricksManager.OnLevelLoaded += OnLevelLoded;
        GameManager.OnLiveLost += OnLiveLost;
    }
    private void Start()
    {
        
        OnLiveLost(GameManager.Instance.AvailibleLives);
    }


    private void OnLiveLost(int live)
    {
        LivesText.text = $"LIVES:{live}";
    }

    private void OnLevelLoded()
    {
        UpdateRemainingBricksText();
        UpdateScoreText(0);

    }


    private void UpdateScoreText(int score)
    {
        this.Score += score;
        string scoreString = this.Score.ToString().PadLeft(5, '0');
        ScoreText.text = $"SCORE:{Environment.NewLine}{scoreString}";
    }


    private void OnBrickDestruction(Brick obj)
    {
        UpdateRemainingBricksText();
        UpdateScoreText(10);


    }

    private void UpdateRemainingBricksText()
    {
        TargetText.text = $"TARGET:{Environment.NewLine}{BricksManager.Instance.RemainingBricks.Count}/{BricksManager.Instance.InitialBricksCount}";

    }
    private void OnDisable()
    {
        Brick.OnBrickDestruction -= OnBrickDestruction;
        BricksManager.OnLevelLoaded -= OnLevelLoded;
        GameManager.OnLiveLost -= OnLiveLost;
    }


}
