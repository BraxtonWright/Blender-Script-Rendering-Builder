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
using Blender_Script_Rendering_Builder.Modules;
using Blender_Script_Rendering_Builder.Shared;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Controls;
using static Blender_Script_Rendering_Builder.UserControls.Render_Info.clsRenderInfoLogic;

namespace Blender_Script_Rendering_Builder.UserControls.Render_Info
{
    #region Namespace Variables
    //The reason why we have these here is so that we can access them both in the below class and the binding them to the UI file as shown here https://youtu.be/uWsvh5rEMRI?t=15

    /// <summary>
    /// A list of options that are valid for the type of render for the render data
    /// </summary>
    public enum enumAnimationOrFrameOptions
    {
        [Description("Use Blender configs")] UseBlender,
        [Description("Animation")] Animation,
        [Description("Frame Range")] FrameRange,
        [Description("Frames Custom")] FrameCustom
    }

    /// <summary>
    /// A list of valid options to define what output file type they want to output.
    /// </summary>
    public enum enumOutputFileOptions
    {
        [Description("Use Blender configs")] UseBlender,
        [Description("avijpeg")] AVIJPEG,
        [Description("aviraw")] AVIRAW,
        [Description("bmp")] BMP,
        [Description("iris")] IRIS,
        [Description("iriz")] IRIZ,
        [Description("jpeg")] JPEG,
        [Description("png")] PNG,
        [Description("rawtga")] RAWTGA,
        [Description("tga")] TGA
    }

    /// <summary>
    /// A list of valid options to define what rendering engine they want to use.
    /// </summary>
    public enum enumRenderEngineOptions
    {
        [Description("Use Blender configs")] UseBlender,
        [Description("Cycles")] Cycles,
        [Description("Eevee")] Eevee,
        [Description("Workbench")] Workbench
    }

    /// <summary>
    /// A list of valid options to define what output folder they want to use.
    /// </summary>
    public enum enumOutputFolderOptions
    {
        [Description("Use Blender configs")] UseBlender,
        [Description("Browse for folder")] Browse
    }
    #endregion

    /// <summary>
    /// Interaction logic for RenderInfo.xaml
    /// </summary>
    public partial class RenderInfo : UserControl
    {
        #region Class Variables
        /// <summary>
        /// Object to perform logic for the RenderInfo UserControl.
        /// </summary>
        private clsRenderInfoLogic logic;

        /// <summary>
        /// Will contain all the data about the rendering info found on the UI
        /// </summary>
        public RenderModel renderData;
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
                logic = new clsRenderInfoLogic();  // Make a new instance of the logic class for this user control
                renderData = new RenderModel();  // make a new instance of the RenderModel class
                //DataContext = this;  // Set the data context of this UserControl itself
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
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Open a folder browser dialog to allow the user to select a folder to output the renders to
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void btnOutputPathBrowse_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.FolderBrowserDialog outputPath = new System.Windows.Forms.FolderBrowserDialog();
                if (outputPath.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string folderPath = outputPath.SelectedPath;
                    string folderName = logic.ExtractFolderName(folderPath);

                    // Temporary, will be saved to an instance of the class clsRender
                    lblOutputFolder.Tag = folderPath;
                    lblOutputFolder.Content = folderName;
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This event listener will listen for when you change the item selected in the combo box and change the fields below it so it will best the use case
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Selection Changed Event</param>
        private void cmbAnimationOrFrame_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBox cb = sender as ComboBox;
                Object selectedItem = cb.SelectedItem;

                switch (selectedItem)
                {
                    case enumAnimationOrFrameOptions.UseBlender:
                        grdStartEndFrames.Visibility = System.Windows.Visibility.Collapsed;
                        grdCustomFrames.Visibility = System.Windows.Visibility.Collapsed;
                        break;
                    case enumAnimationOrFrameOptions.Animation:
                    case enumAnimationOrFrameOptions.FrameRange:
                        grdStartEndFrames.Visibility = System.Windows.Visibility.Visible;
                        grdCustomFrames.Visibility = System.Windows.Visibility.Collapsed;
                        break;
                    case enumAnimationOrFrameOptions.FrameCustom:
                        grdStartEndFrames.Visibility = System.Windows.Visibility.Collapsed;
                        grdCustomFrames.Visibility = System.Windows.Visibility.Visible;
                        break;
                    default:
                        throw new Exception("There is no option with the name " + selectedItem);
                }
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
        /// <param name="e">The event's information, I.E. a Selection Changed Event</param>
        private void cmbOutputFolder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBox cb = sender as ComboBox;
                Object selectedItem = cb.SelectedItem;

                switch (selectedItem)
                {
                    case enumOutputFolderOptions.UseBlender:
                        grdOutputFolderInfo.Visibility = System.Windows.Visibility.Collapsed;
                        break;
                    case enumOutputFolderOptions.Browse:
                        grdOutputFolderInfo.Visibility = System.Windows.Visibility.Visible;
                        break;
                    default:
                        throw new Exception("There is no option with the name " + selectedItem);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region Helper Functions
        #endregion
    }
}
