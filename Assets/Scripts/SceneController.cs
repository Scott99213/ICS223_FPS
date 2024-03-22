using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyPrefab;
    private const int enemyCount = 5;
    private GameObject[] enemies = new GameObject[enemyCount];
    private Vector3 spawnPoint = new Vector3(0, 0, 5);

    [SerializeField] private GameObject iguanaPrefab;
    [SerializeField] private Transform iguanaSpawnPt;
    private const int iguanaCount = 10;
    private GameObject[] Iguanas = new GameObject[iguanaCount];

    [SerializeField] private UIController ui;
    private int score = 0;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_DEAD, OnEnemyDead);
        Messenger<int>.AddListener(GameEvent.DIFFICULTY_CHANGED, OnDifficultyChanged);
        Messenger.AddListener(GameEvent.PLAYER_DEAD, OnPlayerDead);
        Messenger.AddListener(GameEvent.RESTART_GAME, OnRestartGame);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_DEAD, OnEnemyDead);
        Messenger<int>.RemoveListener(GameEvent.DIFFICULTY_CHANGED, OnDifficultyChanged);
        Messenger.RemoveListener(GameEvent.PLAYER_DEAD, OnPlayerDead);
        Messenger.RemoveListener(GameEvent.RESTART_GAME, OnRestartGame);
    }

    public void OnRestartGame()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDifficultyChanged(int newDifficulty) 
    {
        Debug.Log("Scene.OnDifficultyChanged(" + newDifficulty + ")");
        for (int i = 0; i < enemies.Length; i++)
        {
            WanderingAI ai = enemies[i].GetComponent<WanderingAI>();
            ai.SetDifficulty(newDifficulty);
        }
    }

    public int GetDifficulty()
    {
        return PlayerPrefs.GetInt("difficulty", 1);
    }

    private void OnEnemyDead()
    {
        score++;
        ui.UpdateScore(score);
    }

    private void OnPlayerDead()
    {
        ui.ShowGameOverPopup();
    }

    // Start is called before the first frame update
    void Start()
    {
        ui.UpdateScore(score);
        for (int i = 0; i < iguanaCount; i++)
        {
            Iguanas[i] = Instantiate(iguanaPrefab) as GameObject;
            Iguanas[i].transform.position = iguanaSpawnPt.position;
            float angle = Random.Range(0, 360);
            Iguanas[i].transform.Rotate(0, angle, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null)
            {
                enemies[i] = Instantiate(enemyPrefab) as GameObject;
                enemies[i].transform.position = spawnPoint;
                float angle = Random.Range(0, 360);
                enemies[i].transform.Rotate(0, angle, 0);
                WanderingAI ai = enemies[i].GetComponent<WanderingAI>();
                ai.SetDifficulty(GetDifficulty());
            }
        }
    }
}
