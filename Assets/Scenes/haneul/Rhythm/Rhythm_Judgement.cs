using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhythm_Judgement : MonoBehaviour
{
    public KeyCode keyToPress;
    public string noteTag;               // 해당 방향 노트 태그 (예: "LeftNote")
    public GameObject hitObjectPrefab;   // 히트 시 생성될 오브젝트 프리펩
    public GameObject missObjectPrefab;  // 미스 시 생성될 오브젝트 프리펩

    private Queue<GameObject> noteQueue = new Queue<GameObject>();  // 노트 큐

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("In");
        if (other.CompareTag(noteTag))
        {
            noteQueue.Enqueue(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(noteTag) && noteQueue.Contains(other.gameObject))
        {
            // 큐에서 해당 노트 제거
            Queue<GameObject> newQueue = new Queue<GameObject>();

            foreach (var note in noteQueue)
            {
                if (note != other.gameObject)
                    newQueue.Enqueue(note);
            }

            noteQueue = newQueue;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            Vector3 spawnPosition = transform.position + new Vector3(0f, 0f, 0.1f);

            if (noteQueue.Count > 0)
            {
                GameObject firstNote = noteQueue.Dequeue();
                Destroy(firstNote);

                // 히트 오브젝트 생성 (z + 0.1f 위치에)
                if (hitObjectPrefab != null)
                {
                    Instantiate(hitObjectPrefab, spawnPosition, Quaternion.identity);
                }

                Debug.Log("Hit!");
            }
            else
            {
                // 미스 오브젝트 생성 (z + 0.1f 위치에)
                if (missObjectPrefab != null)
                {
                    Instantiate(missObjectPrefab, spawnPosition, Quaternion.identity);
                }

                Debug.Log("Miss...");
            }
        }
    }
}
