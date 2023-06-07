using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    [SerializeField]
    private Text enemyKillCountTxt;

    private int EnemyKillCount;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void EnemyKilled()
    {
        EnemyKillCount++;
        enemyKillCountTxt.text = "Enemies Killed: " + EnemyKillCount;
    }
    public void RestartGame()
    {
        Invoke("Restart", 3f);
    }

    void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }
}