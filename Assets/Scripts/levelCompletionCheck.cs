using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelCompletionCheck : MonoBehaviour
{
    public static levelCompletionCheck instance;
    [SerializeField]  public static bool level1Check = false;
    [SerializeField]  public static bool level2Check = false;
    [SerializeField]  public static bool level3Check = false;
    [SerializeField]  public static bool script1flag = false;
    [SerializeField]  public static bool script2flag = false;

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

    public static void setScriptStatus(int x)
    {
        print("SetScriptStat");
        switch (x)
        {
            case 1:
                script1flag = true;
                break;
            case 2:
                script2flag = true;
                break;
        }
    }
    public static bool getScriptStatus(int x)
    {
        print("GetScriptStat");
        switch (x)
        {
            case 1:
                return script1flag;
            case 2:
                return script2flag;
        }
        return true;
    }




}
