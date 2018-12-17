using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {
    public static int GOALPOINT = 1000;

    public Bullet Bullet;
    public float shootDelayTime;
    public Transform firePos;
    Rigidbody rigidbody;
    Vector3 targetPos;
    int power;

    public Text ResultText;
    public Text ScoreText;

    AudioSource audioSource;
    AudioClip audioClip;

    public static UnityAction UpgradePower;


	// Use this for initialization
    void Start () {
        ScoreText = GameObject.FindWithTag("UIRoot").transform.Find("Score").GetComponent<Text>();

        ResultText = GameObject.FindWithTag("UIRoot").transform.Find("Result").GetComponent<Text>();
        ResultText.text = "Your Goal is to get " + GOALPOINT + " points! Try your best!";
        audioSource = GetComponent<AudioSource>();
        UpgradePower = () =>
        {
            shootDelayTime -= 0.01f;
            if (shootDelayTime <= 0.2f)
            {
                shootDelayTime = 0.2f;
            }
            power += power / 10;
            //if (power >= 1000)
                //power = 1000;
        };

        rigidbody = GetComponent<Rigidbody>();
        targetPos = new Vector3(transform.position.x, Screen.height / 2, transform.position.z);
        power = 10;
        StartCoroutine(Fire());
	}
	
	// Update is called once per frame
	void Update () {
        if ( Input.GetMouseButton(0) ) {
            targetPos = Input.mousePosition;
        }

        MoveToTarget();

        int score = int.Parse(ScoreText.text);
        if ( score >= GOALPOINT) {
            ResultText.text = "You win! Game will be close in 5s...";
            Invoke("QuitGame", 5f);
        }
        
        //if ( Time.timeScale == 0 ) {
        //    ScoreText.fontSize = 12;
        //    ScoreText.text = "Press Space key to quit";
        //    if (Input.GetKeyDown(KeyCode.Space))
        //        QuitGame();
        //}

    }

    void MoveToTarget() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(targetPos);
        float mouseY = mousePos.y;
        float posY = transform.position.y;

        if (Mathf.Abs(mouseY - posY) < 0.1f)
            return;

        //if (mouseY > Camera.main.pixelHeight ) mouseY = Camera.main.pixelHeight - 2;
        //if (mouseY < Camera.main.pixelHeight ) mouseY = -Camera.main.pixelHeight + 2;
        //Debug.Log("Camera.main.pixelHeight = " + Camera.main.pixelHeight);

        Vector3 targetVec = new Vector3(transform.position.x, mouseY - posY, transform.position.z).normalized;
        rigidbody.MovePosition(transform.position + targetVec);
    }

    IEnumerator Fire() {
        while ( true ) {
            Bullet bullet = Instantiate(Bullet, firePos.position, Quaternion.identity);
            bullet.UpdatePower(power);
            audioSource.Play();
            yield return new WaitForSeconds(shootDelayTime);
        }
    }

    public void  Behit ( ) {
        Destroy(gameObject);

        Time.timeScale = 0.1f;
        //ScoreText.fontSize = 24;
        ResultText.text = "You lose, Closing game...";
        //Invoke("QuitGame", 5f);
        QuitGame();

    }
    private void QuitGame()
    {

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void AddPower(int power) {
        this.power += power;
    }
    public int getPower() {
        return this.power;
    }
}
