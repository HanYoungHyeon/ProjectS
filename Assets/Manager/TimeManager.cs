using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : Singleton<TimeManager>
{
    [SerializeField, Range(1,10)]
    private int timeAcceleration;
    private int gameSecond = 0;
    public int dayCount { get; private set; }
    private WaitForSecondsRealtime realSecond;
    public UnityAction timeProgressAction;
    public UnityAction dayProgressAction;
    private bool isPause;

    private IEnumerator Start()
    {
        realSecond = new WaitForSecondsRealtime(1f);
        while(true)
        {
            if(!isPause)
            {
                gameSecond += timeAcceleration;
                if (gameSecond >= 1440)
                {
                    gameSecond = 0;
                    dayCount++;
                    dayProgressAction?.Invoke();
                }
                timeProgressAction?.Invoke();
            }
            yield return realSecond;
        }
    }





}
