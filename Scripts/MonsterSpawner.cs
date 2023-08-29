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
            m_queue.Enqueue(t_object);  // �ʱ⿡ 10���� ���͸� �����Ͽ� ť�� �߰��մϴ�.
            t_object.SetActive(false);  // ������ ���͸� ��Ȱ��ȭ�մϴ�.
        }

        StartCoroutine(MonsterSpawn());  // ���� ���� �ڷ�ƾ�� �����մϴ�.
    }

    public void InsertQueue(GameObject p_object)
    {
        m_queue.Enqueue(p_object);  // ���͸� ť�� �߰��մϴ�.
        p_object.SetActive(false);  // �߰��� ���͸� ��Ȱ��ȭ�մϴ�.
    }

    public GameObject GetQueue()
    {
        GameObject t_object = m_queue.Dequeue();  // ť���� ���͸� �����ɴϴ�.
        t_object.SetActive(true);  // ������ ���͸� Ȱ��ȭ�մϴ�.

        return t_object;  // ������ ���͸� ��ȯ�մϴ�.
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
                GameObject t_object = GetQueue();  // ť���� ���͸� �����ɴϴ�.
                t_object.transform.position = gameObject.transform.position + RandomVector;  // ������ ������ ��ġ�� �����մϴ�.
            }
            yield return new WaitForSeconds(5f);  // 1�ʸ��� ���� ������ �ݺ��մϴ�.
        }
    }

}
