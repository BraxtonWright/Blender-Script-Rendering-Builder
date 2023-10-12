using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blender_Script_Rendering_Builder.Shared
{
    class SceneInstance
    {
        #region Class Variables
        /// <summary>
        /// The name of the scene
        /// </summary>
        public string sceneName { get; set; }

        /// <summary>
        /// A list of rendering information for the scene what we want to render
        /// </summary>
        public List<RenderInstance> renderData { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SceneInstance()
        {

        }

        /// <summary>
        /// Overloaded constructor that accepts the name of the scene and rendering information for that scene
        /// </summary>
        /// <param name="sceneName">The name of the scene</param>
        /// <param name="renderData">A list of rendering information for this scene</param>
        public SceneInstance(string sceneName, List<RenderInstance> renderData)
        {
            try
            {
                this.sceneName = sceneName;
                this.renderData = renderData;
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
    }
}
