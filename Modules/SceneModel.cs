using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blender_Script_Rendering_Builder.Modules
{
    public class SceneModel
    {
        /// <summary>
        /// The name of the scene
        /// </summary>
        public string SceneName { get; set; }

        /// <summary>
        /// A list of rendering information for the scene what we want to render
        /// </summary>
        public List<RenderModel> RenderData { get; set; }
    }
}
