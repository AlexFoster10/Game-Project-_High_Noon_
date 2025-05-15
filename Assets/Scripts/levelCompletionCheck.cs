using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelCompletionCheck : MonoBehaviour
{
    public static levelCompletionCheck instance;
    [SerializeField]  public static bool level1Check=false;
    [SerializeField]  public static bool level2Check=false;
    [SerializeField]  public static bool level3Check=false;

    public static void markLevelComplete(string x)
    {
        switch (x)
        {
            case "Level 1":
                level1Check = true;
                break;
            case "Level 2":
                level2Check = true;
                break;
            case "Level 3":
                level3Check = true;
                break;
        }
    }

    public static bool getLevelStatus(string x)
    {
        switch (x)
        {
            case "Level 1":
                return level1Check;
            case "Level 2":
                return level2Check;
            case "Level 3":
                return level3Check;
        }
        return false;
    }


}
