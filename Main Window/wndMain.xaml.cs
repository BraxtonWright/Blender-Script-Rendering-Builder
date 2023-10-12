/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder Window MainWindow
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the required event listeners for the main window.
 * -----------------------------------------------------------------------------------------------------------
 */

using Blender_Script_Rendering_Builder.Main;
using Blender_Script_Rendering_Builder.Shared;
using Blender_Script_Rendering_Builder.UserControls.Blender_Selection;
using System;
using System.Reflection;
using System.Windows;

namespace Blender_Script_Rendering_Builder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables
        /// <summary>
        /// Object to perform logic for the main window.
        /// </summary>
        clsMainLogic logic;
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                logic = new clsMainLogic();
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
        /// This event listener will listen for when you press the button to add a new blender file to be processed.
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void btnAddNewBlenderFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                spBlenderFiles.Children.Add(new BlenderSelection());
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                               MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This event listener will listen for then you press the button to create the script file from the information you supplied
        /// </summary>
        /// <param name="sender">The object that called the event.</param>
        /// <param name="e">Contains the event data for the event.</param>
        private void btnCreateScriptFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {

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
