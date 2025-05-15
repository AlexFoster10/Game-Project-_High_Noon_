using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class levelSelectScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //levelIconCheck();
        //onStartDeactivate();
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void onStartDeactivate()
    {
        for (int i = 0, count = transform.childCount - 1; i < count; i++)
        {
            //print("This Many Children");
            GameObject child = transform.GetChild(i).gameObject;
            {
                GameObject temp = child.transform.GetChild(0).gameObject;
                if (temp.name == "Pass")
                {
                    switch (child.name)
                    {
                        case "Level 1":

                            if (levelCompletionCheck.getLevelStatus("Level 1"))
                            {
                                temp.SetActive(false);
                                print("start Getting run");
                            }
                            break;
                        case "Level 2":

                            if (levelCompletionCheck.getLevelStatus("Level 2"))
                            {
                                temp.SetActive(false);
                            }
                            break;
                        case "Level 3":

                            if (levelCompletionCheck.getLevelStatus("Level 3"))
                            {
                                temp.SetActive(false);
                            }
                            break;

                    }
                }
            }
        }
    }

    void levelIconCheck()
    {
        for (int i = 0, count = transform.childCount-1; i < count; i++)
        {
            //print("This Many Children");
            GameObject child = transform.GetChild(i).gameObject;
            {
                GameObject temp = child.transform.GetChild(0).gameObject;
                if (temp.name == "Pass")
                {
                    switch (child.name)
                    {
                        case "Level 1":

                            if (levelCompletionCheck.getLevelStatus("Level 1"))
                            {
                                temp.SetActive(true);
                            }
                            break;
                        case "Level 2":

                            if (levelCompletionCheck.getLevelStatus("Level 2"))
                            {
                                temp.SetActive(true);
                            }
                            break;
                        case "Level 3":

                            if (levelCompletionCheck.getLevelStatus("Level 3"))
                            {
                                temp.SetActive(true);
                            }
                            break;

                    }
                }
            }
        }
        for (int y = 0, count = transform.childCount - 1; y < count; y++)
        {
            GameObject child = transform.GetChild(y).gameObject;
            {
                GameObject temp = child.transform.GetChild(1).gameObject;
                if (temp.name == "Lock")
                {
                    switch (child.name)
                    {
                        case "Level 2":

                            if (levelCompletionCheck.getLevelStatus("Level 1"))
                            {
                                temp.SetActive(false);
                            }
                            break;
                        case "Level 3":

                            if (levelCompletionCheck.getLevelStatus("Level 2"))
                            {
                                temp.SetActive(false);
                            }
                            break;

                    }
                }
            }
        }
        
    }
}

