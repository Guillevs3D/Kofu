using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;

    public class ControlPlayer : MonoBehaviour
    {

        //Variable Publica
        [Header("Game Object")]
        public GameObject bullet;
        public GameObject spawnBullet;
        public GameObject tmpBullet;
        [Header("Config")]
        public float attackChop = 15;
        public int countBullet = 10;
        public float nockBack = 5000;
        public float fireRate = 2;
        public bool checkReload = false;
        // Variables globales Privadas
        private bool jump;
        private MoveCharacter player;
        private float timeLastShoot = 0;

    private void Awake()
        {
            player = GetComponent<MoveCharacter>();
           tmpBullet.GetComponent<TextMeshProUGUI>().text = countBullet.ToString();
        }


    private void Update()
        {
            if (!jump)
            {
                // no se pierde las pulsaciones de las teclas
                jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
            if(CrossPlatformInputManager.GetButtonDown("Attack"))
            {
                Chop();
            }
            if (CrossPlatformInputManager.GetButtonDown("Shoot"))
            {
                Shoot();
            }

            //Tiempo que trancurre al se activa sonido de recarga
            float elapsed = (Time.time - timeLastShoot);
            if (countBullet >= 1 && elapsed > fireRate && checkReload == false)
            {
                checkReload = true;
                // Sonido de recarga            
                GameManager.instance.audioControl.reload.Play();
            }
         }
    private void FixedUpdate()
        {
            // leyendo inputs.
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            //Pasar todos los parametros del player control script
            player.Move(h,jump);
            jump = false;
        }

    private void Chop()
    {
        player.anima.SetTrigger("Chop Player");
        GameManager.instance.audioControl.attack1.Play();
    }

    private void Shoot()
    {
        //Tiempo que trancurre al disparar
        float elapsed = (Time.time - timeLastShoot);
        if (countBullet >=1 && elapsed > fireRate) 
        {
            if (player.faceRight == true)
            {
                Instantiate(bullet, spawnBullet.transform.position, transform.rotation);
            }
            else
            {
                Instantiate(bullet, spawnBullet.transform.position, spawnBullet.transform.rotation);
            }
           player.anima.SetTrigger("Shoot Player");
           GameManager.instance.audioControl.shoot.Play();
           //contador de balas y tiempo de disparo
           countBullet -= 1;
            Refresh();
           timeLastShoot = Time.time;
           checkReload = false;       
        }
    }

    public void Refresh()
    {

           tmpBullet.GetComponent<TextMeshProUGUI>().text = countBullet.ToString();
    }
  
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Cristal")
        {
            col.GetComponent<Cristal>().SoltarMineral();
        }
        if(col.tag =="Enemy")
        {
            col.gameObject.GetComponent<Enemy>().healt -= attackChop;
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(nockBack, 0));
        }
    }   
}
