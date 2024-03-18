using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed; // Velocidade de movimento do jogador
    new Rigidbody2D rigidbody; // Corpo do Jogador
    public GameObject bulletPreFab; // Armazena o pre fab da bala
    public float bulletSpeed; // Velocidade da bala
    private float lastFire; // Tempo da última bola
    public float fireDelay; // Tempo de delay dos tiros
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>(); // Pega o componente rigibody e armazena na variavel
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // Armazena o eixo horizontal baseado no Input
        float vertical = Input.GetAxis("Vertical"); // Armazena o eixo vertical baseado no Input
        float shootHor = Input.GetAxis("ShootHorizontal"); // Armazena o eixo de tiro horizontal baseado no Input
        float shootVer = Input.GetAxis("ShootVertical"); // Armazena o eixo de tiro vertical baseado no Input

        if ((shootHor != 0 || shootVer != 0) && Time.time > lastFire + fireDelay) // Verifica se qualquer um dos eixos de tiro esta ativo
        {
            Shoot(shootHor, shootVer); // Chama a função de atirar
            lastFire = Time.time; // Atribui o time atual a variável
        }

        rigidbody.velocity = new Vector2(horizontal * speed, vertical * speed); // Deixa a velocidade do jogador baseado no eixo pressionado multiplicado pela velocidade definida
    }

    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPreFab, transform.position, transform.rotation); // Intancia a bala no local atual do jogador
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0; // Adiciona um RigidBody a bala e registra a gravidade dele para 0
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2((x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed, (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed); // Define a "velocidade" da bala baseada nas variaveis de velocidade e no eixo pressionado
    }
}
