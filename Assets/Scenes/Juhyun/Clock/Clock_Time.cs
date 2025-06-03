using System.Collections;
using UnityEngine;

namespace ClockSample
{
    public class Clock : MonoBehaviour
    {
        public Transform handHours;
        public Transform handMinutes;
        public Transform handSeconds;

        private float hours = 8f;
        private float minutes = 0f;
        private float seconds = 0f;

        private Tutorial tutorial;

        private void Start()
        {
            FindObjectOfType<GameManager>().Emotion.SetActive(true);
            FindObjectOfType<GameManager>().Mission.SetActive(true);
            FindObjectOfType<GameManager>().Crosshair.SetActive(true);

            tutorial = FindObjectOfType<Tutorial>();
            StartCoroutine(PlayerLooksAtClock());
            InvokeRepeating(nameof(UpdateHands), 0, 1);
        }

        IEnumerator PlayerLooksAtClock()
        {
            Transform playerCam = Camera.main.transform;
            Player playerScript = playerCam.GetComponentInParent<Player>();

            if (playerScript != null)
            {
                playerScript.SetLockCamera(true);
                playerScript.SetDontMove(true);
            }

            float duration = 3f;
            float timer = 0f;

            while (timer < duration)
            {
                playerCam.LookAt(transform);
                timer += Time.deltaTime;
                yield return null;
            }

            if (playerScript != null)
            {
                playerScript.SetLockCamera(false);
                playerScript.SetDontMove(false);
            }

            if (tutorial != null)
            {
                tutorial.Tuto();
            }
        }

        void UpdateHands()
        {
            seconds += 1f;
            if (seconds >= 60f)
            {
                seconds = 0f;
                minutes += 1f;

                if (minutes >= 60f)
                {
                    minutes = 0f;
                    hours += 1f;

                    if (hours >= 12f)
                    {
                        hours = 0f;
                    }
                }
            }

            float handRotationHours = (hours + minutes / 60f) * 30f;
            float handRotationMinutes = minutes * 6f;
            float handRotationSeconds = seconds * 6f;

            if (handHours)
                handHours.localEulerAngles = new Vector3(0, 0, handRotationHours);
            if (handMinutes)
                handMinutes.localEulerAngles = new Vector3(0, 0, handRotationMinutes);
            if (handSeconds)
                handSeconds.localEulerAngles = new Vector3(0, 0, handRotationSeconds);
        }

        private void OnDestroy()
        {
            CancelInvoke();
        }
    }
}
