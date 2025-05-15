using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DogMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerTransform;
    NavMeshAgent agent;
    Animator animator;
    public float barkTimer;
    [SerializeField] AudioClip[] barkSFX;
    float agroTimer;
    bool agrod;

    void Start()
    {
        agrod = false;
        checkAgro();
        barkTimer = Random.Range(0f, 20.0f);
        agent = GetComponent<NavMeshAgent>();
        
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        barkTimer -= Time.deltaTime;
        agroTimer -= Time.deltaTime;
        if (agroTimer <= 0f) {
            checkAgro();
        }
        if (agrod) { 
            agent.destination = playerTransform.position;
         }
        animator.SetFloat("Speed", agent.velocity.magnitude);
        if (barkTimer <= 0f)
        {
            bark();
        }
    }

    void checkAgro()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < 30f)
        {

            agrod =  true;
        }

        else 
        {
            agroTimer = 1f;
            agrod =  false;
        }

    }


    void bark()
    {
        sfxManager.instance.playRandSFX(barkSFX, transform.transform, 0.5f);
        barkTimer = Random.Range(0f, 20.0f);
    }
}
