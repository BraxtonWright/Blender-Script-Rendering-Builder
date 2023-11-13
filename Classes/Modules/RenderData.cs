/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder class Render
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 0.5
 * -----------------------------------------------------------------------------------------------------------
 * This file contains the variables and functions that are required make the rendering data for the UI when
 * outputting to a script file.
 * -----------------------------------------------------------------------------------------------------------
 */

using Blender_Script_Rendering_Builder.Classes.Helpers;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Blender_Script_Rendering_Builder.Classes.Modules
{
    public class RenderData : INotifyPropertyChangedImplmented
    {
        #region Class variables
        #region Render info variables

        //This class immplment a custom enumeration (Enum) that we can use so we can use strings instead of numbers for the value stored inside the Enum.
        // This will also allow us to output the string if we for example console.log one the below options
        // This method doesn't do what I want it to do see about implementing a struct of strings as shown here https://stackoverflow.com/a/59203775 or implmenting the topmost answer?
        /// <summary>
        /// A list of options that are valid for the type of render for the render data
        /// </summary>
        public class RenderTypeOption : Enumeration
        {
            public static RenderTypeOption UseBlender => new(1, "Use Blender configs");
            public static RenderTypeOption Animation => new(2, "Animation");
            public static RenderTypeOption FrameRange => new(3, "Frame Range");
            public static RenderTypeOption CustomFrames => new(4, "Custom Frames");

            public RenderTypeOption(int id, string name) : base(id, name) { }
        }
        private string _renderType;
        /// <summary>
        /// The type of render to be performed, an animation or frames
        /// </summary>
        public string RenderType
        {
            get { return _renderType; }
            set { _renderType = value; }
        }

        #region Frame varaibles
        private int _startFrame;
        /// <summary>
        /// The starting frame for the render
        /// </summary>
        public int StartFrame
        {
            get { return _startFrame; }
            set { _startFrame = value; }
        }

        private int _endFrame;
        /// <summary>
        /// The ending frame for the render
        /// </summary>
        public int EndFrame
        {
            get { return _endFrame; }
            set { _endFrame = value; }
        }

        private string _customFrames;
        /// <summary>
        /// A string that will represent any combination of frames using a ',' or ', ' between entries and a '-' to represent a range of frames (both start and end of the range are inclusive)
        /// </summary>
        public string CustomFrames
        {
            get { return _customFrames; }
            set
            {
                _customFrames = value;
                //OnPropertyChanged(nameof(CustomFrames));
            }
        }
        #endregion

        /// <summary>
        /// A list of valid options to define what rendering engine they want to use.
        /// </summary>
        public class RenderEngineOptions : Enumeration
        {
            public static RenderEngineOptions UseBlender => new(1, "Use Blender configs");
            public static RenderEngineOptions Cycles => new(2, "Cycles");
            public static RenderEngineOptions Eevee => new(3, "Eevee");
            public static RenderEngineOptions Workbench => new(4, "Workbench");

            public RenderEngineOptions(int id, string name) : base(id, name) { }
        }
        private string _renderEngine;
        /// <summary>
        /// The rendering engine that will be used to make the render
        /// </summary>
        public string RenderEngine
        {
            get { return _renderEngine; }
            set { _renderEngine = value; }
        }
        #endregion

        #region Output info variables
        /// <summary>
        /// A list of valid options to define what file type to output
        /// </summary>
        public class OutputFileOptions : Enumeration
        {
            public static OutputFileOptions UseBlender => new(1, "Use Blender configs");
            public static OutputFileOptions AVIJPEG => new(2, "avijpeg");
            public static OutputFileOptions AVIRAW => new(3, "aviraw");
            public static OutputFileOptions BMP => new(4, "bmp");
            public static OutputFileOptions IRIS => new(5, "iris");
            public static OutputFileOptions IRIZ => new(6, "iriz");
            public static OutputFileOptions JPEG => new(7, "jpeg");
            public static OutputFileOptions PNG => new(8, "png");
            public static OutputFileOptions RAWTGA => new(9, "rawtga");
            public static OutputFileOptions TGA => new(10, "tga");

            public OutputFileOptions(int id, string name) : base(id, name) { }
        }
        private string _outputFileType;
        /// <summary>
        /// The file type that the render will output
        /// </summary>
        public string OutputFileType
        {
            get { return _outputFileType; }
            set
            {
                _outputFileType = value;
                //OnPropertyChanged(nameof(OutputFileType));
            }
        }

        /// <summary>
        /// A list of valid options to define what output folder they want to use.
        /// </summary>
        public class OutputFolderOptions : Enumeration
        {
            public static OutputFolderOptions UseBlender => new(1, "Use Blender configs");
            public static OutputFolderOptions Browse => new(2, "Browse for folder");

            public OutputFolderOptions(int id, string name) : base(id, name) { }
        }
        private string _outputPathSelection;
        /// <summary>
        /// Whether  to choose to use what output folder blender uses or to browse for the folder
        /// </summary>
        public string OutputPathSelection
        {
            get { return _outputPathSelection; }
            set { _outputPathSelection = value; }
        }

        private string _outputFullPath;
        /// <summary>
        /// The full path to the folder that the renders will be outputed to
        /// </summary>
        public string OutputFullPath
        {
            get { return _outputFullPath; }
            set
            {
                _outputFullPath = value;
                OutputFolderName = GetFolderName(value);  // Set the property 'OutputFolderName" to be the name of the folder
            }
        }

        private string _outputFolderName;
        /// <summary>
        /// The name of the folder that the renders will be outputed to
        /// </summary>
        public string OutputFolderName
        {
            get { return _outputFolderName; }
            private set  // We have this set as private because we don't want to have it be set from outside this file.  This is because when the OutputFullPath property changes, this property is automatically updated.
            {
                _outputFolderName = value;
                OnPropertyChanged(nameof(OutputFolderName));
            }
        }
        #endregion
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public RenderData()
        {
            
        }

        /// <summary>
        /// Private constructor so we simply inherite from it using the below two constructors for the common fields
        /// </summary>
        /// <param name="outputFileType">The output file type</param>
        /// <param name="outputFullPath">The output folder path</param>
        /// <param name="renderEngine">The render engine to use</param>
        private RenderData(string outputFileType, string outputFullPath, string renderEngine)
        {
            try
            {
                _outputFileType = outputFileType;
                _outputFullPath = outputFullPath;
                _renderEngine = renderEngine;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Overloaded constructor for renders with a start and end frames
        /// </summary>
        /// <param name="startFrame">Starting frame to render from</param>
        /// <param name="endFrame">Ending frame to render to</param>
        /// <param name="outputFileType">The output file type</param>
        /// <param name="outputFullPath">The output folder path</param>
        /// <param name="renderEngine">The render engine to use</param>
        public RenderData(int startFrame, int endFrame, string outputFileType, string outputFullPath, string renderEngine) : this(outputFileType, outputFullPath, renderEngine)
        {
            try
            {
                _startFrame = startFrame;
                _endFrame = endFrame;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Overloaded constructor for renders with a custom defined set of frames
        /// </summary>
        /// <param name="customFrames">A string to represent a custom set of frames to render</param>
        /// <param name="outputFileType">The output file type</param>
        /// <param name="outputFullPath">The output folder path</param>
        /// <param name="renderEngine">The render engine to use</param>
        public RenderData(string customFrames, string outputFileType, string outputFullPath, string renderEngine) : this(outputFileType, outputFullPath, renderEngine)
        {
            try
            {
                _customFrames = customFrames;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region Functions
        /// <summary>
        /// Grabs the name of the file from the full path to the file.
        /// Source (using a file name, but the same thing applies here) https://forum.uipath.com/t/regex-getting-filename-out-from-filepath/190312/3
        /// </summary>
        /// <param name="FullPath">The full path to the folder</param>
        /// <returns>The name of the folder</returns>
        private string GetFolderName(string FullPath)
        {
            try
            {
                return System.IO.Path.GetFileName(FullPath);
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                return "";
            }
        }
        #endregion
    }
}
