using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soldier : MonoBehaviour
{
    [Header("Claculo")]
    public float visioRadius;
    public float attackRadius;
    float time = 2;
    private Vector2 Ptext;
    private bool TiFrase;
    public ScriptObjectEnemy ScriptTexto;
    public Text Texto;
    public RawImage Img;
    [Header("Animador")]
    private Animator anim;
    [Header("Objetos")]
    private GameObject player;
    private Rigidbody2D rbEne;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rbEne = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        Ptext = Img.transform.position; //Recoge la posicion inicial del texto
    }

    // Update is called once per frame
    void Update()
    {
        // flip segun la posicion del player
        if (player.transform.position.x > transform.position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(player.transform.position.x < transform.position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (TiFrase)
        {
            time -= Time.deltaTime;
        }

        
    }
    private void FixedUpdate()
    {
        Radios(); //Metodo del radio enemy

    }
    void Radios()
    {
        float distancia = Vector3.Distance(player.transform.position, transform.position); // Flotante con la distancia hacia el jugador

        if (distancia < visioRadius && distancia > attackRadius)
        {
            //Apunta el ray hacia el player y lo debugea       
            Vector3 forwad = transform.TransformDirection(player.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position + forwad, forwad, visioRadius); // Si entra el player en el radio crea el ray

            if (hit.collider != null) // Comprueba que el el player quien entra en el radio
            {
                if (hit.collider.tag == "Player")
                {
                    // Calcula la distancia del radio cuando entra el jugador
                    rbEne.MovePosition(transform.position + forwad * 4.5f * Time.deltaTime);
                    anim.SetBool("walking", true);


                }


            }  
        }

        else{
            anim.SetBool("walking", false);
        }
        // Si esta en el radio de ataque, salta un texto
        if (distancia < attackRadius)
        {              
                anim.SetBool("walking", false);
                TiFrase = true;
                Img.transform.position = new Vector2(transform.position.x, transform.position.y + 1.4f);

            if (time > 1) Texto.text = ScriptTexto.frase1;
            else Texto.text = ScriptTexto.frase2;

        }
        else
        {
            Img.transform.position = Ptext;
            TiFrase = false;
            time = 2;
        }
       



    }
    //Dibuja el gizmo del Area
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visioRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
