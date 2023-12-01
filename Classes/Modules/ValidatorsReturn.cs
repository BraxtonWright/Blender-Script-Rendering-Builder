/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder class ValidatorsReturn
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 1.0
 *  ----------------------------------------------------------------------------------------------------------
 * This file simply contains the variables required for the Validators class so we can return a custom data
 * type to the calling function.
 * -----------------------------------------------------------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blender_Script_Rendering_Builder.Classes.Modules
{
    public class ValidatorsReturn
    {
        #region Class variables
        /// <summary>
        /// A bool telling you if the information is valid
        /// </summary>
        public bool Valid { get; set; }

        /// <summary>
        /// A string of the error if it is not valid
        /// </summary>
        public string ErrorMessage { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public ValidatorsReturn()
        {

        }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="valid">A bool representing if the information is valid or not</param>
        public ValidatorsReturn(bool valid)
        {
            Valid = valid;
        }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="valid">A bool representing if the information is valid or not</param>
        /// <param name="errorMessage">A string representing the error discovered and to be displayed</param>
        public ValidatorsReturn(bool valid, string errorMessage) : this(valid)  // This constructor inherits from the above constructor so we don't need include the same code inside this constructor
        {
            string ErrorMessage = errorMessage;
        }
        #endregion
    }
}
