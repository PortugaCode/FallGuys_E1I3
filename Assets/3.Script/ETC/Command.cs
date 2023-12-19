using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command : MonoBehaviour
{
    private Queue<KeyCode> commandQueue = new Queue<KeyCode>();

    public bool isCommandInsert = false;

    private void CommandCheck()
    {
        int count = 0;

        if (commandQueue.Dequeue() == KeyCode.UpArrow && count == 0)
        {
            count = count + 1;
            if (commandQueue.Dequeue() == KeyCode.UpArrow && count == 1)
            {
                count = count + 1;
                if (commandQueue.Dequeue() == KeyCode.DownArrow && count == 2)
                {
                    count = count + 1;
                    if (commandQueue.Dequeue() == KeyCode.DownArrow && count == 3)
                    {
                        count = count + 1;
                        if (commandQueue.Dequeue() == KeyCode.LeftArrow && count == 4)
                        {
                            count = count + 1;
                            if (commandQueue.Dequeue() == KeyCode.RightArrow && count == 5)
                            {
                                count = count + 1;
                                if (commandQueue.Dequeue() == KeyCode.LeftArrow && count == 6)
                                {
                                    count = count + 1;
                                    if (commandQueue.Dequeue() == KeyCode.RightArrow && count == 7)
                                    {
                                        count = count + 1;
                                        if (commandQueue.Dequeue() == KeyCode.E && count == 8)
                                        {
                                            count = count + 1;
                                            if (commandQueue.Dequeue() == KeyCode.I && count == 9)
                                            {
                                                count = count + 1;
                                                if (commandQueue.Dequeue() == KeyCode.I && count == 10)
                                                {
                                                    count = count + 1;
                                                    if (commandQueue.Dequeue() == KeyCode.I && count == 11)
                                                    {
                                                        count = count + 1;
                                                    }
                                                    else
                                                    {
                                                        count = 0;
                                                    }
                                                }
                                                else
                                                {
                                                    count = 0;
                                                }
                                            }
                                            else
                                            {
                                                count = 0;
                                            }
                                        }
                                        else
                                        {
                                            count = 0;
                                        }
                                    }
                                    else
                                    {
                                        count = 0;
                                    }
                                }
                                else
                                {
                                    count = 0;
                                }
                            }
                            else
                            {
                                count = 0;
                            }
                        }
                        else
                        {
                            count = 0;
                        }
                    }
                    else
                    {
                        count = 0;
                    }
                }
                else
                {
                    count = 0;
                }
            }
            else
            {
                count = 0;
            }
        }
        else
        {
            count = 0;
        }

        Debug.Log(count);
        if (count == 12)
        {
            GetComponent<PlayerControl>().isDev = true;
            GetComponent<PlayerControl>().speed *= 1.5f;
            Debug.Log("커맨드 입력 성공");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Insert))
        {
            if (!isCommandInsert)
            {
                isCommandInsert = true;
                Debug.Log("커맨드 입력 시작");
            }
            else
            {
                isCommandInsert = false;
                Debug.Log("커맨드 입력 종료");
                commandQueue.Clear();
            }
        }

        if (isCommandInsert)
        {

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                commandQueue.Enqueue(KeyCode.UpArrow);
                //Debug.Log(commandQueue.Dequeue().ToString());
                Debug.Log(commandQueue.Count);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                commandQueue.Enqueue(KeyCode.DownArrow);
                //Debug.Log(commandQueue.Dequeue().ToString());
                Debug.Log(commandQueue.Count);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                commandQueue.Enqueue(KeyCode.LeftArrow);
                //Debug.Log(commandQueue.Dequeue().ToString());
                Debug.Log(commandQueue.Count);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                commandQueue.Enqueue(KeyCode.RightArrow);
                //Debug.Log(commandQueue.Dequeue().ToString());
                Debug.Log(commandQueue.Count);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                commandQueue.Enqueue(KeyCode.E);
                //Debug.Log(commandQueue.Dequeue().ToString());
                Debug.Log(commandQueue.Count);
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                commandQueue.Enqueue(KeyCode.I);
                //Debug.Log(commandQueue.Dequeue().ToString());
                Debug.Log(commandQueue.Count);
            }

            if (commandQueue.Count >= 12)
            {
                CommandCheck();
            }
        }
    }
}
