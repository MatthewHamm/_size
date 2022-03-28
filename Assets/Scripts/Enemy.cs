using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ParticleSystem deathParticles;
    public ParticleSystem damageHit;
    string seed;
    System.Random pseudoRandom;
    int rotate;
    public AudioSource deathSound;
    public AudioSource attackSound;
    bool playerInView;
    public Transform player;
    Rigidbody m_Rigidbody;
    bool hasTarget;
    Vector3 target;
    public GameObject pickUp;
    Vector3 direction;
    public int health = 2;
    public Animator m_animator;
    bool isDead=false;
    int deathCount = 0;
    void Start()
    {

        playerInView = false;
        m_Rigidbody = GetComponent<Rigidbody>();
        damageHit.Pause();
        deathParticles.Pause();
        
        seed = Time.time.ToString();
        pseudoRandom = new System.Random(seed.GetHashCode());

        hasTarget = false;
        direction = new Vector3();
    }

    


    void Update()
    {
       
        if ((target - transform.position).magnitude < 0.5)
        {
            print("arrived");
            hasTarget = false;
            //m_animator.SetBool("IsMoving", false);
        }
        if (!hasTarget && Mathf.Approximately(m_Rigidbody.velocity.y,0))
        {
            rotate = pseudoRandom.Next(-180, 180);

            direction = new Vector3(Mathf.Sin(rotate * Mathf.Deg2Rad), 0, Mathf.Cos(rotate * Mathf.Deg2Rad));

            if (m_Rigidbody.SweepTest(direction, out RaycastHit hit))
            {
                if (hit.distance > 2)
                {
                    hasTarget = true;

                    target = (hit.distance - 1) * direction + transform.position;
                }

                
            }
        }



        /*for(int i=-180; i < 180; ++i)
        {
            Vector3 direction = new  Vector3(Mathf.Sin(i* Mathf.Deg2Rad), 0, Mathf.Cos(i* Mathf.Deg2Rad));
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;
            if(Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.tag == "Player")
                {
                    print("here");
                    force +=  (transform.position- raycastHit.point).normalized * Mathf.Exp((- raycastHit.distance)/0.8f)* -raycastHit.distance*47f;
                }
                else
                {

                    force += ( transform.position- raycastHit.point).normalized *Mathf.Exp((1-raycastHit.distance)/0.8f)* (1 - raycastHit.distance)*3f;
                }
            }

        }
        if (m_Rigidbody.velocity.magnitude > 7)
        {
            force -= m_Rigidbody.velocity * 100;
        }*/
        //force +=new Vector3((float)(Mathf.Sin(rotate * Mathf.Deg2Rad) * mag), 0, (float)(Mathf.Cos(rotate * Mathf.Deg2Rad) * mag));

    }
    void FixedUpdate()
    {
        if (!isDead)
        {
            if (hasTarget)
            {
                Debug.DrawLine(transform.position, target, Color.red, Time.fixedDeltaTime);
                Vector3 lastPosition = transform.position;
                direction = (target - transform.position).normalized;
                direction.y = 0;
                // transform.Translate(velocity* Time.fixedDeltaTime);
                if ((direction - transform.forward).magnitude < Time.fixedDeltaTime * 5)
                {
                    m_animator.SetBool("IsMoving", true);
                    //transform.position=Vector3.MoveTowards(transform.position,target, Time.fixedDeltaTime * 5);
                    transform.Translate(Vector3.MoveTowards(transform.position, target, Time.fixedDeltaTime * 5) - transform.position, Space.World);
                    if ((transform.position - lastPosition).magnitude < Time.fixedDeltaTime * 4 && !playerInView)
                    {
                        hasTarget = false;
                    }
                }
                else
                {
                    Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, Time.fixedDeltaTime * 5, 0.0f);
                    transform.rotation = Quaternion.LookRotation(newDirection);
                }

            }



            if (playerInView)
            {
                target = player.position;

            }
        }
        else
        {
            deathCount++;
        }
        if (deathCount == 45) {
            GameObject.Instantiate(pickUp, transform.position, transform.rotation);
        }
        

        
        //else
        //{
        //print(player.position + m_Rigidbody.velocity);
        //transform.LookAt(player.position + m_Rigidbody.velocity);
        // }


    }
    public void playerLost()
    {
        playerInView = false;
    }
    public void playerSeen(Transform seenPlayer)
    {
        player = seenPlayer;
        playerInView = true;
    }
    void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            if (!attackSound.isPlaying)
            {
                attackSound.Play();
            }
            Player player1 = collision.gameObject.GetComponent<Player>();
            player1.GetComponent<Rigidbody>().AddForce(Vector3.forward * 50);
            m_Rigidbody.AddForce(Vector3.back * 50);
            player1.health--;
            m_animator.SetTrigger("Attack");
           
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            hasTarget = false;
            
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_animator.ResetTrigger("Attack");
        }
    }
    public void takeDamage() {
        health--;
        if (health <= 0 &&!isDead)
        {
            
            deathSound.Play();
            isDead = true;
            m_animator.SetTrigger("Death");
            deathParticles.Play();
            GameObject.Destroy(gameObject, 2);
            


            
        }
        else if(!isDead)
        {
            damageHit.Play();
            m_animator.SetTrigger("GetHit");
            //m_animator.ResetTrigger("GetHit");
        }
        
    }
}
