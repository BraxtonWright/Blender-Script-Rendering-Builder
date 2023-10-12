/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder class clsRenderInfoLogic
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the logistic for the UserControl RenderInfo so that the logistics is not behind the UI.
 * -----------------------------------------------------------------------------------------------------------
 */

using Blender_Script_Rendering_Builder.Shared;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Blender_Script_Rendering_Builder.UserControls.Render_Info
{
    class clsRenderInfoLogic
    {
        #region Class Variables
        /// <summary>
        /// Will contain all the data about the rendering info found on the UI
        /// </summary>
        public RenderInstance renderInstance;
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public clsRenderInfoLogic()
        {
            try
            {
                renderInstance = new RenderInstance();
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region Functions
        /// <summary>
        /// Makes a list of valid options to define what type of render the user wants to do.
        /// </summary>
        /// <returns>A list of valid options for the user to chose what render they want to do.</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across.</exception>
        public List<string> AnimationOrFrameList()
        {
            try
            {
                // Make a list of options
                List<string> list = new List<string>
                {
                    "Use Blender configs",
                    "Animation",
                    "Frames",
                    "Frames (custom)"
                };

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Makes a list of valid options to define what output file type they want to output.
        /// </summary>
        /// <returns>A list of valid options for the user to chose what output file type they want to output.</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across.</exception>
        public List<string> OutputFileTypeList()
        {
            try
            {
                // Make a list of options
                List<string> list = new List<string>
                {
                    "Use Blender configs",
                    "avijpeg",
                    "aviraw",
                    "bmp",
                    "iris",
                    "iriz",
                    "jpeg",
                    "png",
                    "rawtga",
                    "tga"
                };

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Makes a list of valid options to define what rendering engine they want to use.
        /// </summary>
        /// <returns>A list of valid options for the user to chose what rendering engine they want to use.</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across.</exception>
        public List<string> RenderingEngineList()
        {
            try
            {
                // Make a list of options
                List<string> list = new List<string>
                {
                    "Use Blender configs",
                    "Cycles",
                    "Eevee",
                    "Workbench"
                };

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
    }
}
