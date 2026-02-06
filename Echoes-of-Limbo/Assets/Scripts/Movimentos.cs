using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidade = 10f;
    public float forcaPulo = 10f;

    public bool noChao = false;
    public bool andando = false;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private bool atacando = false;
    private bool curando = false;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
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
        andando = false;

        
        if (!atacando)
        {
            Movimento();
            Pulo();
        }

        Ataque();
        Cura();

        _animator.SetBool("Andando", andando);
        _animator.SetBool("Pulando", !noChao);
    }

    void Movimento()
    {
        float move = Input.GetAxisRaw("Horizontal");

        _rigidbody2D.linearVelocity = new Vector2(
            move * velocidade,
            _rigidbody2D.linearVelocity.y
        );

        if (move != 0)
        {
            _spriteRenderer.flipX = move < 0;
            if (noChao) andando = true;
        }
    }

    void Pulo()
    {
        if (Input.GetKeyDown(KeyCode.Space) && noChao)
        {
            _rigidbody2D.AddForce(
                Vector2.up * forcaPulo,
                ForceMode2D.Impulse
            );
        }
    }

    void Ataque()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !atacando && noChao)
        {
            atacando = true;
            _animator.SetTrigger("Atacar");
        }
    }

    void Cura()
    {
        if (Input.GetKeyDown(KeyCode.E) && !curando)
        {
            curando = true;
            _animator.SetTrigger("Curar");

            
            Invoke(nameof(FimCura), 1.2f);
        }
    }

    public void FimAtaque()
    {
        atacando = false;
    }

    public void FimCura()
    {
        curando = false;
    }
}
