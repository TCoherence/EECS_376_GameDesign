using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Enemy : MonoBehaviour {

    public Rigidbody rigidbody;
    Text healthText;
    Text healthTextIns;
    public Color color;

    public ParticleSystem Boom;
    private readonly float DEADTIME = 12f;

    public int health;
    public float moveSpeed;


	// Use this for initialization
	void Start () {
        Boom = Resources.Load<ParticleSystem>("Prefabs\\Boom");
        healthText = Resources.Load<Text>("Prefabs\\Text");
        healthTextIns = Instantiate(healthText, transform.position, Quaternion.identity, GameObject.FindWithTag("UIRoot").transform);
        healthTextIns.text = health.ToString();

        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(-1, 0, 0) * moveSpeed;


        // destory them if time out
        Destroy(gameObject, DEADTIME);
        Destroy(healthTextIns, DEADTIME);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 srcPos = Camera.main.WorldToScreenPoint(transform.position);
        healthTextIns.transform.position = srcPos;
        healthTextIns.text = health.ToString();
        rigidbody.velocity = new Vector3(-1, 0, 0) * moveSpeed;

    }

    public void Behit (int power) {
        health -= power;
        if ( health <= 0) {
            health = 0;

            // Destory effect
            Instantiate(Boom, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(healthTextIns);
            PlayerController.UpgradePower();
            //if ( this.color == Color.yellow ) {
            //    Debug.Log("Some block is yellow...");
            //    PlayerController pc = GameObject.Find("Player").GetComponent<PlayerController>();
            //    pc.AddPower(10);
            //}
        }
    }

    public void Init(int health, float moveSpeed){
        this.health = health;
        this.moveSpeed = moveSpeed;
        //this.color = color;

    }

    public void OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.name == "Player" ) {
            PlayerController pc = other.GetComponent<PlayerController>();
            pc.Behit();
        }
    }
} 
