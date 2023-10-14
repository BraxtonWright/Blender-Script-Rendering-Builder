using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blender_Script_Rendering_Builder.Modules
{
    class RenderModel
    {
        #region Class Variables
        private int _startFrame;
        private int _endFrame;
        private string _customFrames;
        private string _outputFileType;
        private string _outputFullPath;
        private string _renderEngine;

        /// <summary>
        /// The starting frame for the render
        /// </summary>
        public int StartFrame
        {
            get { return _startFrame; }
            set { _startFrame = value; }
        }

        /// <summary>
        /// The ending frame for the render
        /// </summary>
        public int EndFrame
        {
            get { return _endFrame; }
            set { _endFrame = value; }
        }

        /// <summary>
        /// A string that will represent any combination of frames using a ',' between entries and a '-' to represent a range of frames
        /// </summary>
        public string CustomFrames
        {
            get { return _customFrames; }
            set { _customFrames = value; }
        }

        /// <summary>
        /// The file type that the render will output
        /// </summary>
        public string OutputFileType
        {
            get { return _outputFileType; }
            set { _outputFileType = value; }
        }

        /// <summary>
        /// The full path to the output folder
        /// </summary>
        public string OutputFullPath
        {
            get { return _outputFullPath; }
            set { _outputFullPath = value; }
        }

        /// <summary>
        /// The rendering engine that will be used to make the render
        /// </summary>
        public string RenderEngine
        {
            get { return _renderEngine; }
            set { _renderEngine = value; }
        }
        #endregion
    }
}
