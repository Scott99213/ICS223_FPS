using static PlayerController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] private GameObject laserbeamPrefab;
    private GameObject laserbeam;

    public float fireRate = 2.0f;
    private float nextFire = 0.0f;

    private float enemySpeed = 2.0f;
    private float obstacleRange = 5.0f;
    private float sphereRadius = 0.75f;

    private float baseSpeed = 0.25f;
    float difficultySpeedDelta = 0.3f;

    public enum EnemyStates { alive, dead };
    private EnemyStates enemyState;
    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyStates.alive;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState == EnemyStates.alive)
        {
            Vector3 movement = Vector3.forward * enemySpeed * Time.deltaTime;
            transform.Translate(movement);
            // generate Ray
            Ray ray = new Ray(transform.position, transform.forward);
            // Spherecast and determine if Enemy needs to turn
            RaycastHit hit;
            if (Physics.SphereCast(ray, sphereRadius, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerController>())
                {
                    if (laserbeam == null && Time.time > nextFire)
                    {
                        nextFire = Time.time + fireRate;
                        laserbeam = Instantiate(laserbeamPrefab) as GameObject;
                        laserbeam.transform.position = transform.TransformPoint(0, 1.5f, 1.5f);
                        laserbeam.transform.rotation = transform.rotation;
                    }
                }
                else if (hit.distance < obstacleRange)
                {
                    float turnAngle = Random.Range(-110, 110);
                    transform.Rotate(Vector3.up * turnAngle);
                }
            }
        } 
    }

    public void SetDifficulty(int newDifficulty) 
    {
        Debug.Log("WanderingAI.SetDifficulty(" + newDifficulty + ")");
        enemySpeed = baseSpeed + (newDifficulty * difficultySpeedDelta);
    }

    public void ChangeState(EnemyStates state)
    {
        enemyState = state;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector3 rangeTest = transform.position + transform.forward * obstacleRange;

        Debug.DrawLine(transform.position, rangeTest);

        Gizmos.DrawWireSphere(rangeTest, sphereRadius);
    }
}
