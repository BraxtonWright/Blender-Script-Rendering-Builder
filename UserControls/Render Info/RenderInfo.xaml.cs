/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder UserControl RenderInfo
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the required event listeners for the UserControl RenderInfo.
 * -----------------------------------------------------------------------------------------------------------
 */
using Blender_Script_Rendering_Builder.Main;
using Blender_Script_Rendering_Builder.Shared;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Controls;

namespace Blender_Script_Rendering_Builder.UserControls.Render_Info
{
    /// <summary>
    /// Interaction logic for RenderInfo.xaml
    /// </summary>
    public partial class RenderInfo : UserControl
    {
        #region Variables
        /// <summary>
        /// Object to perform logic for the RenderInfo UserControl.
        /// </summary>
        private clsRenderInfoLogic logic;

        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public RenderInfo()
        {
            try
            {
                InitializeComponent();
                logic = new clsRenderInfoLogic();
                FillComboBoxes();
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion

        #region Event Listeners
        /// <summary>
        /// Deletes the user control from the parent's stack panel
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void btnRenderInfoDelete_Click(object sender, System.Windows.RoutedEventArgs e)
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
        #endregion

        #region Helper Functions
        /// <summary>
        /// Populates all the ComboBoxes with the valid options
        /// Call this function before showing this window
        /// </summary>
        /// <exception cref="Exception">Catches any exceptions that this method might come across.</exception>
        private void FillComboBoxes()
        {
            try
            {
                cmbAnimationOrFrame.ItemsSource = logic.AnimationOrFrameList();
                cmbOutputFileType.ItemsSource = logic.OutputFileTypeList();
                cmbRenderEngine.ItemsSource = logic.RenderingEngineList();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
    }
}
