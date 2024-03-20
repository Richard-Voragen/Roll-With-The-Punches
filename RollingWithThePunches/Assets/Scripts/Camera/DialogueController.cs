using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CameraControls
{
    public class DialogueController : MonoBehaviour
    {
        public TextMeshProUGUI DialogueText;
        public string[] Sentences;
        private int index = 0;
        public float dialogueSpeed;
        private TutorialCameraController cam;
        private bool ready = true;

        void Start()
        {
            this.cam = FindObjectOfType<TutorialCameraController>();
        }

        void Update()
        {
            if (Input.GetButtonDown("Fire1") && cam.scrolling == false && ready)
            {
                ready = false;
                NextSentence();
            }
        }

        void NextSentence()
        {
            DialogueText.text = "";
            if (index <= Sentences.Length - 1)
            {
                FindObjectOfType<SoundManager>().PlaySoundEffect("Fireball");
                if (Sentences[index] == "NextImage")
                {
                    cam.NextImage();
                    ready = true;
                    index++;
                }
                else
                {
                    StartCoroutine(WriteSentence());
                }
            }
            else
            {
                FindObjectOfType<ADSRManager>().enabled = true;
                FindObjectOfType<ShootFireball>().enabled = true;
                FindObjectOfType<PositionLockCameraController>().enabled = true;

                cam.enabled = false;
            }
        }

        IEnumerator WriteSentence()
        {
            foreach(char character in Sentences[index].ToCharArray())
            {
                DialogueText.text += character;
                yield return new WaitForSeconds(dialogueSpeed);
            }
            ready = true;
            index++;
        }
    }
}