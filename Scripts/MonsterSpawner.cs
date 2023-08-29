using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monster;
    public static MonsterSpawner instance;
    public Queue<GameObject> m_queue = new Queue<GameObject>();

    public float xPos;
    public float zPos;
    private Vector3 RandomVector;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        for (int i = 0; i < 5; i++)
        {
            GameObject t_object = Instantiate(monster, this.gameObject.transform);
            m_queue.Enqueue(t_object);  // 초기에 10개의 몬스터를 생성하여 큐에 추가합니다.
            t_object.SetActive(false);  // 생성한 몬스터를 비활성화합니다.
        }

        StartCoroutine(MonsterSpawn());  // 몬스터 생성 코루틴을 실행합니다.
    }

    public void InsertQueue(GameObject p_object)
    {
        m_queue.Enqueue(p_object);  // 몬스터를 큐에 추가합니다.
        p_object.SetActive(false);  // 추가한 몬스터를 비활성화합니다.
    }

    public GameObject GetQueue()
    {
        GameObject t_object = m_queue.Dequeue();  // 큐에서 몬스터를 가져옵니다.
        t_object.SetActive(true);  // 가져온 몬스터를 활성화합니다.

        return t_object;  // 가져온 몬스터를 반환합니다.
    }

    IEnumerator MonsterSpawn()
    {
        while (true)
        {
            if (m_queue.Count != 0)
            {
                xPos = Random.Range(-5, 5);
                //zPos = Random.Range(-5, 5);
                RandomVector = new Vector3(0.0f, xPos, 0.0f);
                GameObject t_object = GetQueue();  // 큐에서 몬스터를 가져옵니다.
                t_object.transform.position = gameObject.transform.position + RandomVector;  // 가져온 몬스터의 위치를 설정합니다.
            }
            yield return new WaitForSeconds(5f);  // 1초마다 몬스터 생성을 반복합니다.
        }
    }

}
