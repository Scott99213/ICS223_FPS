using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{

    private float enemySpeed = 3.0f;
    private float obstacleRange = 5.0f;
    private float sphereRadius = 0.75f;

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
                if (hit.distance < obstacleRange)
                {
                    float turnAngle = Random.Range(-110, 110);
                    transform.Rotate(Vector3.up * turnAngle);
                }
            }
        } 
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
