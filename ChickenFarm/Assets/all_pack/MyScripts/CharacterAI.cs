using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAI : MonoBehaviour {

    [SerializeField] SYSTEM_APP systems; // Система игры
    public Animator animator; // Система анимаций
    [SerializeField] NavMeshAgent nav; // Система навигации

    [HideInInspector]
    public int do_index; // Индекс в массиве, какую точку будем достигать
    [HideInInspector]
    public  bool go; // Движение


    public IEnumerator end(GameObject obj)
    {
        yield return new WaitForSeconds(10.3f);
        obj.SetActive(false);
    }

    public Transform[] points; // координаты точек куда персонаж будет ходить (колодец, миска, курица и тд)

    void Update () { // Обработка на каждый кадр
        //print(do_index);
		if(go)
        {


            nav.destination = points[do_index - 1].position; // Достигаем обьект в массиве points
            if (Vector3.Distance(transform.position, points[do_index - 1].position) < 0.6f) // Если дистанция между обьектом и персонажем меньше 0.6 то включаем анимацию
            {
                animator.SetTrigger("work"); // Анимация
                go = false;
                animator.SetBool("walk", false); // Анимация покоя

                if (do_index == 1)
                    StartCoroutine(end(systems.WaterLamp)); // Выключаем синий свет

                if (do_index == 3)
                    StartCoroutine(end(systems.kal)); // Выключаем clear через 1.3 сек

                if (do_index == 4)
                    StartCoroutine(end(points[do_index - 1].gameObject)); // Выключаем яйцо через 1.3 сек

            }
        }
	}
}
