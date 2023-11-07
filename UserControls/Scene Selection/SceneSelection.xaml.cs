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

using Blender_Script_Rendering_Builder.Classes.Modules;
using Blender_Script_Rendering_Builder.Classes.Shared;
using Blender_Script_Rendering_Builder.UserControls.Render_Info;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;

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
        private SceneSelectionLogic logic;

        /// <summary>
        /// Object to contain all the data nessary for the scene
        /// </summary>
        private Scene sceneData;
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
                logic = new SceneSelectionLogic();
                sceneData = new Scene();
                DataContext = sceneData;
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region Event Listeners
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
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
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void btnNewRenderInfo_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                spRenderingInfo.Children.Add(new RenderInfo());
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This event listener will hide or show the placeholder text for the textbox depending on if the textbox is empty
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Text Changed Event</param>
        private void txtSceneName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtSceneName.Text.Length < 1)
                {
                    lblSceneNamePlacholder.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    lblSceneNamePlacholder.Visibility = System.Windows.Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Determines if the scene name is valid, I.E. it does not contain any spaces inside it
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void txtSceneName_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                // The below regex command searches for spaces inside the text
                Match regexResults = Regex.Match(txtSceneName.Text, " ");
                // The input contains a space, so it is invalid
                if (regexResults.Success)
                {
                    txtSceneName.Background = new SolidColorBrush(Color.FromArgb(255, 255, 128, 128));
                }
                // The input is valid
                else
                {
                    txtSceneName.Background = Brushes.White;
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
