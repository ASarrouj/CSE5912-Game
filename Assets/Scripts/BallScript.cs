using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
    // Start is called before the first frame update
    bool startLeft;
    GameObject player1,player2;
    int score1,score2;
    private float speed;
    public Text player1Text;
    public Text player2Text;
    Vector3 vel;
    private AudioSource source;
    public AudioClip bounce;
    private bool pause;
    public Light spot;

    void Start()
    {
        speed = 0.2f;
        Respawn();
        score1 =0;
        score2=0;
        SetCountText(player1Text,score1);
        SetCountText(player2Text,score2);
        player1=GameObject.Find("Player1");
        player2=GameObject.Find("Player2");
        source = gameObject.GetComponent<AudioSource>();
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        spot.transform.position=transform.position+new Vector3(0,10f,0);
        if (!pause)
        {
            if (transform.position.x < -10f)
            {
                score2++;
                SetCountText(player2Text, score2);
                Respawn();
            }
            else if (transform.position.x > 10f)
            {
                score1++;
                SetCountText(player1Text, score1);
                Respawn();
            }
            else if (transform.position.x > 9.2f || transform.position.x < -9.2f)
            {
                checkCollision();
            }
            else if ((transform.position.z > 5f && vel.z > 0) || (transform.position.z < -5f && vel.z < 0))
            {
                vel = new Vector3(vel.x, 0, -vel.z);
                source.PlayOneShot(bounce);
            }
            transform.Translate(vel.normalized * speed);
            CheckInput();
            Debug.Log(speed);
        }
    }
    void SetCountText(Text change,int score)
    {
        change.text="Score: "+score;
    }
    void checkCollision()
    {
         if(transform.position.x > 9.2f && vel.x > 0)
         {
             float dist=player2.transform.position.z-transform.position.z;
             if(dist < .75f && dist > -.75f)
             {
                float ballX=-.282f*Mathf.Cos((dist*100)*Mathf.PI/180);
                float ballZ=.282f*-Mathf.Sin((dist*100)*Mathf.PI/180);
                vel=new Vector3(ballX,0,ballZ);
                source.PlayOneShot(bounce);
             }
         }
        else if(transform.position.x < -9.2f && vel.x < 0)
         {
             float dist=player1.transform.position.z-transform.position.z;
             if(dist < .75f && dist > -.75f)
             {
                float ballX=.282f*Mathf.Cos((dist*100)*Mathf.PI/180);
                float ballZ=.282f*-Mathf.Sin((dist*100)*Mathf.PI/180);
                vel=new Vector3(ballX,0,ballZ);
                source.PlayOneShot(bounce);
             }
         }
    }

    void Respawn()
    {
        vel = Vector3.zero;
        transform.position = new Vector3(0f, 0.5f, 0f);
        CancelInvoke("SetVelocity");
        Invoke("SetVelocity", 1.5f);
    }

    void SetVelocity()
    {
        float x;
        if (Random.value < 0.5f)
            x = -1;
        else
            x = 1;
        vel = new Vector3(x, 0, Random.Range(0.0f, 1.0f));
    }
    
    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && speed < 0.3f)
        {
            speed += 0.05f;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && speed > 0.1f)
        {
            speed -= 0.05f;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }

    public void Halt()
    {
        pause = true;
    }

    public void Resume()
    {
        pause = false;
    }
}
