using UnityEngine;
using UnityEngine.Events;

public static class RoundEnd
{
    public static UnityEvent EventRoundEnd = new UnityEvent();

    public static void InvokeRoundEnd()
    {
        EventRoundEnd?.Invoke();
    }
}
