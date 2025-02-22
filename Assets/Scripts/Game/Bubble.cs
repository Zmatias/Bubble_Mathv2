using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageNumbersPro;

public class Bubble : MonoBehaviour
{
    public ParticleSystem heartParticle;
    public ParticleSystem heart2Particle;
    public ParticleSystem shockwaveParticle;

    public int bubbleColor;

    private Vector3 mMovementDirection = Vector3.zero;
    // private SpriteRenderer mSpriteRenderer = null;
    private Coroutine mCurrentChanger = null;

    public DamageNumber DamagePrefab;

    public Rigidbody rb;

    public bool isLeft;

    public GameObject floatingEffectPrefab; // Assign in the Inspector


    public void SpawnFloatingEffect(int rawScore,bool isTeam1)
    {
        // Instantiate the effect at the bubble's position
        GameObject effect = Instantiate(floatingEffectPrefab, transform.position, Quaternion.identity);

        // Get the FloatingEffect script
        FloatingEffect floatingEffect = effect.GetComponent<FloatingEffect>();
        floatingEffect.RawAmount=rawScore;
        floatingEffect.isTeam1=isTeam1;

        int multipleScore=0;
        if(isTeam1)
        {
            multipleScore=GameManager.instance.multipleScoreT1;
        }
        else
        {
            multipleScore=GameManager.instance.multipleScoreT2;
        }
        DamageNumber damageNumber = DamagePrefab.Spawn(transform.localPosition, multipleScore*GameManager.instance.MainAddScore);

        // Assign the correct UI target based on the bubble's side
        floatingEffect.targetUI = isLeft ? BubbleManager.Instance.leftUIElement : BubbleManager.Instance.rightUIElement;

    }


    private void Awake()
    {
        // mSpriteRenderer = GetComponent<SpriteRenderer>();
        rb.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        mCurrentChanger = StartCoroutine(DirectionChanger());
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Stopper") || other.gameObject.CompareTag("Bubble") )
            {
                Vector3 normal = other.contacts[0].normal; // Get collision normal
                Vector3 reflectedDirection = Vector3.Reflect(mMovementDirection.normalized, normal); // Normalize before reflection
                reflectedDirection.z=0;

                // Ensure the reflected direction is valid
                if (reflectedDirection.magnitude < 0.1f)
                {
                    reflectedDirection = GetRandomDirection();
                }

                // ✅ Ensure the new movement direction maintains the same speed
                mMovementDirection = reflectedDirection.normalized * 1.0f; // Keep the movement magnitude constant

                // ✅ Ensure we don't get stuck by moving slightly away from the obstacle
                rb.MovePosition(rb.position + normal * 0.1f);

            }
            else
            {
                transform.position = BubbleManager.Instance.GetNextSpawnPosition(isLeft);
                transform.localScale = new Vector3(1,1,1);
                BubbleManager.Instance.SetABubbleColor(this);                
            } 
             
    }

    public void ChangeColor(Color TeamColor)
    {

    }

    /* private void Update()
    {
        // Movement
        transform.position += mMovementDirection * Time.deltaTime * 1.4f;

        // Rotation
        transform.Rotate(Vector3.forward * Time.deltaTime * mMovementDirection.x * 20, Space.Self);
    } */

     private Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), 0, 0).normalized;
    }

    void FixedUpdate()
    {
        // ✅ Ensure movement direction is normalized
        Vector3 normalizedDirection = mMovementDirection.normalized;

        // ✅ Define a constant speed (adjust as needed)
        float movementSpeed = 1.4f; 

        // ✅ Override any external changes and enforce a constant movement speed
        mMovementDirection = normalizedDirection * movementSpeed;

        // ✅ Ensure movement is applied only in X-Y plane
        mMovementDirection.z = 0;

        // ✅ Multiply speed by delta time
        Vector3 moveAmount = mMovementDirection * Time.fixedDeltaTime;

        // ✅ Move the Rigidbody while ensuring Z stays at 0
        Vector3 newPosition = rb.position + moveAmount;
        newPosition.z = 0; // Force final position Z to 0

        rb.MovePosition(newPosition);

        // ✅ Apply Rotation (rotate only around Y-axis)
        float rotationSpeed = 20f;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(0, normalizedDirection.x * rotationSpeed * Time.fixedDeltaTime, 0)));
    }




    public IEnumerator Pop()
    {
        // mSpriteRenderer.sprite = mPopSprite;

        StopCoroutine(mCurrentChanger);
        mMovementDirection = Vector3.zero;

        heartParticle.Play();
        heart2Particle.Play();
        shockwaveParticle.Play();
        GetComponent<AudioSource>().Play();

        string LeftOrRigh="";

        if(BubbleManager.Instance.leftBubbles.Contains(this))
        {
            LeftOrRigh="Right";
        }
        else
        {
            LeftOrRigh="Left";
        }

        GameManager.instance.CheckAndScore(bubbleColor,this,LeftOrRigh);        

        yield return new WaitForSeconds(0.5f);

        transform.position = BubbleManager.Instance.GetPlanePosition();
        BubbleManager.Instance.SetABubbleColor(this);

        // mSpriteRenderer.sprite = mBubbleSprite;
        mCurrentChanger = StartCoroutine(DirectionChanger());

        
    }

    private IEnumerator DirectionChanger()
    {
        while (gameObject.activeSelf)
        {
            // Generate a random direction with uniform magnitude
            Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(0f, 1f)).normalized;

            // Ensure the object moves at a fixed speed
            mMovementDirection = randomDirection * 1.0f; // 1.0f ensures a fixed magnitude

            yield return new WaitForSeconds(5.0f);
        }
    }

}


