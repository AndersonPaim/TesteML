using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public delegate void SetFinalScoreHandler(float kills, float points);
    public SetFinalScoreHandler OnSetFinalScore;

    private float _points;
    private float _kills;

    private void Start()
    {
        SetupDelegates();
    }

    private void OnDestroy() 
    {
        RemoveDelegates();
    }

    private void SetupDelegates()
    {
        Enemy.OnKill += KillEnemy;
        GameManager.sInstance.OnGameOver += SetFinalScore;
        GameManager.sInstance.OnFinish += SetFinalScore;
    }

    private void RemoveDelegates()
    {
        Enemy.OnKill -= KillEnemy;
        GameManager.sInstance.OnGameOver -= SetFinalScore;
        GameManager.sInstance.OnFinish -= SetFinalScore;
    }

    private void KillEnemy(float points) //increase score when killing an enemy
    {
        _points += points;
        _kills++;
    }

    private void SetFinalScore() //set score when the game ends
    {
        OnSetFinalScore?.Invoke(_kills, _points);
    }
}
