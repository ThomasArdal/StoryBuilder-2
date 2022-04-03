﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using StoryBuilder.DAL;
using StoryBuilder.Models;
using StoryBuilder.Services.Logging;
using StoryBuilder.Services.Messages;
using StoryBuilder.Services.Navigation;
using Syncfusion.UI.Xaml.Editors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;  

namespace StoryBuilder.ViewModels;

public class SceneViewModel : ObservableRecipient, INavigable
{
    #region Fields

    private readonly StoryReader _rdr;
    private readonly StoryWriter _wtr;
    private readonly LogService _logger;
    private StatusMessage _smsg;
    private bool _changeable; // process property changes for this story element
    private bool _changed;    // this story element has changed

    #endregion

    #region Properties

    // StoryElement data

    private Guid _uuid;
    public Guid Uuid
    {
        get => _uuid;
        set => SetProperty(ref _uuid, value);
    }

    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            if (_changeable && _name != value) // Name changed?
            {
                _logger.Log(LogLevel.Info, $"Requesting Name change from {_name} to {value}");
                NameChangeMessage msg = new(_name, value);
                Messenger.Send(new NameChangedMessage(msg));
            }
            SetProperty(ref _name, value);
        }
    }

    //  Scene general data

    private string _description;
    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    private string _viewpointCharacter;
    public string ViewpointCharacter
    {
        get => _viewpointCharacter;
        set
        {
            SetProperty(ref _viewpointCharacter, value);
            if (value.Equals(string.Empty))
                return;
            if (!CastMemberExists(value))
            {
                CastMembers.Add(StringToStoryElement(value));
            }
        }
    }

    private string _date;
    public string Date
    {
        get => _date;
        set => SetProperty(ref _date, value);
    }

    private string _time;
    public string Time
    {
        get => _time;
        set => SetProperty(ref _time, value);
    }

    private string _setting;
    public string Setting
    {
        get => _setting;
        set => SetProperty(ref _setting, value);
    }

    private string _sceneType;
    public string SceneType
    {
        get => _sceneType;
        set => SetProperty(ref _sceneType, value);
    }

    private int _existingCastIndex;
    public int ExistingCastIndex
    {
        get => _existingCastIndex;
        set
        {
            SetProperty(ref _existingCastIndex, value);
            if (_existingCastIndex == -1)
                return;
            Messenger.Send(new StatusChangedMessage(new($"Existing cast member {CastMembers[value].Name} selected", LogLevel.Info)));
        }
    }

    private ObservableCollection<StoryElement> _castMembers;
    public ObservableCollection<StoryElement> CastMembers
    {
        get => _castMembers;
        set => SetProperty(ref _castMembers, value);
    }

    private string _newCastMember;
    public string NewCastMember
    {
        get => _newCastMember;
        set => SetProperty(ref _newCastMember, value);
    }

    private string _remarks;
    public string Remarks
    {
        get => _remarks;
        set => SetProperty(ref _remarks, value);
    }

    // Scene development data (from Lisa Cron's Story Genius)

    private ObservableCollection<string> _scenePurpose;
    public ObservableCollection<string> ScenePurpose
    {
        get => _scenePurpose;
        set => SetProperty(ref _scenePurpose, value);
    }

    private string _valueExchange;
    public string ValueExchange
    {
        get => _valueExchange;
        set => SetProperty(ref _valueExchange, value);
    }

    private string _events;
    public string Events
    {
        get => _events;
        set => SetProperty(ref _events, value);
    }

    private string _consequences;
    public string Consequences
    {
        get => _consequences;
        set => SetProperty(ref _consequences, value);
    }

    private string _significance;
    public string Significance
    {
        get => _significance;
        set => SetProperty(ref _significance, value);
    }

    private string _realization;
    public string Realization
    {
        get => _realization;
        set => SetProperty(ref _realization, value);
    }

    //  Scene Conflict data

    private string _protagonist;
    public string Protagonist
    {
        get => _protagonist;
        set => SetProperty(ref _protagonist, value);
    }

    private string _protagEmotion;
    public string ProtagEmotion
    {
        get => _protagEmotion;
        set => SetProperty(ref _protagEmotion, value);
    }

    private string _protagGoal;
    public string ProtagGoal
    {
        get => _protagGoal;
        set => SetProperty(ref _protagGoal, value);
    }

    private string _antagonist;
    public string Antagonist
    {
        get => _antagonist;
        set => SetProperty(ref _antagonist, value);
    }

    private string _antagEmotion;
    public string AntagEmotion
    {
        get => _antagEmotion;
        set => SetProperty(ref _antagEmotion, value);
    }

    private string _antagGoal;
    public string AntagGoal
    {
        get => _antagGoal;
        set => SetProperty(ref _antagGoal, value);
    }

    private string _opposition;
    public string Opposition
    {
        get => _opposition;
        set => SetProperty(ref _opposition, value);
    }

    private string _outcome;
    public string Outcome
    {
        get => _outcome;
        set => SetProperty(ref _outcome, value);
    }

    //  Scene Sequel data

    private string _emotion;
    public string Emotion
    {
        get => _emotion;
        set => SetProperty(ref _emotion, value);
    }

    private string _newGoal;
    public string NewGoal
    {
        get => _newGoal;
        set => SetProperty(ref _newGoal, value);
    }

    private string _review;
    public string Review
    {
        get => _review;
        set => SetProperty(ref _review, value);
    }

    //  Scene notes data
    private string _notes;
    public string Notes
    {
        get => _notes;
        set => SetProperty(ref _notes, value);
    }

    private SceneModel _model;
    public SceneModel Model
    {
        get => _model;
        set => SetProperty(ref _model, value);
    }

    private string _vpCharTip;
    public string VpCharTip 
    {
        get => _vpCharTip;
        set => SetProperty(ref _vpCharTip, value);  
    }

    private bool _vpCharTipIsOpen;
    public bool VpCharTipIsOpen
    {
        get => _vpCharTipIsOpen;
        set => SetProperty(ref _vpCharTipIsOpen, value);
    }


    #endregion

    #region Relay Commands

    public RelayCommand AddCastCommand { get; }
    public RelayCommand RemoveCastCommand { get; }

    #endregion

    #region Methods

    public void Activate(object parameter)
    {
        Model = (SceneModel)parameter;
        LoadModel();   // Load the ViewModel from the Story
    }

    public void Deactivate(object parameter)
    {
        SaveModel();    // Save the ViewModel back to the Story
    }
    private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
    {
        if (_changeable)
        {
            _changed = true;
            ShellViewModel.ShowChange();
        }
    }

    private void LoadModel()
    {
        _changeable = false;
        _changed = false;

        Uuid = Model.Uuid;
        Name = Model.Name;
        Description = Model.Description;
        Date = Model.Date;
        Time = Model.Time;
        Setting = Model.Setting;
        SceneType = Model.SceneType;
        CastMembers.Clear();
        foreach (string member in Model.CastMembers)
        {
            StoryElement element = StringToStoryElement(member);
            if (element != null)        // found
                CastMembers.Add(StringToStoryElement(member));
        }
        ViewpointCharacter = Model.ViewpointCharacter;

        // The ScenePurpose multi-select SfComboBox
        // SelectedItems IList is read-only, so we
        // use callback delegates to clear and add
        // the scene's list of purposes from delegate
        // methods declared in ScenePage.xaml.cs
        ClearScenePurpose();
        foreach (string purpose in Model.ScenePurpose)
        {
            AddScenePurpose(purpose);
            ScenePurpose.Add(purpose);
        }

        ValueExchange = Model.ValueExchange;
        Protagonist = Model.Protagonist;
        ProtagEmotion = Model.ProtagEmotion;
        ProtagGoal = Model.ProtagGoal;
        Antagonist = Model.Antagonist;
        AntagEmotion = Model.AntagEmotion;
        AntagGoal = Model.AntagGoal;
        Opposition = Model.Opposition;
        Outcome = Model.Outcome;
        Emotion = Model.Emotion;
        NewGoal = Model.NewGoal;
        Remarks = Model.Remarks;
        Events = Model.Events;
        Consequences = Model.Consequences;
        Significance = Model.Significance;
        Realization = Model.Realization;
        Review = Model.Review;
        Notes = Model.Notes;
        NewCastMember = string.Empty;
        GetOverviewViewpoint();

        _changeable = true;
    }

    internal void SaveModel()
    {
        _changeable = false;
        if (_changed)
        {
            // Story.Uuid is read-only and cannot be assigned
            Model.Name = Name;
            Model.Description = Description;
            Model.ViewpointCharacter = ViewpointCharacter;
            Model.Date = Date;
            Model.Time = Time;
            Model.Setting = Setting;
            Model.SceneType = SceneType;
            Model.CastMembers.Clear();
            foreach (StoryElement element in CastMembers)
                Model.CastMembers.Add(element.ToString());
            Model.ScenePurpose.Clear();
            foreach(string purpose in ScenePurpose)
                Model.ScenePurpose.Add(purpose);    
            Model.ValueExchange = ValueExchange;
            Model.Protagonist = Protagonist;
            Model.ProtagEmotion = ProtagEmotion;
            Model.ProtagGoal = ProtagGoal;
            Model.Antagonist = Antagonist;
            Model.AntagEmotion = AntagEmotion;
            Model.AntagGoal = AntagGoal;
            Model.Opposition = Opposition;
            Model.Outcome = Outcome;
            Model.Emotion = Emotion;
            Model.NewGoal = NewGoal;

            // Write RTF files
            Model.Remarks = Remarks;
            Model.Events = Events;
            Model.Consequences = Consequences;
            Model.Significance = Significance;
            Model.Realization = Realization;
            Model.Review = Review;
            Model.Notes = Notes;

            //_logger.Log(LogLevel.Info, string.Format("Requesting IsDirty change to true"));
            //Messenger.Send(new IsChangedMessage(Changed));
            _changeable = true;
        }
    }

    /// Delegate types and instances for updating the
    /// ScenePurpose SfComboBox
    public delegate void ClearScenePurposeDelegate();
    public delegate void AddScenePurposeDelegate(string purpose);

    public ClearScenePurposeDelegate ClearScenePurpose;
    public AddScenePurposeDelegate AddScenePurpose;

    public void ScenePurpose_SelectionChanged(object sender, ComboBoxSelectionChangedEventArgs e)
    {
        foreach (string purpose in e.AddedItems)
            ScenePurpose.Add(purpose);
        foreach (string purpose in e.RemovedItems)
            ScenePurpose.Remove(purpose);
    }

    private bool CastMemberExists(string uuid)
    {
        foreach (StoryElement element in CastMembers) {if (uuid == element.Uuid.ToString()) {return true;}}
        return false;
    }

    private void AddCastMember()
    {
        // Edit the character to add
        StoryElement element = StringToStoryElement(NewCastMember);
        if (element == null)
        {
            Messenger.Send(new StatusChangedMessage(new StatusMessage("Select a character to add to scene cast", LogLevel.Warn, true)));
            return;
        }
        if (CastMemberExists(NewCastMember))
        {
            _smsg = new StatusMessage("Character is already in scene cast", LogLevel.Warn);
            Messenger.Send(new StatusChangedMessage(_smsg));
            return;
        }

        CastMembers.Add(element);
        OnPropertyChanged();
        Messenger.Send(new StatusChangedMessage(new($"New cast member {element.Name} added", LogLevel.Info, true)));
    }

    private void RemoveCastMember()
    {
        if (ExistingCastIndex == -1)
        {
            _smsg = new StatusMessage("Select a scene cast member to remove", LogLevel.Info);
            Messenger.Send(new StatusChangedMessage(_smsg));
            return;
        }
        StoryElement element = CastMembers[ExistingCastIndex];
        CastMembers.RemoveAt(ExistingCastIndex);
        OnPropertyChanged();
        Messenger.Send(new StatusChangedMessage(new($"Cast member {element.Name} removed", LogLevel.Info, true)));
    }

    private StoryElement StringToStoryElement(string value)
    {
        if (value == null)
            return null;
        if (value.Equals(string.Empty))
            return null;

        // Look for the StoryElement corresponding to the passed guid
        // (This is the normal approach)
        // Get the current StoryModel's StoryElementsCollection
        StoryModel model = ShellViewModel.GetModel();
        StoryElementCollection elements = model.StoryElements;
        if (Guid.TryParse(value, out Guid guid))
        {
            if (elements.StoryElementGuids.ContainsKey(guid)) { return elements.StoryElementGuids[guid]; }

        }

        // legacy: locate the StoryElement from its Name
        foreach (StoryElement element in elements)  // Character or Setting??? Search both?
        {
            if (element.Type == StoryItemType.Character | element.Type == StoryItemType.Setting)
            {
                if (value.Trim().Equals(element.Name.Trim()))
                    return element;
            }
        }
        // not found
        string msg = $"Story Element not found name {value}";
        _logger.Log(LogLevel.Warn, msg);
        return null;   
    }

    /// <summary>
    /// Build VpCharTip, the bound content of the ViewpointCharacterTip TeachingTip,
    /// by finding and parsing OverViewModel's Viewpoint and ViewPointCharacter.
    /// 
    /// For example, if the Viewpoint is 'First person', the scene's viewpoint character
    /// should be the same as the overview's viewpoint character (the entire story's
    /// told in first person.) 
    /// 
    /// Theis is presented as a suggestion, not a hard-and-fast rule.
    /// </summary>
    private void GetOverviewViewpoint()
    {
        try
        {
            string viewpointText;
            string viewpointName;

            VpCharTipIsOpen = false;
            StoryModel model = ShellViewModel.GetModel();
            StoryNodeItem node = model.ExplorerView[0];
            OverviewModel overview = (OverviewModel) model.StoryElements.StoryElementGuids[node.Uuid];
            string viewpoint = overview?.Viewpoint;
            if (viewpoint == string.Empty | viewpoint == null)
            {
                viewpointText = "No story viewpoint selected";
            }
            else
            {
                viewpointText = "Story viewpoint = " + viewpoint.ToString();
            }

            var viewpointChar = overview?.ViewpointCharacter;
            if (Guid.TryParse(viewpointChar, out Guid guid))
            {
                viewpointName = "Story viewpoint character = " + model.StoryElements.StoryElementGuids[guid].Name;
            }
            else
            {
                viewpointName = "Story viewpoint character not found";
            }

            var tip = new StringBuilder();
            tip.AppendLine(string.Empty);
            tip.AppendLine(viewpointText);
            tip.AppendLine(viewpointName);
            VpCharTip = tip.ToString();

            // The TeachingTip should only display if there's no scene ViewpointCharcter selected
            if (ViewpointCharacter.Equals(string.Empty))
            {
                VpCharTipIsOpen = true;
                string msg = "ViewpointCharacterTip displayed";
                _logger.Log(LogLevel.Warn, msg);
            }
        }
        catch (Exception ex)
        {
            _logger.LogException(LogLevel.Error,ex,"Error displaying tooltip");
        }

    }
    #endregion

    #region ComboBox ItemsSource collections

    // ListLoader collections
    public ObservableCollection<string> ViewpointList;
    public ObservableCollection<string> SceneTypeList;
    public ObservableCollection<string> ScenePurposeList;
    public ObservableCollection<string> StoryRoleList;
    public ObservableCollection<string> EmotionList;
    public ObservableCollection<string> GoalList;
    public ObservableCollection<string> OppositionList;
    public ObservableCollection<string> OutcomeList;
    public ObservableCollection<string> ValueExchangeList;

    #endregion  

    #region Constructors

    public SceneViewModel()
    {
        _logger = Ioc.Default.GetService<LogService>();
        _wtr = Ioc.Default.GetService<StoryWriter>();
        _rdr = Ioc.Default.GetService<StoryReader>();

        Date = string.Empty;
        Time = string.Empty;
        Setting = string.Empty;
        SceneType = string.Empty;
        CastMembers = new ObservableCollection<StoryElement>();
        ViewpointCharacter = string.Empty;
        ScenePurpose = new ObservableCollection<string>();
        ValueExchange = string.Empty;
        Remarks = string.Empty;
        Protagonist = string.Empty;
        ProtagEmotion = string.Empty;
        ProtagGoal = string.Empty;
        Antagonist = string.Empty;
        AntagEmotion = string.Empty;
        AntagGoal = string.Empty;
        Opposition = string.Empty;
        Outcome = string.Empty;
        Emotion = string.Empty;
        NewGoal = string.Empty;
        Events = string.Empty;
        Consequences = string.Empty;
        Significance = string.Empty;
        Realization = string.Empty;
        Review = string.Empty;
        Notes = string.Empty;

        Dictionary<string, ObservableCollection<string>> lists = GlobalData.ListControlSource;
        ViewpointList = lists["Viewpoint"];
        SceneTypeList = lists["SceneType"];
        ScenePurposeList = lists["ScenePurpose"];
        StoryRoleList = lists["StoryRole"];
        EmotionList = lists["Emotion"];
        GoalList = lists["Goal"];
        OppositionList = lists["Opposition"];
        OutcomeList = lists["Outcome"];
        ViewpointList = lists["Viewpoint"];
        ValueExchangeList = lists["ValueExchange"];

        AddCastCommand = new RelayCommand(AddCastMember, () => true);
        RemoveCastCommand = new RelayCommand(RemoveCastMember, () => true);

        PropertyChanged += OnPropertyChanged;
    }

    #endregion
}