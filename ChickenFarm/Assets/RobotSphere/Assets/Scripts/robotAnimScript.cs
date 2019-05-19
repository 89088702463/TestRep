using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class robotAnimScript : MonoBehaviour {

	Vector3 rot = Vector3.zero;
	float rotSpeed = 40f;
	Animator anim;

	// Use this for initialization
	void Awake () {
		anim = gameObject.GetComponent<Animator> ();
		gameObject.transform.eulerAngles = rot;
	}

    public Animator animator; // Система анимаций
    [SerializeField] NavMeshAgent nav; // Система навигации
    public Transform[] points; // координаты точек куда персонаж будет ходить(колодец, миска, курица и тд)
    [HideInInspector]
    public int do_index; // индекс в массиве, какую точку будем достигать
    [HideInInspector]
    public bool go;
    [SerializeField] SYSTEM_APP systems; // Система игры

    public IEnumerator end(GameObject obj)
    {
        yield return new WaitForSeconds(10.3f);
        obj.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		CheckKey ();
		gameObject.transform.eulerAngles = rot;

        if (go)
        {
            nav.destination = points[do_index - 1].position; // Достигаем обьект в массиве points
                                                             // Когда подошел меньше 0.6 то включаем анимацию
            if (Vector3.Distance(transform.position, points[do_index - 1].position) < 0.6f)
            {
                animator.SetTrigger("talk"); // Анимация (ВЗЯТЬ)
                go = false;
                animator.SetBool("walk", false); // Анимация стойки на месте

                if (do_index == 3)
                    StartCoroutine(end(systems.kal)); // Выключаем какашки через 1.3 сек

                if (do_index == 4)
                    StartCoroutine(end(points[do_index - 1].gameObject)); // Выключаем яйцо через 1.3 сек
            }
        }

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
}
