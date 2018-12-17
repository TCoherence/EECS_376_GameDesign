using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bullet : MonoBehaviour {

    Rigidbody rigidbody ;
    public float moveSpeed;
    public int power;
    public Text ScoreText;
    AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        ScoreText = GameObject.FindWithTag("UIRoot").transform.Find("Score").GetComponent<Text>();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(1, 0, 0) * moveSpeed;
        Destroy(gameObject, 5f);
	}   
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider collider)
    {
        //if (collider.gameObject.tag == "Enemy" ) {
        audioSource.Play();
        //}
        Debug.Log("collision.go.tag = " + collider.gameObject.tag);
        Enemy enemy = collider.GetComponent<Enemy>();
        BonusEnemy be = collider.GetComponent<BonusEnemy>();
        int score = int.Parse(ScoreText.text);


        if ( enemy != null ) {
            //if ( power > enemy.health ) {
            //    score += enemy.health;
            //}
            //else {
            //    score += power;
            //}
            score++;
            enemy.Behit(power);

        }

        if ( be != null ) {
            //if (power > be.health)
            //{
            //    score += be.health;
            //}
            //else
            //{
            //    score += power;
            //}
            score += 2;
            be.Behit(power);
        }
        ScoreText.text = score.ToString();

        Destroy(gameObject);
    }
    public void UpdatePower(int power) {
        this.power = power;
    }
}
