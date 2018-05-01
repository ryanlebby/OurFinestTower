﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableBuff : ScriptableObject
{

    public float Duration;

    public abstract TimedBuff InitializeBuff(GameObject obj);

}
