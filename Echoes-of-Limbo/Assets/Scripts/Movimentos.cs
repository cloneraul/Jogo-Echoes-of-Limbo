using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float velocidade = 10f;
    public float forcaPulo = 10f;

    public bool noChao = false;
    public bool andando = false;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    private bool atacando = false;
    private bool curando = false;
    private bool morto = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("chao"))
            noChao = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("chao"))
            noChao = false;
    }

    void Update()
    {
        if (morto) return; // trava tudo quando morre

        andando = false;

        if (!atacando && !curando)
        {
            Movimento();
            Pulo();
        }

        Ataque();
        Cura();

        anim.SetBool("Andando", andando);
        anim.SetBool("Pulando", !noChao);
    }

    void Movimento()
    {
        float move = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(move * velocidade, rb.linearVelocity.y);

        if (move != 0)
        {
            sr.flipX = move < 0;
            if (noChao) andando = true;
        }
    }

    void Pulo()
    {
        if (Input.GetKeyDown(KeyCode.Space) && noChao)
        {
            rb.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
            noChao = false;
        }
    }

    void Ataque()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !atacando && noChao)
        {
            atacando = true;
            anim.SetTrigger("Atacar");
        }
    }

    void Cura()
    {
        if (Input.GetKeyDown(KeyCode.E) && !curando)
        {
            curando = true;
            anim.SetTrigger("Curar");
        }
    }

    public void FimAtaque() => atacando = false;
    public void FimCura() => curando = false;

    
    public void Morrer()
    {
        if (morto) return;

        morto = true;
        rb.linearVelocity = Vector2.zero;
        anim.SetTrigger("Morrer");
    }

    
    public void ReiniciarFase()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
