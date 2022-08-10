using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bullet : MonoBehaviour
{
    public ParticleSystem pS;
    public GameObject spark;
    PlayerController pC;
    bool isMouseOverUIElement;
    

    public Transform damageText;
    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();

    void Start()
    {
        pS = GetComponent<ParticleSystem>();        
        
    }
    void Update()
    {
        isMouseOverUIElement = EventSystem.current.currentSelectedGameObject == null;

        if(pS.tag == "effect"){
            pS.Play();
        }
        if(Input.GetMouseButtonDown(0) && isMouseOverUIElement){
            AudioManager.soundPlay("hit");
            pS.Play();
        }
        Destroy(GameObject.Find("Spark(Clone)"),1);    
    }

    void OnParticleCollision(GameObject other)
    {
        if(other.tag.Equals("Enemy")){
            int events = pS.GetCollisionEvents(other,colEvents);
            for (int i = 0; i < events; i++)
            {
                Instantiate(spark, colEvents[i].intersection, Quaternion.LookRotation(colEvents[i].normal));                                           
            }
            
            other.GetComponent<Enemy>().enemyHealth -= PlayerController.Instance.damage;
            other.GetComponent<Enemy>().slider.value =  other.GetComponent<Enemy>().enemyHealth;
            Instantiate(damageText,other.transform.position,Quaternion.identity).GetComponent<TextMesh>().text = "10";
        }
       
    }
    
}
