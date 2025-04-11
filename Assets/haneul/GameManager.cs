using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static GameManager Instance { get; private set; }

    public GameObject Emotion;
    public static int currentEmotion = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� �Ѿ�� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

}