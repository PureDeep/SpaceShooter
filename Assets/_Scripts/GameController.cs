using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public Vector3 spawnValues;
    private Vector3 spawnPosition = Vector3.zero;
    private Quaternion spawnRotation;

    public int hazardCount;

    public float spawnWait;

    public Text scoreText;
    private int score;

    public Text gameOverText;
    private bool gameOver;

    public Text restartText;
    private bool restart;

    // Start is called before the first frame update
    void Start()
    {
        gameOverText.text = "";
        gameOver = false;
        restartText.text = "";
        restart = false;
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                EditorSceneManager.LoadScene("Main");
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        for(int i = 0; i < hazardCount; i++)
        {
            if (gameOver)
            {
                restartText.text = "按【R】重新开始";
                restart = true;
                break;
            }
            spawnPosition.x = Random.Range(-spawnValues.x, spawnValues.x);
            spawnPosition.z = spawnValues.z;
            Instantiate(hazard, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(spawnWait);
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "得分：" + score;
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverText.text = "游戏结束";
    }
}
