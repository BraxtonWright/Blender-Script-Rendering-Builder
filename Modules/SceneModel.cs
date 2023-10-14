using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blender_Script_Rendering_Builder.Modules
{
    class SceneModel
    {
        private string _sceneName;
        private List<RenderModel> _renders;

        /// <summary>
        /// The name of the scene
        /// </summary>
        public string SceneName
        {
            get { return _sceneName; }
            set { _sceneName = value; }
        }

        /// <summary>
        /// A list of rendering information for the scene what we want to render
        /// </summary>
        public List<RenderModel> Renders
        {
            get { return _renders; }
            set { _renders = value; }
        }
    }
}
