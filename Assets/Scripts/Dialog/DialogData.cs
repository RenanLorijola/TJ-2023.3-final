using System;
using MyBox;
using UnityEngine;

[Serializable]
public class DialogData
{
    public enum DialogTalker
    {
        None, Player, Custom
    }

    [SerializeField] private DialogTalker talker;
    [ConditionalField(nameof(talker), false, DialogTalker.Custom)][SerializeField] private String talkerCustom;

    [SerializeField] private String message;

    public DialogTalker Talker => talker;

    public string TalkerCustom => talkerCustom;

    public string Message => message;

}
