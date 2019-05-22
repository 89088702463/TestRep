using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class robotAnimScript : MonoBehaviour {

    [SerializeField] SYSTEM_APP systems; // Система игры
    Animator anim; // Система анимаций
    [SerializeField] NavMeshAgent nav; // Система навигации

    [HideInInspector]
    public int do_index; // Индекс в массиве, какую точку будем достигать
    [HideInInspector]
    public bool go; // Движение


    Vector3 rot = Vector3.zero;
	float rotSpeed = 40f;


	// Use this for initialization
	void Awake () {
		anim = gameObject.GetComponent<Animator> ();
		gameObject.transform.eulerAngles = rot;
	}

    public Transform[] points; // координаты точек куда двигаться роботу


    void Update () {

        if (go)
        {
            print(do_index);
            anim.SetBool("Roll_Anim", true);
            nav.destination = points[do_index - 1].position; // Достигаем обьект в массиве points
            if (Vector3.Distance(transform.position, points[do_index - 1].position) < 0.6f) // Если дистанция между обьектом и персонажем меньше 0.6 то включаем анимацию
            {
                anim.SetTrigger("work"); // Анимация (ВЗЯТЬ)
                go = false;
                anim.SetBool("walk", false); // Анимация стойки на месте

                if (do_index == 3)
                    StartCoroutine(end1(systems.kal)); // Выключаем какашки через 1.3 сек

                if (do_index == 4)
                    StartCoroutine(end1(points[do_index - 1].gameObject)); // Выключаем яйцо через 1.3 сек
            }
        }

        //CheckKey ();
		//gameObject.transform.eulerAngles = rot;
	}

	void CheckKey(){
		// Walk
		if (Input.GetKey (KeyCode.W)) {
			anim.SetBool ("Walk_Anim", true);
		} 
		else if (Input.GetKeyUp (KeyCode.W)) {
			anim.SetBool ("Walk_Anim", false);
		}

		// Rotate Left
		if(Input.GetKey(KeyCode.A)){
			rot[1] -= rotSpeed * Time.fixedDeltaTime;
		}

		// Rotate Right
		if(Input.GetKey(KeyCode.D)){
			rot[1] += rotSpeed * Time.fixedDeltaTime;
		}

		// Roll
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (anim.GetBool ("Roll_Anim")) {
				anim.SetBool ("Roll_Anim", false);
			}
			else {
				anim.SetBool ("Roll_Anim", true);
			}
		} 

		// Close
		if(Input.GetKeyDown(KeyCode.LeftControl)){
			if (!anim.GetBool ("Open_Anim")) {
				anim.SetBool ("Open_Anim", true);
			} 
			else {
				anim.SetBool ("Open_Anim", false);
			}
		}
	}

    public IEnumerator end1(GameObject obj) // Прекращение существования объекта
    {
        yield return new WaitForSeconds(10.3f);
        obj.SetActive(false);
    }
}
