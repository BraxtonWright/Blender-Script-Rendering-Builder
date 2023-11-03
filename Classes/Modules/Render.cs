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

using System;
using System.ComponentModel;

namespace Blender_Script_Rendering_Builder.Classes.Modules
{
    public class Render : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged members
        /// <summary>
        /// This is the contract we have to make with the compiler because we are implementing the interface "INotifyPropertyChanged".  So we must have this event defined.  We will raise this event anytime one of our properties changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// A reusable set of code so that we can attach the PropertyChangedEventHandler to the below properties, without having to type out this code multiple times
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Private class variables
        private int _startFrame;
        private int _endFrame;
        private string _customFrames;
        private string _outputFileType;
        private string _outputFullPath;
        private string _renderEngine;
        #endregion

        #region Getters/Setters for private class variables
        /// <summary>
        /// The starting frame for the render
        /// </summary>
        public int StartFrame
        {
            get { return _startFrame; }
            set
            {
                _startFrame = value;
                OnPropertyChanged("StartFrame");
            }
        }
        /// <summary>
        /// The ending frame for the render
        /// </summary>
        public int EndFrame
        {
            get { return _endFrame; }
            set
            {
                _endFrame = value;
                OnPropertyChanged("EndFrame");
            }
        }

        /// <summary>
        /// A string that will represent any combination of frames using a ',' between entries and a '-' to represent a range of frames
        /// </summary>
        public string CustomFrames
        {
            get { return _customFrames; }
            set
            {
                _customFrames = value;
                OnPropertyChanged("CustomFrames");
            }
        }

        /// <summary>
        /// The file type that the render will output
        /// </summary>
        public string OutputFileType
        {
            get { return _outputFileType; }
            set
            {
                _outputFileType = value;
                OnPropertyChanged("OutputFileType");
            }
        }

        /// <summary>
        /// The full path to the output folder
        /// </summary>
        public string OutputFullPath
        {
            get { return _outputFullPath; }
            set
            {
                _outputFullPath = value;
                OnPropertyChanged("OutputFullPath");
            }
        }

        /// <summary>
        /// The rendering engine that will be used to make the render
        /// </summary>
        public string RenderEngine
        {
            get { return _renderEngine; }
            set
            {
                _renderEngine = value;
                OnPropertyChanged("RenderEngine");
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
        /// Overloaded constructor for renders with a start and end frames
        /// </summary>
        /// <param name="startFrame">Starting frame to render from</param>
        /// <param name="endFrame">Ending frame to render to</param>
        /// <param name="outputFileType">The output file type</param>
        /// <param name="outputFullPath">The output folder path</param>
        /// <param name="renderEngine">The render engine to use</param>
        public Render(int startFrame, int endFrame, string outputFileType, string outputFullPath, string renderEngine)
        {
            _startFrame = startFrame;
            _endFrame = endFrame;
            _outputFileType = outputFileType;
            _outputFullPath = outputFullPath;
            _renderEngine = renderEngine;
        }

        /// <summary>
        /// Overloaded constructor for renders with a custom defined set of frames
        /// </summary>
        /// <param name="customFrames">A string to represent a custom set of frames to render</param>
        /// <param name="outputFileType">The output file type</param>
        /// <param name="outputFullPath">The output folder path</param>
        /// <param name="renderEngine">The render engine to use</param>
        public Render(string customFrames, string outputFileType, string outputFullPath, string renderEngine)
        {
            _customFrames = customFrames;
            _outputFileType = outputFileType;
            _outputFullPath = outputFullPath;
            _renderEngine = renderEngine;
        }
        #endregion
    }
}
