/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder UserControl RenderSelection
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the required event listeners for the UserControl RenderSelection.
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

    #endregion

    /// <summary>
    /// Interaction logic for RenderSelection.xaml
    /// </summary>
    public partial class RenderSelection : UserControl
    {
        #region Class Variables
        /// <summary>
        /// Object to perform logic for the RenderSelection UserControl.
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
                renderData = new RenderData();  // make a new instance of the RenderModel class
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
        /// This is ran when the user control is done being loaded so that we can set the default selected combobox items, I.E. use what is defined in the blender file
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. an Routed Event</param>
        private void ucRenderSelection_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            cmbAnimationOrFrame.SelectedIndex = 0;
            cmbRenderEngine.SelectedIndex = 0;
            cmbOutputFileType.SelectedIndex = 0;
            cmbOutputFolder.SelectedIndex = 0;
        }

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
                    case "Custom Frames":
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
        /// This event listener listens for when the you change the text in a texbox to hide/show the placeholder text for the textbox
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Text Changed Event</param>
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

        /// <summary>
        /// This event listener listens for when the textbox looses focus so it can determin if the input is a valid string
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void txtCustomFrames_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                // It is a valid input
                if (Validators.CustomFramesValid(txtCustomFrames.Text))
                {
                    txtCustomFrames.Background = Brushes.White;
                }
                // It is invalid input
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
                        lblOutputFolder.Visibility = System.Windows.Visibility.Collapsed;
                        break;
                    case "Browse for folder":
                        try
                        {
                            System.Windows.Forms.FolderBrowserDialog outputPath = new System.Windows.Forms.FolderBrowserDialog();
                            // The user has selected a folder, so save the path to said folder
                            if (outputPath.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                renderData.OutputFullPath = outputPath.SelectedPath;
                                lblOutputFolder.Visibility = System.Windows.Visibility.Visible;
                            }
                            // The user has closed the window without defining the folder, so reset the control back to what it was before altering
                            else
                            {
                                cb.SelectedItem = e.RemovedItems[0];
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                        }
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

        #region Functions
        #region Combobox fillers
        /// <summary>
        /// Fills the comboboxes for the render type, engine, file output type, and output folder.
        /// </summary>
        private void FillComboBoxes()
        {
            FillAnimationOrFrameCombobox();
            FillRenderEngineCombobox();
            FillOutputFileTypeComboBox();
            FillOutputFolderComboBox();
        }

        /// <summary>
        /// Fills the combobox for the type of render
        /// </summary>
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

        /// <summary>
        /// Fills the combobox for the render engine
        /// </summary>
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

        /// <summary>
        /// Fills the combobox for the output file type
        /// </summary>
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

        /// <summary>
        /// Fills the combobox for the output folder for the render
        /// </summary>
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
        /// Grab all the necessary information required for rendering
        /// </summary>
        /// <returns>A reference to a internal variable of the class RenderData containing all the necessary information required for the render</returns>
        public RenderData GetRenderingInfo()
        {
            return renderData;
        }
        #endregion
    }
}