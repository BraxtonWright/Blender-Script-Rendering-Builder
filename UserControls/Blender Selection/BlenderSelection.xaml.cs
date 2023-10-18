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
using Blender_Script_Rendering_Builder.Classes.Shared;
using Blender_Script_Rendering_Builder.Classes.View_Models;
using Blender_Script_Rendering_Builder.UserControls.Scene_Selection;
using Microsoft.Win32;
using System;
using System.Reflection;
using System.Windows.Controls;

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
        clsBlenderSelectionLogic logic;
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
                logic = new clsBlenderSelectionLogic();
                //DataContext = this;  // Set the data context for the window to be itself
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
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event's information, I.E. a Routed Event.</param>
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
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event's information, I.E. a Routed Event.</param>
        private void btnNewScene_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                spScenes.Children.Add(new SceneSelection());
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This event listener will listen for when you press the button to browse for a blender file.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event's information, I.E. a Routed Event.</param>
        private void btnBlendBrowse_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Blender files (*.blend)|*.blend";
                if (openFileDialog.ShowDialog() == true)
                {
                    //? = openFileDialog.FileName;
                    //lblFileName.Content = logic.ExtractFileName(blenderData.FullPath);
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
