/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder UserControl BlenderSelection
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 0.5
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
        /// Object to contain all the data nessary for the blender file
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
                SceneSelection newScene = new SceneSelection();  // Build a new usercontrol
                spScenes.Children.Add(newScene);  // add the new usercontrol the the stack panel
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
    }
}
