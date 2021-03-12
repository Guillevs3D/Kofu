//Cuando el player colisione con un objeto que tenga el tag ground, se pone la variable ground a true.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    //Variables globales Publicas
    [Header("Player Configuration")]
    public float speed = 10f;              //Velocidad Jugador
    public float jumpForce = 400f;        //Fuerza salto
    public bool airControl = false;      //El jugador si puede o no moverse en el salto
    public bool ground;                 //El jugador toca suelo
    public bool faceRight = true;       //Dirección que mira actualmente el jugador.

    //Vatiable globales Privadas
    private Rigidbody2D rigidBodyPlayer;
    private float climbTime;
    
    // Se puede accerder desde otros script como si fuese publico, no aparece en el inspector.
    internal Animator anima;            //Animator componente.

    private void Awake()
    {
        // Opciones de referencia.
        anima = GetComponent<Animator>();
        rigidBodyPlayer = GetComponent<Rigidbody2D>();
        // Le indicamos que toca el suelo nada mas empezar
        ground = true;
    }
   
    public void Move(float move, bool jump)
    {
        //Solo el control del player si esta en el suelo o en el aire
        if (ground || airControl)
        {
            // El parámetro de la animacion la velocidad se establece con el valor de la entrada horizontal.
            anima.SetFloat("Speed Player", Mathf.Abs(move));

            // movimiento player
            rigidBodyPlayer.velocity = new Vector2(move * speed, rigidBodyPlayer.velocity.y);

            // el jugador mueve hacia la derecha la cara
            if (move > 0 && !faceRight)
            {
                //Flip jugador
                Flip();
            }
            // el jugador mueve hacia la izquierda la cara
            else if (move < 0 && faceRight)
            {
                //Flip jugador
                Flip();
            }
            GameManager.instance.audioControl.walkPlayer.Play();
        }
        //El jugador salta
        if (ground == true && jump == true)
        {
            //fuerza vertical del jugador
            ground = false;
            anima.SetBool("Jump Player", true);
            rigidBodyPlayer.AddForce(new Vector2(0f, jumpForce));
        }
    }

    private void Flip()
    {
        //Cambia la forma en que el reproductor está etiquetado como enfadado
        faceRight = !faceRight;

        //Multiplica la escala local x del jugador por -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    //Colision con el suelo con Salto pared a pared
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag =="Ground")
        {
            ground = true;
            anima.SetBool("Jump Player", false);            
        }
    }
}
