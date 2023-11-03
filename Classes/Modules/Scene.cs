/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder class Scene
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
using System.Reflection;

namespace Blender_Script_Rendering_Builder.Classes.Modules
{
    public class Scene : INotifyPropertyChangedImplmented
    {
        #region Private class variables
        private string _sceneName;
        private string _defaultSceneText = "Scene's name (spaces not allowed)...";
        #endregion

        #region Getters/Setters
        /// <summary>
        /// The name of the scene
        /// </summary>
        public string SceneName {
            get { return _sceneName; }
            set
            {
                _sceneName = value;
                OnPropertyChanged(nameof(SceneName));
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Scene()
        {
            SceneName = _defaultSceneText;
        }
        #endregion

        #region Functions
        /// <summary>
        /// Extract all the nessary information required for the script
        /// </summary>
        /// <returns>The name of the scene</returns>
        public string ExtractData()
        {
            return SceneName;
        }
        #endregion
    }
}
