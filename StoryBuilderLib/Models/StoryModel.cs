using System.Collections.ObjectModel;
using StoryBuilder.Models.Tools;
using StoryBuilder.ViewModels;

//using StoryBuilder.Models.Tools;

namespace StoryBuilder.Models
{
    public class StoryModel
    {
        // TODO: Note sorting filtering and grouping depend on ICollectionView (for TreeView?)
        // TODO: See http://msdn.microsoft.com/en-us/library/ms752347.aspx#binding_to_collections
        // TODO: Maybe http://stackoverflow.com/questions/15593166/binding-treeview-with-a-observablecollection
        /// <summary>
        /// If any of the story entities have been modified by other than retrieval from the Story
        /// (that is, by a user modification from a View) 'Changed' is set to true. That is, a change
        /// to OverViewModel, or any ProblemModel, CharacterModel, SettingModel, PlotPointModel, or
        /// FolderModel will result in Changed being set to true. This amounts to a 'dirty' bit that
        /// indicates the StoryModel needs to be written to its backing store. 
        ///
        /// It's necessary for View controls using source binding to the entity collections to be aware
        /// of changes (additions and deletions) to them. For example, the Problem view Protagonist
        /// and Antagonist tabs contain drop down lists of CharacterNames from CharacterList.
        /// This code uses a (source provided) ObservableSortedDictionary which implements the full
        /// SortedDictionary functionality but also implemeents INotifyPropertyChanged and 
        /// INotifyCollectionChanged (that is, is an ObservableCollection.) See the included source
        /// files for Copyright.  
        /// See http://drwpf.com/blog/2007/09/16/can-i-bind-my-itemscontrol-to-a-dictionary/ for details.
        /// One comment on above is http://weblogs.asp.net/pwelter34/archive/2006/05/03/444961.aspx for XML
        /// serialization.
        /// </summary>
        public bool Changed;

        #region StoryExplorer and NarratorView (TreeView) properties

        /// The StoryModel contains two persisted TreeView representations, a Story Explorer tree which
        /// contains all Story Elements (the StoryOverview and all Problem, Character, Setting, PlotPoint
        /// and Folder elements) and a Narrator View which contains just Section (chapter, etc) and
        /// selected PlotPoint elements. 
        /// 
        /// The active TreeView in the Shell page view is bound to a StoryNodeItem tree based on
        /// whichever of these two TreeView representations is presently  selected.

        public ObservableCollection<StoryNodeItem> ExplorerView = new ObservableCollection<StoryNodeItem>();

        public ObservableCollection<StoryNodeItem> NarratorView = new ObservableCollection<StoryNodeItem>();

        public ObservableCollection<CharacterRelationshipsModel> RelationList;

        #endregion

        #region Constructor
        public StoryModel()
        {
            RelationList = new ObservableCollection<CharacterRelationshipsModel>();
            Changed = false;
        }

        #endregion
    }
}