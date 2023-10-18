/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder class clsScene
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 0.5
 * -----------------------------------------------------------------------------------------------------------
 * This file contains defines the view model for the class clsBlender so we can use it inside the UserControl.
 * -----------------------------------------------------------------------------------------------------------
 */

using Blender_Script_Rendering_Builder.Classes.Modules;

namespace Blender_Script_Rendering_Builder.Classes.View_Models
{
    class clsBlenderViewModel
    {
        #region Variables
        /// <summary>
        /// Private variable that stores the blenderData object.
        /// </summary>
        private static clsBlender _blenderData;
        #endregion

        #region Properties
        /// <summary>
        /// Either Gets or Sets the passenger object.
        /// </summary>
        public clsBlender BlenderData
        {
            get { return _blenderData; }
            set { _blenderData = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor for this class.
        /// </summary>
        public clsBlenderViewModel()
        {
            //do nothing
        }
        #endregion
    }
}
