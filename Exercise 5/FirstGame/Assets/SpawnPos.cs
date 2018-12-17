using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPos : MonoBehaviour {
    private static int LEVELMIN = 3;
    private static int LEVELMAX = 6;


    public Enemy Enemy;
    public BonusEnemy BonusEnemy;
    public float DelayTime;
    public float offset;
    int incVal;
    int inc = 20;
    float moveSpeed = 4;
    static int ENEMY_NUM = 3;

	// Use this for initialization
	void Start () {
        incVal = 5;
        StartCoroutine(CreateEnemy());
	}

    IEnumerator CreateEnemy()
    {
        while (true)
        {
            //Color color = Color.red;


            //List<Enemy> enemies = new List<Enemy>();

            moveSpeed += 0.5f;
            if ( moveSpeed >= 12 ) {
                moveSpeed = 12;
            }

            Enemy en = Instantiate(Enemy, transform.position, Quaternion.identity);
            en.Init(incVal * Random.Range(LEVELMIN, LEVELMAX), moveSpeed);

            float offsetY = FindObjectOfType<Enemy>().GetComponent<Renderer>().bounds.size.y;
            for (int i = 1; i <= ENEMY_NUM; i++)
            {
                //Random.InitState((int)Time.time);
                int bonus = Random.Range(0, 100);
                int healthVal = incVal * Random.Range(LEVELMIN, LEVELMAX);
                //Debug.Log(bonus);
                if ( bonus <= 20 ) {
                    BonusEnemy be = Instantiate(BonusEnemy,
                                        new Vector3(transform.position.x, transform.position.y + offsetY * i + offset * i, transform.position.z),
                                        Quaternion.identity);
                    be.Init(healthVal, moveSpeed);
                }
                else {
                    Enemy enemy = Instantiate(Enemy,
                                        new Vector3(transform.position.x, transform.position.y + offsetY * i + offset * i, transform.position.z),
                                        Quaternion.identity);
                    enemy.Init(healthVal, moveSpeed);
                }
                    

            }
            for (int i = 1; i <= ENEMY_NUM; i++ )  {
                //Random.InitState((int)Time.time);
                int bonus = Random.Range(0, 100);
                int healthVal = incVal * Random.Range(LEVELMIN, LEVELMAX);
                if (bonus <= 20)
                {
                    BonusEnemy be = Instantiate(BonusEnemy,
                                        new Vector3(transform.position.x, transform.position.y - offsetY * i - offset * i, transform.position.z),
                                        Quaternion.identity);
                    be.Init(healthVal, moveSpeed);

                }
                else
                {
                    Enemy enemy = Instantiate(Enemy,
                                        new Vector3(transform.position.x, transform.position.y - offsetY * i - offset * i, transform.position.z),
                                        Quaternion.identity);
                    enemy.Init(healthVal, moveSpeed);

                }
            }
            // update enemies attributes 


            //for (int i = 0; i < enemies.Count; i++ ) {
            //    int healthVal = Random.Range(1, incVal + 1);
            //}
            PlayerController pc = GameObject.Find("Player").GetComponent<PlayerController>();
            incVal += pc.getPower();

            yield return new WaitForSeconds(DelayTime);
        }
    }

    // Update is called once per frame
    void Update () {
	}



}
