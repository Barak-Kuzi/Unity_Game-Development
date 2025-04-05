using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class TalkingGranny1 : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    public GameObject player;
    public Text talkText;
    public GameObject talkBackground;
    RawImage background;

    public GameObject target;
    public GameObject point1;
    public GameObject point2;

    int ansCount = 0;
    static int level = 0;
    bool isQuestioning = false;
    bool isnavigating = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        background = talkBackground.GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isnavigating)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if(agent.isStopped)
            {
                agent.isStopped = false;
            }
            if(distance < 1)
            {
                animator.SetBool("isWalking", false);
                agent.isStopped = true;
                isnavigating = false;
            }

        }
        else
        {
            float distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceFromPlayer < 2)
            {
                // rotate the npc towards the player
                Vector3 target_dir = player.transform.position - transform.position;
                target_dir.y = 0; // stay in x-z plane
                Vector3 temp_dir = Vector3.RotateTowards(transform.forward, target_dir, Time.deltaTime, 0);
                transform.rotation = Quaternion.LookRotation(temp_dir);
                animator.SetBool("isTalking", true); // start talking
                if (!isQuestioning)
                {
                    isQuestioning = true;
                    StartDialog();
                }
            }
            else
            {
                EndDialog();
            }
        }
    }

    private void WalkToNext()
    {
        level++;
        switch(level)
        {
            case 1:
                target.transform.position = point1.transform.position;
                break;
            case 2:
                target.transform.position = point2.transform.position;
                break;
            default:
                break;
        }
        agent.SetDestination(target.transform.position);
        animator.SetBool("isWalking", true);
        isnavigating = true;
    }

    private void StartDialog()
    {   
        background.enabled = true;
        talkText.enabled = true;
        switch(level)
        {
            case 0:
                //first dialog;
                StartCoroutine(FirstDialog());
                break;
            case 1:
                //find key in drawer dialog
                StartCoroutine(SecondDialog());
                break;
            case 2:
                StartCoroutine(AskQuestions());
                break;
        }
    }

    private void EndDialog()
    {
        switch(level)
        {
            case 0:
                //first dialog;
                StopCoroutine(FirstDialog());
                break;
            case 1:
                //find key in drawer dialog
                
                break;
            case 2:
                StopCoroutine(AskQuestions());
                break;
        }
        animator.SetBool("isTalking", false);
        isQuestioning = false;
        background.enabled = false;
        talkText.enabled = false;
    }

    private IEnumerator FirstDialog()
    {
        talkText.text = "Hello visitor! Welcome to the house of giants!";
        yield return new WaitForSeconds(2f);
        talkText.text = "If you wish to go back to your world...";
        yield return new WaitForSeconds(2f);
        talkText.text = "Find the 4 keys to open the elevator";
        yield return new WaitForSeconds(2f);
        EndDialog();
        WalkToNext();
    }

        private IEnumerator SecondDialog()
    {
        talkText.text = "In this room a key is hidden in one of the drawers";
        yield return new WaitForSeconds(2f);
        talkText.text = "I will be waiting for you in the next room";
        yield return new WaitForSeconds(2f);
        EndDialog();
        WalkToNext();
    }

    private IEnumerator AskQuestions()
    {
        talkText.text = "Answer 3 questions to get your key:";
        yield return new WaitForSeconds(2f);
        while (ansCount < 3)
        {
            switch (ansCount)
            {
                case 0:
                    talkText.text = "Question number 1:";
                    yield return new WaitForSeconds(2f);
                    talkText.text = "Is Victor the best lecturer in Afeka? Y/N";
                    yield return StartCoroutine(GetAnswer(answer =>
                    {
                        if (answer)
                        {
                            // Player answered "Yes"
                            talkText.text = "Correct";
                            ansCount++;
                        }
                        else
                        {
                            // Player answered "No"
                            talkText.text = "Wrong";
                        }
                    }));
                    yield return new WaitForSeconds(2f);
                    break;

                case 1:
                    talkText.text = "Question number 2:";
                    yield return new WaitForSeconds(2f);
                    talkText.text = "Is Yarin the best Student in Afeka? Y/N";
                    yield return StartCoroutine(GetAnswer(answer =>
                    {
                        if (answer)
                        {
                            // Player answered "Yes"
                            talkText.text = "Correct";
                            ansCount++;
                        }
                        else
                        {
                            // Player answered "No"
                            talkText.text = "Wrong";
                        }
                    }));
                    yield return new WaitForSeconds(2f);
                    break;
                
                case 2:
                    talkText.text = "Question number 3:";
                    yield return new WaitForSeconds(2f);
                    talkText.text = "Is this project worth 100? Y/N";
                    yield return StartCoroutine(GetAnswer(answer =>
                    {
                        if (answer)
                        {
                            // Player answered "Yes"
                            talkText.text = "Correct";
                            ansCount++;
                        }
                        else
                        {
                            // Player answered "No"
                            talkText.text = "Wrong";
                        }
                    }));
                    yield return new WaitForSeconds(2f);
                    break;
            }   
        }
        if(ansCount >= 3)
        {
            talkText.text = "You got your key from this room! Good Luck in your journey.";
            yield return new WaitForSeconds(2f);
            KeyBehaviour.setNumOfKeys(KeyBehaviour.getNumOfKeys()+ 1);
        }
    }

    private IEnumerator GetAnswer(System.Action<bool> callback)
    {
        bool waitingForInput = true;

        while (waitingForInput)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                waitingForInput = false;
                callback(true);
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                waitingForInput = false;
                callback(false);
            }

            yield return null;
        }
    }

    private IEnumerator ShowMessageAndContinue(string message, float duration)
    {
        talkText.text = message;
        yield return new WaitForSeconds(duration);
    }
}
