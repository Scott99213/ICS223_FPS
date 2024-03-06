using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
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
            }
        }
    }
}
