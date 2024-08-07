using System.Collections;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Wander,
    Follow,
    Attack,
};

public enum EnemyType
{
    Melee,
    Ranged,
}

public class EnemyController : MonoBehaviour
{
    GameObject player;
    public EnemyState currState = EnemyState.Idle;
    public EnemyType enemyType;
    public float range;
    public float speed;
    public float attackRange;
    public float bulletSpeed;
    public float coolDown;
    public bool notInRoom = false;
    private bool chooseDir = false;
    private bool coolDownAttack = false;
    private Vector3 randomDir;
    public GameObject bulletPreFab;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        switch (currState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Wander:
                Wander();
                break;

            case EnemyState.Follow:
                Follow();
                break;

            case EnemyState.Attack:
                Attack();
                break;
        }

        if (!notInRoom)
        {
            if (IsPlayerInRange(range))
            {
                currState = EnemyState.Follow;
            }
            else if (!IsPlayerInRange(range))
            {
                currState = EnemyState.Wander;
            }

            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
            {
                currState = EnemyState.Attack;
            }
        }
        else
        {
            currState = EnemyState.Idle;
        }
    }

    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        randomDir = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDir = false;
    }

    void Wander()
    {
        if (!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }

        transform.position += speed * Time.deltaTime * -transform.right;
        if (IsPlayerInRange(range))
        {
            currState = EnemyState.Follow;
        }

    }

    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void Attack()
    {
        if (!coolDownAttack)
        {
            switch (enemyType)
            {
                case EnemyType.Melee:
                    GameController.DamagePlayer(1);
                    StartCoroutine(Cooldown());
                    break;
                case EnemyType.Ranged:
                    GameObject bullet = Instantiate(bulletPreFab, transform.position, Quaternion.identity);
                    bullet.GetComponent<BulletController>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<BulletController>().isEnemyBullet = true;
                    StartCoroutine(Cooldown());
                    break;
            }
        }
    }

    private IEnumerator Cooldown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void Idle()
    {

    }

}
