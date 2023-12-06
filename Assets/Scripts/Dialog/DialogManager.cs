using System;
using System.Collections.Generic;
using System.Linq;
using MyBox;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour, IEventAction
{
    public static DialogManager Singleton { get; private set; }

    [Header("References")]
    [SerializeField] private Text dialogText;
    [SerializeField] private Text talkerText;
    [SerializeField] private GameObject talkerContainer;

    [SerializeField] private AudioSource audioSource;

    [Header("Config")]
    [SerializeField] private float wordsPerSec = 10;
    [SerializeField] private float fastForwardScale = 3f;
    
    [SerializeField] private AudioClip playerAudio;
    [SerializeField] private AudioClip otherAudio;


    // local
    private List<DialogData> _dialogs;
    private int _dialogsCurrentPage;
    private InteractibleEventAction _currentAction;
    private Action<bool, InteractibleEventAction> _completionCallback;
    private float _currentMessageWordIndex;

    public DialogManager()
    {
        Singleton = this;

    }

    private void Update()
    {
        if (_dialogs == null)
        {
            return;
        }

       
        var currentDialog = _dialogs[_dialogsCurrentPage];
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        if (currentDialog.Message != null && currentDialog.Message.Length > _currentMessageWordIndex)
        {
            float scale = 1;
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E))
            {
                scale = fastForwardScale;
            }
            _currentMessageWordIndex += wordsPerSec * Time.deltaTime * scale;
            _currentMessageWordIndex = Mathf.Clamp(_currentMessageWordIndex, -9999, currentDialog.Message.Length);
            if (_currentMessageWordIndex > 0)
            {
                dialogText.text = currentDialog.Message.Substring(0, Mathf.FloorToInt(_currentMessageWordIndex));
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.Stop();
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)  || Input.GetKeyDown(KeyCode.E))
            {
                NextPage();
            } 
        }
    }

    private void NextPage()
    {
        if (_dialogsCurrentPage >= _dialogs.Count - 1)
        {
            FinishedDialog();
        }
        else
        {
            SetDialogPage(_dialogsCurrentPage + 1);
        }
    }
    private void SetDialogPage(int newPage)
    {
        
        dialogText.text = "";
        _dialogsCurrentPage = newPage;
        _currentMessageWordIndex = -1;
        var cPage = _dialogs[_dialogsCurrentPage];
        if (cPage.Talker == DialogData.DialogTalker.None)
        {
            talkerContainer.SetActive(false);
        }
        else
        {
            talkerContainer.SetActive(true);
            if (cPage.Talker == DialogData.DialogTalker.Custom)
            {
                talkerText.text = cPage.TalkerCustom;
            }
            else
            {
                talkerText.text = cPage.Talker.ToString();
            }
        }
        
        if (cPage.Talker == DialogData.DialogTalker.Player)
        {
            audioSource.clip = playerAudio;
        } else {
            audioSource.clip = otherAudio;
        }
    }

    private void FinishedDialog()
    {
        _dialogs = null;
        gameObject.SetActive(false);
        _completionCallback(true, _currentAction);
    }

    public void DoEventAction(InteractibleEventAction action, Action<bool, InteractibleEventAction> completionCallback)
    {
        this._dialogs = action.Dialogs.Value.ToList();
        if (!_dialogs.IsNullOrEmpty())
        {
            this._currentAction = action;
            this._completionCallback = completionCallback;
            gameObject.SetActive(true);
            SetDialogPage(0);
        }
        else
        {
            completionCallback(true, action);
        }
    }

    private void OnValidate()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }
}
