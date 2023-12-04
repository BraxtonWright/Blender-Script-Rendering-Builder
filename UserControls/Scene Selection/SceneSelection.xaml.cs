/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder UserControl SceneSelection
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 1.0
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the required event listeners for the UserControl SceneSelection.
 * -----------------------------------------------------------------------------------------------------------
 */

using Blender_Script_Rendering_Builder.Classes.Modules;
using Blender_Script_Rendering_Builder.Classes.Helpers;
using Blender_Script_Rendering_Builder.UserControls.Render_Selection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input.StylusPlugIns;
using System.Windows;

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
        /// Object to contain all the data necessary for the scene
        /// </summary>
        private SceneData sceneData;
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
                sceneData = new SceneData();
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
        /// This event listener will listen for then the user control is finished being loaded and once it is done, it will collapse the expander named expScene
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void ucSceneSelection_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                expScene.IsExpanded = false;
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This event listener will listen for when the scene is being deleted
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
        /// This event listener will add a new section to add 
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void btnNewRenderInfo_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                spRenderingInfo.Children.Add(new RenderSelection());
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This event listener will hide or show the placeholder text for the text box depending on if the text box is empty
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
                ValidatorsReturn results = Validators.SceneNameValid(txtSceneName.Text);

                // The SetResourceReference will search the *.xmal file located in the "Themes" folder for the resource with the name defined in the second argument.  This will make it so if you switch theme's while there is data, and the background color will be updated.  Uses a combination of https://stackoverflow.com/a/1754658 and https://stackoverflow.com/a/53463353 to achieve this.
                // The input is valid
                if (results.Valid)
                {
                    txtSceneName.SetResourceReference(Control.BackgroundProperty, "ControlBackgroundBrush");

                    txtSceneName.ToolTip = null;  // Removes the tool tip from the text box so the error message will no longer show when you hover over it
                }
                // The input is invalid
                else
                {
                    txtSceneName.SetResourceReference(Control.BackgroundProperty, "ErrorFillBrush");

                    ToolTip errorTooltip = new ToolTip { Content = results.ErrorMessage };
                    txtSceneName.ToolTip = errorTooltip;
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
        /// <returns>A new instance of the class SceneData containing all the necessary information required for the render</returns>
        public SceneData GetRenderingInfo()
        {
            // The reason why we are creating a new instance of the object is because with the current implementation of this program, I do not at runtime add/remove items from the "sceneData.rendersInfo".  If I were to simply set the variable "returnData" to be equal to the variable "sceneData", it would create a reference to it and then when we would add items to the "rendersInfo" section inside "returnData", it would also add them to the "sceneData" variable.  However, I also cannot simply make a new insane of the list of rendering information using the same method as the blender file's full path, it would still create a referee because it is a list.  So i create a new empty list of data and add to it.
            SceneData returnData = new SceneData(sceneData.SceneName, new List<RenderData>());

            foreach(RenderSelection renderSelection in spRenderingInfo.Children)
            {
                returnData.rendersInfo.Add(renderSelection.GetRenderingInfo());
            }

            return returnData;
        }
        #endregion
    }
}
