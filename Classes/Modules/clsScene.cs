/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder class clsScene
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 0.5
 * -----------------------------------------------------------------------------------------------------------
 * This file contains the variables and functions that are required make the scene data for the UI when
 * outputting to a script file.
 * -----------------------------------------------------------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Blender_Script_Rendering_Builder.Classes.Modules
{
    public class clsScene : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged members
        /// <summary>
        /// This is the contract we have to make with the compiler because we are implementing the interface "INotifyPropertyChanged".  So we must have this event defined.  We will raise this event anytime one of our properties changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// A reusable set of code so that we can attach the PropertyChangedEventHandler to the below properties, without having to type out this code multiple times
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Private class variables
        private string _sceneName;
        private List<clsRender> _renderData;
        #endregion
        /// <summary>
        /// The name of the scene
        /// </summary>
        public string SceneName {
            get { return _sceneName; }
            set
            {
                _sceneName = value;
                OnPropertyChanged("SceneName");
            }
        }

        /// <summary>
        /// A list of rendering information for the scene what we want to render
        /// </summary>
        public List<clsRender> RenderData
        {
            get { return _renderData; }
            set
            {
                _renderData = value;
                OnPropertyChanged("RenderData");
            }
        }

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public clsScene()
        {
            _renderData = new List<clsRender>();
        }

        /// <summary>
        /// Overloaded constructor for the for scenes with a name and a list of instances of the class RenderModel
        /// </summary>
        /// <param name="sceneName">The name of the scene</param>
        /// <param name="renderData">A list of rendering information for the scene</param>
        public clsScene(string sceneName, List<clsRender> renderData)
        {
            _sceneName = sceneName;
            _renderData = renderData;
        }
        #endregion
    }
}
