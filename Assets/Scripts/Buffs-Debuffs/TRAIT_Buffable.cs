using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRAIT_Buffable : MonoBehaviour
{

    public List<TimedBuff> CurrentBuffs = new List<TimedBuff>();

    void Update()
    {
        //if (Game.isPaused)
        //    return;

        foreach (TimedBuff buff in CurrentBuffs.ToArray())
        {
            buff.Tick(Time.deltaTime);
            if (buff.IsFinished)
            {
                CurrentBuffs.Remove(buff);
            }
        }
    }

    public void AddBuff(TimedBuff buff)
    {
        CurrentBuffs.Add(buff);
        buff.Activate();
    }
}
