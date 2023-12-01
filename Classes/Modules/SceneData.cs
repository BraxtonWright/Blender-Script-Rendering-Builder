/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder class SceneData
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 1.0
 * -----------------------------------------------------------------------------------------------------------
 * This file contains the variables and functions that are required make the scene data for the UI when
 * outputting to a script file.
 * -----------------------------------------------------------------------------------------------------------
 */

using System.Collections.Generic;
using Blender_Script_Rendering_Builder.Classes.Helpers;

namespace Blender_Script_Rendering_Builder.Classes.Modules
{
    public class SceneData : INotifyPropertyChangedImplmented
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
        public List<RenderData> rendersInfo = new List<RenderData>();
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public SceneData()
        {
            
        }

        /// <summary>
        /// Overloaded constructor to define both the scene's name and the rendering information for that scene
        /// </summary>
        /// <param name="sceneName">The name of the scene</param>
        /// <param name="renderInfo">A list of rendering information for the scene</param>
        public SceneData(string sceneName, List<RenderData> renderInfo)
        {
            SceneName = sceneName;
            this.rendersInfo = renderInfo;
        }
        #endregion

        #region Functions
        #endregion
    }
}
