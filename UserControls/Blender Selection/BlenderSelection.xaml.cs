/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder UserControl BlenderSelection
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 1.0
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the required event listeners for the UserControl BlenderSelection.
 * -----------------------------------------------------------------------------------------------------------
 */

using Blender_Script_Rendering_Builder.Classes.Modules;
using Blender_Script_Rendering_Builder.Classes.Helpers;
using Blender_Script_Rendering_Builder.UserControls.Scene_Selection;
using Microsoft.Win32;
using System;
using System.Reflection;
using System.Windows.Controls;
using System.Collections.Generic;

namespace Blender_Script_Rendering_Builder.UserControls.Blender_Selection
{
    /// <summary>
    /// Interaction logic for BlenderSelection.xaml
    /// </summary>
    public partial class BlenderSelection : UserControl
    {
        #region Variables
        /// <summary>
        /// Object to perform logic for the BlenderSelection UserControl.
        /// </summary>
        private BlenderSelectionLogic logic;

        /// <summary>
        /// Object to contain all the data necessary for the blender file
        /// </summary>
        private BlenderData blendData;
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BlenderSelection()
        {
            try
            {
                InitializeComponent();
                logic = new BlenderSelectionLogic();
                blendData = new BlenderData();
                DataContext = blendData;
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region Event Listeners
        /// <summary>
        /// This event listener will listen for then the user control is finished being loaded and once it is done, it will collapse the expander named expBlender
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void ucBlenderSelection_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                expBlender.IsExpanded = false;
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// Deletes the user control from the parent's stack panel
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void btnDeleteBlend_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                // Grab the current instance of this class by using the "this" keyword
                UserControl UC = this;
                //Grab the parent of the UserControl, cast it as a StackPanel, and remove the UserControl from it
                ((StackPanel)(UC.Parent)).Children.Remove(UC);
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Adds a new scene user control to the stack panel
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void btnNewScene_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                SceneSelection newScene = new SceneSelection();  // Build a new user control
                spScenes.Children.Add(newScene);  // add the new user control the stack panel
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This event listener will listen for when you press the button to browse for a blender file.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void btnBlendBrowse_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Blender files (*.blend)|*.blend";
                if (openFileDialog.ShowDialog() == true)
                {
                    blendData.FullPath = openFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion

        #region Functions
        /// <summary>
        /// Grab all the necessary information required for rendering
        /// </summary>
        /// <returns>An instance of the class SceneData containing all the necessary information required for the render</returns>
        public BlenderData GetRenderingInfo()
        {
            // The reason why we are creating a new instance of the object is because I do not at run time, add/remove items from the "scenesInfo" portion of the object.  If I were to simply set the variable "returnData" to be equal to the varaible "blendData", it would create a reference to it and then when we would add items to the "scenesInfo" section inside "returnData", it would also add them to the "blendData" varaible.  However, I also cannot simply make a new insance of the list of scenes using the same method as the blender file's full path, it would still create a referece because it is a list.  So i create a new empty list of data and add to it.
            BlenderData returnData = new BlenderData(blendData.FullPath, new List<SceneData>());

            foreach (SceneSelection sceneSelection in spScenes.Children)
            {
                returnData.scenesInfo.Add(sceneSelection.GetRenderingInfo());
            }

            return returnData;
        }
        #endregion
    }
}
