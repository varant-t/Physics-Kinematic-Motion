using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scipt : MonoBehaviour
{
    public Rigidbody rigidbodyComponent;
    public TextMeshProUGUI totalScoreText;
    public Slider gameSlider;
    public Transform player;
    public int minValue = 0;
    public int maxValue = 100;
    public int maxStrengthValue = 100;
    public int medStrengthValue = 50;
    public int minStrengthValue = 25;
    public bool goingup = true;
    public int change = 1;
    public bool active = false;
    public int acceleration;
    public int deccelarte = -10;
    public bool gameOver = false;
    public int score = 0;
    public int totalScore = 0;
    private float zPos_0;
    private float zVel_0;
    private float elapsedTIme;
    public float decelarate = 0.5f;
    

    // Start is called before the first frame update
    void Start()
    {
       rigidbodyComponent = GetComponent<Rigidbody>();
       gameSlider.minValue = 0;
       gameSlider.maxValue = 100;
       totalScore = 0;
       score = 0;
    }

    // Update is called once per frame
    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.Space))
        {
            active = true;
            Invoke("Deactivate", 3);
            if(gameSlider.value >= 45 && gameSlider.value <= 55)
            {
                acceleration = 10;
                score = maxStrengthValue;
                totalScore += score;
                totalScoreText.text = "Score: " + Mathf.Round(totalScore * 100);
                Debug.Log("MaxStrength");
            }
            else if (gameSlider.value >= 25 && gameSlider.value <= 44 || gameSlider.value >= 56 && gameSlider.value <= 75)
            {
                acceleration = 3;
                score = medStrengthValue;
                totalScore += score;
                totalScoreText.text = "Score: " + Mathf.Round(totalScore * 100);
                Debug.Log("MediumStrength");
            }
            else if(gameSlider.value >= 0 && gameSlider.value <= 24 || gameSlider.value >= 76 && gameSlider.value <= 100)
            {
                acceleration = 1;
                score = medStrengthValue;
                totalScore += score;
                totalScoreText.text = "Score: " + Mathf.Round(totalScore * 100);
                Debug.Log("MinStrength");
            }
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            gameOver = false;
            active = false;
            player.position = new Vector3(0, 0.5f, -6);
            rigidbodyComponent.isKinematic = true;
        }
   
      {
        
     }
    }

    private void FixedUpdate()
    {
        if(!gameOver)
        {
            if (gameSlider.value == 0)
            {
                goingup = true;
            }
            else if (gameSlider.value == 100)
            {
                goingup = false;
            }

            if (!active)
            {
                if (goingup)
                {
                    gameSlider.value += change;
                }
                else
                {
                    gameSlider.value -= change;
                }
            }


            if (active)
            {
                Move();

            }

        }

        if (zPos_0 >= -2.5)
        {
            Decelerate();
        }

        if (zVel_0 <= -0.1)
        {
            rigidbodyComponent.isKinematic = false;
        }




    }
    private void Move()
    {
        //Vector3 movement = rigidbodyComponent.position;
        //movement.z = rigidbodyComponent.position.z + rigidbodyComponent.velocity.magnitude * Time.fixedDeltaTime + 0.5f * acceleration * Mathf.Pow(Time.fixedDeltaTime, 2);
        //rigidbodyComponent.MovePosition(movement);

        zVel_0 = rigidbodyComponent.velocity.z;
        zPos_0 = rigidbodyComponent.position.z;
        elapsedTIme = Time.fixedDeltaTime;

        Vector3 movement = rigidbodyComponent.position;
        movement.z = zPos_0 + (zVel_0 * elapsedTIme) + 0.5f * acceleration * elapsedTIme * elapsedTIme;
        rigidbodyComponent.MovePosition(movement);

    }

    public void  Decelerate()
    {
        zVel_0 = rigidbodyComponent.velocity.z;
        zPos_0 = rigidbodyComponent.position.z;
        elapsedTIme = Time.fixedDeltaTime;

        Vector3 movement = rigidbodyComponent.position;
        movement.z = zPos_0 + (zVel_0 * elapsedTIme) - 0.5f * decelarate * elapsedTIme * elapsedTIme;
        rigidbodyComponent.MovePosition(movement);

    }


    private void Deactivate()
    {
        active = false;
        gameOver = true;
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        rigidbodyComponent.isKinematic = false;
    }

   


}
