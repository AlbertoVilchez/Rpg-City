using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb2;
    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        rb2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
      
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -10.5f, 10.5f), Mathf.Clamp(transform.position.y, -4.5f, 4.5f)); // limite del mapa
    }
    private void FixedUpdate()
    {
        Movi();
    }
    void Movi()
    {
        Vector2 mo = new Vector2(Input.GetAxis("Horizontal"),0); 
        rb2.MovePosition(rb2.position + mo * 6.2f * Time.fixedDeltaTime);// movimiento del jugador

        //Animacion del jugador siempre que se este moviendo
        if(mo != Vector2.zero)
        {
            anim.SetFloat("movX", mo.x);
            //anim.SetFloat("movY", mo.y);
            anim.SetBool("walking", true);

            if (mo.x > 0f)// Flipea el objeto segun el movimiento en x
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;

            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }

        }
        else
        {
            anim.SetBool("walking", false);
        }

       
    }
    


}
