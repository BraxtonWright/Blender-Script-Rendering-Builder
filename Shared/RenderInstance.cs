using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blender_Script_Rendering_Builder.Shared
{
    /// <summary>
    /// The ':' makes it so this class inherits from the CommonRenderInfo class, to prevent duplicates of code for common variables
    /// </summary>
    class RenderInstance : CommonRenderInfo
    {
        #region Class Variables
        /// <summary>
        /// The starting frame for the render
        /// </summary>
        public int startFrame;

        /// <summary>
        /// The ending frame for the render
        /// </summary>
        public int endFrame;

        /// <summary>
        /// A string that will represent any combination of frames using a ',' between entries and a '-' to represent a range of frames
        /// </summary>
        public string customFrames;
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public RenderInstance()
        {

        }

        /// <summary>
        /// Overloaded constructor that accepts all the information for a start/end frame render
        /// </summary>
        /// <param name="startFrame">Starting frame for the render</param>
        /// <param name="endFrame">Ending frame for the render</param>
        /// <param name="outputFileType">The file type the render will make</param>
        /// <param name="outputFullPath">The full path to the folder to save the renders</param>
        /// <param name="renderEngine">The rendering engine to use when making the renders</param>
        public RenderInstance(int startFrame, int endFrame, string outputFileType, string outputFullPath, string renderEngine) : base(outputFileType, outputFullPath, renderEngine)
        {
            try
            {
                this.startFrame = startFrame;
                this.endFrame = endFrame;
                /*this.outputFileType = outputFileType;
                this.outputFullPath = outputFullPath;
                this.renderEngine = renderEngine;*/
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                             MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Overloaded constructor that accepts all the information for a start/end frame render
        /// </summary>
        /// <param name="customFrames">A string to represent a more advanced set of rendering information with each separated by a ',' and a range of frames separated by a '-'</param>
        /// <param name="outputFileType">The file type the render will make</param>
        /// <param name="outputFullPath">The full path to the folder to save the renders</param>
        /// <param name="renderEngine">The rendering engine to use when making the renders</param>
        public RenderInstance(string customFrames, string outputFileType, string outputFullPath, string renderEngine) : base(outputFileType, outputFullPath, renderEngine)
        {
            try
            {
                this.customFrames = customFrames;
                /*this.outputFileType = outputFileType;
                this.outputFullPath = outputFullPath;
                this.renderEngine = renderEngine;*/
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                               MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion
    }

    /// <summary>
    /// The "abstract" at the start of this will make it sot that you can't make an instance of this class, you can only inherit from it
    /// </summary>
    abstract class CommonRenderInfo
    {
        #region Class Variables
        /// <summary>
        /// The file type that the render will output
        /// </summary>
        public string outputFileType;

        /// <summary>
        /// The full path to the output folder
        /// </summary>
        public string outputFullPath;

        /// <summary>
        /// The rendering engine that will be used to make the render
        /// </summary>
        public string renderEngine;
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public CommonRenderInfo()
        {

        }

        /// <summary>
        /// Overloaded constructor that accepts all the common rendering information for a render
        /// </summary>
        /// <param name="outputFileType">The file type the render will make</param>
        /// <param name="outputFullPath">The full path to the folder to save the renders</param>
        /// <param name="renderEngine">The rendering engine to use when making the renders</param>
        public CommonRenderInfo(string outputFileType, string outputFullPath, string renderEngine)
        {
            try
            {
                this.outputFileType = outputFileType;
                this.outputFullPath = outputFullPath;
                this.renderEngine = renderEngine;
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                                MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion
    }
}
