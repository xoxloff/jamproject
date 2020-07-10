using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEventArgs : EventArgs
{
    public GameObject QuestPanel { get; set; }

    public QuestEventArgs()
    {

    }
    public QuestEventArgs(GameObject questPanel)
    {
        this.QuestPanel = questPanel;
    }
}
