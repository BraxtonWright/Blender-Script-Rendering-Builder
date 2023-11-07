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

using Blender_Script_Rendering_Builder.Classes.Shared;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Blender_Script_Rendering_Builder.Classes.Modules
{
    public class Render : INotifyPropertyChangedImplmented
    {
        #region Class variables
        private int _startFrame;
        /// <summary>
        /// The starting frame for the render
        /// </summary>
        public int StartFrame
        {
            get { return _startFrame; }
            set
            {
                _startFrame = value;
                OnPropertyChanged(nameof(StartFrame));
            }
        }

        private int _endFrame;
        /// <summary>
        /// The ending frame for the render
        /// </summary>
        public int EndFrame
        {
            get { return _endFrame; }
            set
            {
                _endFrame = value;
                OnPropertyChanged(nameof(EndFrame));
            }
        }

        private string _customFrames;
        /// <summary>
        /// A string that will represent any combination of frames using a ',' between entries and a '-' to represent a range of frames
        /// </summary>
        public string CustomFrames
        {
            get { return _customFrames; }
            set
            {
                _customFrames = value;
                OnPropertyChanged(nameof(CustomFrames));
            }
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
                OnPropertyChanged(nameof(OutputFileType));
            }
        }

        private string _outputFullPath;
        /// <summary>
        /// The full path to the output folder
        /// </summary>
        public string OutputFullPath
        {
            get { return _outputFullPath; }
            set
            {
                _outputFullPath = value;
                OnPropertyChanged(nameof(OutputFullPath));
                OutputFolderName = GetFolderName(value);
            }
        }

        private string _outputFolderName;
        public string OutputFolderName
        {
            get { return _outputFolderName; }
            private set  // We have this set as private because we don't want to have it be set from outside this file.  This is because when the OutputFullPath property changes, this property is automatically updated.
            {
                _outputFolderName = value;
                OnPropertyChanged(nameof(OutputFolderName));
            }
        }

        private string _renderEngine;
        /// <summary>
        /// The rendering engine that will be used to make the render
        /// </summary>
        public string RenderEngine
        {
            get { return _renderEngine; }
            set
            {
                _renderEngine = value;
                OnPropertyChanged(nameof(RenderEngine));
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Render()
        {

        }

        /// <summary>
        /// Private constructor so we simply inherite from it using the below two constructors for the common fields
        /// </summary>
        /// <param name="outputFileType">The output file type</param>
        /// <param name="outputFullPath">The output folder path</param>
        /// <param name="renderEngine">The render engine to use</param>
        private Render(string outputFileType, string outputFullPath, string renderEngine)
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
        public Render(int startFrame, int endFrame, string outputFileType, string outputFullPath, string renderEngine) : this(outputFileType, outputFullPath, renderEngine)
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
        public Render(string customFrames, string outputFileType, string outputFullPath, string renderEngine) : this(outputFileType, outputFullPath, renderEngine)
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
