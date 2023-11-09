﻿/*
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

using System;
using System.Reflection;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Media;
using Blender_Script_Rendering_Builder.Classes.Helpers;
using Blender_Script_Rendering_Builder.Classes.Modules;


namespace Blender_Script_Rendering_Builder.UserControls.Render_Selection
{
    #region Namespace Variables
    //The reason why we have these here is so that we can access them both in the below class and the binding them to the UI file as shown here https://youtu.be/uWsvh5rEMRI?t=15

    /// <summary>
    /// A list of options that are valid for the type of render for the render data
    /// </summary>
    public enum enumAnimationOrFrameOptions
    {
        UseBlender,
        Animation,
        FrameRange,
        FrameCustom
    }

    /// <summary>
    /// A list of valid options to define what output file type they want to output.
    /// </summary>
    public enum enumOutputFileOptions
    {
        UseBlender,
        AVIJPEG,
        AVIRAW,
        BMP,
        IRIS,
        IRIZ,
        JPEG,
        PNG,
        RAWTGA,
        TGA
    }

    /// <summary>
    /// A list of valid options to define what rendering engine they want to use.
    /// </summary>
    public enum enumRenderEngineOptions
    {
        UseBlender,
        Cycles,
        Eevee,
        Workbench
    }

    /// <summary>
    /// A list of valid options to define what output folder they want to use.
    /// </summary>
    public enum enumOutputFolderOptions
    {
        UseBlender,
        Browse
    }
    #endregion

    /// <summary>
    /// Interaction logic for RenderInfo.xaml
    /// </summary>
    public partial class RenderSelection : UserControl
    {
        #region Class Variables
        /// <summary>
        /// Object to perform logic for the RenderInfo UserControl.
        /// </summary>
        private RenderSelectionLogic logic;

        /// <summary>
        /// Will contain all the data about the rendering info found on the UI
        /// </summary>
        public RenderData renderData;
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public RenderSelection()
        {
            try
            {
                InitializeComponent();
                logic = new RenderSelectionLogic();  // Make a new instance of the logic class for this user control
                renderData = new Classes.Modules.RenderData();  // make a new instance of the RenderModel class
                FillComboBoxes();
                DataContext = renderData;
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
        /// This event listener will listen for when you change the item selected in the combo box and change the fields below it so it will use the desired fields
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Selection Changed Event</param>
        private void cmbAnimationOrFrame_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBox cb = sender as ComboBox;
                object selectedItem = cb.SelectedItem;

                switch (selectedItem)
                {
                    case "Use Blender configs":
                        grdStartEndFrames.Visibility = System.Windows.Visibility.Collapsed;
                        grdCustomFrames.Visibility = System.Windows.Visibility.Collapsed;
                        break;
                    case "Animation":
                    case "Frame Range":
                        grdStartEndFrames.Visibility = System.Windows.Visibility.Visible;
                        grdCustomFrames.Visibility = System.Windows.Visibility.Collapsed;
                        break;
                    case "Frames Custom":
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
        /// This event listener will listen for when you change the item selected in the combo box and change the fields below it so it will use the desired fields
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
                    case "Use Blender configs":
                        grdOutputFolderInfo.Visibility = System.Windows.Visibility.Collapsed;
                        break;
                    case "Browse for folder":
                        try
                        {
                            System.Windows.Forms.FolderBrowserDialog outputPath = new System.Windows.Forms.FolderBrowserDialog();
                            if (outputPath.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                renderData.OutputFullPath = outputPath.SelectedPath;
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                        }

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

        private void txtCustomFrames_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if(txtCustomFrames.Text.Length <= 0)
                {
                    lblCustomFramesPlacholder.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    lblCustomFramesPlacholder.Visibility = System.Windows.Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private void txtCustomFrames_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                // If the text in the textbox matches the following regex pattern
                // One or more digites at the start of the string
                // Followed by ",'one or more digits'" OR ", 'one or more digits'" OR "-'one or more digits'" zero or more times
                // The the string ends
                Match regexResults = Regex.Match(txtCustomFrames.Text, "^\\d+(?:,\\d+|, \\d+|-\\d+)*$");

                // The input is valid
                if (regexResults.Success)
                {
                    txtCustomFrames.Background = Brushes.White;
                }
                // The input is invalid
                else
                {
                    txtCustomFrames.Background = new SolidColorBrush(Color.FromArgb(255, 255, 128, 128));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region Functions
        #region Combobox fillers
        private void FillComboBoxes()
        {
            FillAnimationOrFrameCombobox();
            FillRenderEngineCombobox();
            FillOutputFileTypeComboBox();
            FillOutputFolderComboBox();
        }

        private void FillAnimationOrFrameCombobox()
        {
            List<string> list = new List<string>
            {
                "Use Blender configs",
                "Animation",
                "Frame Range",
                "Custom Frames"
            };

            cmbAnimationOrFrame.ItemsSource = list;
        }

        private void FillRenderEngineCombobox()
        {
            List<string> list = new List<string>
            {
                "Use Blender configs",
                "Cycles",
                "Eevee",
                "Workbench"
            };

            cmbRenderEngine.ItemsSource = list;
        }

        private void FillOutputFileTypeComboBox()
        {
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

            cmbOutputFileType.ItemsSource = list;
        }

        private void FillOutputFolderComboBox()
        {
            List<string> list = new List<string>
            {
                "Use Blender configs",
                "Browse for folder",
            };

            cmbOutputFolder.ItemsSource = list;
        }
        #endregion

        /// <summary>
        /// Grab all the nessary information required for the rendering information
        /// </summary>
        /// <returns>An instance of the class RenderData containing all the nessary information required for the render</returns>
        public RenderData GetRenderInfo()
        {
            return renderData;
        }
        #endregion
    }
}