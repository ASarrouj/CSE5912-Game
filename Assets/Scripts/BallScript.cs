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
    public Text player1Text;
    public Text player2Text;
    public Light spot;
    Vector3 vel;
    void Start()
    {
        startLeft=true;
        vel=new Vector3(-0.2f,0,-.2f);
        score1=0;
        score2=0;
        SetCountText(player1Text,score1);
        SetCountText(player2Text,score2);
        player1=GameObject.Find("Player1");
        player2=GameObject.Find("Player2");
    }

    // Update is called once per frame
    void Update()
    {
        spot.transform.position=transform.position+new Vector3(0,30,0);
        if(transform.position.x<-10f)
        {
            score2++;
            SetCountText(player2Text,score2);
            transform.position=new Vector3(8f,0.5f,0f);
            vel=new Vector3(-0.282f,0,0f);
            player1.transform.position=new Vector3(player1.transform.position.x,1,0);
            player2.transform.position=new Vector3(player2.transform.position.x,1,0);
        }
        else if(transform.position.x>10f)
        {
            score1++;
            SetCountText(player1Text,score1);
            transform.position=new Vector3(-8f,0.5f,0f);
            vel=new Vector3(0.282f,0,0);
            player1.transform.position=new Vector3(player1.transform.position.x,1,0);
            player2.transform.position=new Vector3(player2.transform.position.x,1,0);
        }
       else if(transform.position.x>9.2f||transform.position.x<-9.2f)
        {
            checkCollision();
        }
        else if(transform.position.z>5f||transform.position.z<-5f)
        {
            vel=new Vector3(vel.x,0,-vel.z);
        }
        if(transform.position.z>5.5f)
        {
           transform.position=new Vector3(transform.position.x,transform.position.y,5.0f); 
        }
        else if(transform.position.z<-5.5f)
        {
            transform.position=new Vector3(transform.position.x,transform.position.y,-5.0f); 
        }
        transform.Translate(vel);
        
    }
    void SetCountText(Text change,int score)
    {
        change.text="Score: "+score;
    }
    void checkCollision()
    {
         if(transform.position.x>9.2f)
         {
             float dist=player2.transform.position.z-transform.position.z;
             if(dist<.75f&&dist>-.75f)
             {
                 float ballX=-.282f*Mathf.Cos((dist*100)*Mathf.PI/180);
                 float ballZ=.282f*-Mathf.Sin((dist*100)*Mathf.PI/180);
                 vel=new Vector3(ballX,0,ballZ);
             }
         }
        else if(transform.position.x<-9.2f)
         {
             float dist=player1.transform.position.z-transform.position.z;
            Debug.Log(dist);
             if(dist<.75f&&dist>-.75f)
             {
                 float ballX=.282f*Mathf.Cos((dist*100)*Mathf.PI/180);
                 float ballZ=.282f*-Mathf.Sin((dist*100)*Mathf.PI/180);
                 vel=new Vector3(ballX,0,ballZ);
             }
         }
    }
}
