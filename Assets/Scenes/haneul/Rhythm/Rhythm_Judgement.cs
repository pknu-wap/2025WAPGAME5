using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rhythm_Judgement : MonoBehaviour
{
    public KeyCode keyToPress;                  // 키 입력
    public string noteTag;                      // 노트 태그 (RightNote, LeftNote 등)
    public GameObject hitObjectPrefab;          // 히트 이펙트 프리팹
    public GameObject missObjectPrefab;         // 미스 이펙트 프리팹

    public AudioClip clip_R;  // 오른쪽 드럼 소리
    public AudioClip clip_L;  // 왼쪽 드럼 소리

    private Queue<GameObject> noteQueue = new Queue<GameObject>(); // 충돌 중인 노트 큐
    private AudioSource audioSource;

    public static int Rhythm_Score = 100;

    void Start()
    {
        // AudioSource 자동 추가 (없으면)
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(noteTag))
        {
            noteQueue.Enqueue(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(noteTag) && noteQueue.Contains(other.gameObject))
        {
            // 해당 노트를 큐에서 제거
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
            Vector3 spawnPosition = transform.position + new Vector3(0f, 1.95f, 1f);

            if (noteQueue.Count > 0)
            {
                GameObject firstNote = noteQueue.Dequeue();
                Destroy(firstNote);

                if (hitObjectPrefab != null)
                    Instantiate(hitObjectPrefab, spawnPosition, Quaternion.identity);

                Debug.Log("Hit!");
                

                // 방향에 따라 사운드 다르게 재생
                if (noteTag == "RightNote" && clip_R != null)
                    audioSource.PlayOneShot(clip_R,0.5f);
                else if (noteTag == "LeftNote" && clip_L != null)
                    audioSource.PlayOneShot(clip_L,0.6f);
            }
            else
            {
                if (missObjectPrefab != null)
                    Instantiate(missObjectPrefab, spawnPosition, Quaternion.identity);

                Debug.Log("Miss...");
                Rhythm_Score -= 1;
            }
        }
    }
}
