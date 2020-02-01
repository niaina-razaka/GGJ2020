using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;
    public float lifeTime;
    public float distance;
    public int damage;    
    public LayerMask target;
    GameObject player;
    GameObject weapon;
    private int Damage;
    public GameObject effectDestroy;
    private GameObject audioManager;
    private CameraShaker cam;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        weapon = GameObject.FindGameObjectWithTag("Weapon");
      //  audioManager = GameObject.FindGameObjectWithTag("AudioManager");
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShaker>();
        Invoke("DestroyProjectile", lifeTime);
        Instantiate(effectDestroy, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, distance, target);
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        if (hit.collider != null)
        {

            if (hit.collider.CompareTag("Enemy") )
            {
                print("dead");
             //   hit.collider.gameObject.GetComponent<EnemyController>().ReceiveDamage(this.damage);
                DestroyProjectile();
            }
            if (hit.collider.CompareTag("Platform"))
            {
                print("plat");
               
                DestroyProjectile();
            }

            //if (hit.collider.CompareTag("Boss")&& gameObject.tag == "Boule")
            //{
            //    print("boss");
            //  //  hit.collider.gameObject.GetComponent<BossOneController>().ReceiveDamage(this.damage);
            //    DestroyProjectile();
            //}
            if (hit.collider.gameObject.layer==8)
            {
                DestroyProjectile();
               
            }
           
            //if (hit.collider.CompareTag("Player") && gameObject.tag == "ProjectileEnemy")
            //{
            //    print("alive");
            //    //hit.collider.gameObject.GetComponent<Player>().TakeDamage(20);
            //}

        }
        if (Input.GetMouseButtonDown(1))
        {
            //if (player!=null &&  gameObject.tag == "Hat" && weapon.gameObject.GetComponent<WeaponController>().nbHat==1)
            //{
            //    Time.timeScale = 1f;
            //    weapon.gameObject.GetComponent<WeaponController>().nbHat = 0;
            //    player.transform.position = transform.position;
            //  /  int jumpCount= player.gameObject.GetComponent<PlayerController>().JumpCount;
            //    if (jumpCount == 1)
            //    {
            //  //      player.gameObject.GetComponent<PlayerController>().JumpCount = 0;
                   
            //    }
            ////    player.gameObject.GetComponent<PlayerController>().CloudEffect();
            //    //weapon.gameObject.GetComponent<WeaponController>().canHat = true;
               
            //    //Destroy(gameObject);
            //}
            if (player != null)
            {

            }
        }


    }
    
    void Fire()
    {
    }
    void DestroyProjectile()
    {
       
        //audioManager.GetComponent<AudioManager>().Play("Choc");
        //cam.Shake();
        print("Destroy");
        Destroy(gameObject);
       // Destroy(effectDestroy);
    }
}
