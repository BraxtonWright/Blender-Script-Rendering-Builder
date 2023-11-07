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

using System.Collections.Generic;

namespace Blender_Script_Rendering_Builder.Classes.Modules
{
    public class Scene : INotifyPropertyChangedImplmented
    {
        #region Class variables
        private string _sceneName;
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

        /// <summary>
        /// A list of rendering information for the scene
        /// </summary>
        public List<Render> renderInfo = new List<Render>();
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Scene()
        {
            
        }

        /// <summary>
        /// Overloaded constructor to define both the scene's name and the rendering information for that scene
        /// </summary>
        /// <param name="sceneName">The name of the scene</param>
        /// <param name="renderInfo">A list of rendering information for the scene</param>
        public Scene(string sceneName, List<Render> renderInfo)
        {
            SceneName = sceneName;
            this.renderInfo = renderInfo;
        }
        #endregion

        #region Functions
        #endregion
    }
}
