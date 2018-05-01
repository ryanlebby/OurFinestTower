using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSpeedBuff : TimedBuff
{
    private SpeedBuff speedBuff;

    private Unit unit;

    public TimedSpeedBuff(float duration, ScriptableBuff buff, GameObject obj) : base(duration, buff, obj)
    {
        unit = obj.GetComponent<Unit>();
        speedBuff = (SpeedBuff)buff;
    }

    public override void Activate()
    {
        SpeedBuff speedBuff = (SpeedBuff)buff;
        unit.MoveSpeed += speedBuff.SpeedIncrease;
    }

    public override void End()
    {
        unit.MoveSpeed -= speedBuff.SpeedIncrease;
    }
}
