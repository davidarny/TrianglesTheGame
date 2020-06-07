using UnityEngine;

public class LogUtils
{
    public static void LogState(string context)
    {
        Debug.Log($"========== {context} LEVEL={GameStore.instance.GetAbsoluteWeight() + 1} STEP={GameStore.instance.step + 1} ==========");
    }
}
