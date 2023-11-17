/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder class ScriptInfoValid
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 0.5
 * -----------------------------------------------------------------------------------------------------------
 * This file contains the variables required to return an object from a function call so the program knows if
 * the rendering information supplied is valid or not.  If it is not valid, this class will also contain a 
 * list of errors that were detected.
 * -----------------------------------------------------------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blender_Script_Rendering_Builder.Classes.Modules
{
    public class ScriptInfoValid
    {
        #region Class variables
        /// <summary>
        /// The script info is valid
        /// </summary>
        public bool Valid { get; private set; }

        // Some type of variable to contain a list of errors that were found
        public List<ErrorTreeBranch> Errors { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Default contstructor
        /// </summary>
        /// <param name="valid">A bool repersenting if errors exist in the data</param>
        public ScriptInfoValid(bool valid)
        {
            Valid = valid;
        }

        /// <summary>
        /// Overloaded constructor to define if the script is valid or not and if it is not valide, a list of errors that were found
        /// </summary>
        /// <param name="valid">A bool repersenting if errors exist in the data</param>
        /// <param name="errors">A list of the class ErrorTreeBranch containing all the errors that were found</param>
        public ScriptInfoValid(bool valid, List<ErrorTreeBranch> errors) : this(valid)  // This constructor inherits from the above constructor so we don't need include the same code inside this constructor
        {
            Errors = errors;
        }

        // A overloaded constructor that also accepts a list of errors that were found
        #endregion
    }
}
