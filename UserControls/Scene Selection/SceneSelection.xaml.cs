/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder UserControl SceneSelection
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the required event listeners for the UserControl SceneSelection.
 * -----------------------------------------------------------------------------------------------------------
 */

using Blender_Script_Rendering_Builder.Shared;
using Blender_Script_Rendering_Builder.UserControls.Render_Info;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Controls;

namespace Blender_Script_Rendering_Builder.UserControls.Scene_Selection
{
    /// <summary>
    /// Interaction logic for SceneSelection.xaml
    /// </summary>
    public partial class SceneSelection : UserControl
    {
        #region Variables
        /// <summary>
        /// Object to perform logic for the SceneSelection UserControl.
        /// </summary>
        clsSceneSelectionLogic logic;
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SceneSelection()
        {
            try
            {
                InitializeComponent();
                logic = new clsSceneSelectionLogic();
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                      MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion

        #region Event Listeners
        private void btnSceneDelete_Click(object sender, System.Windows.RoutedEventArgs e)
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
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                       MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void btnNewRenderInfo_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                spRenderingInfo.Children.Add(new RenderInfo());
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
