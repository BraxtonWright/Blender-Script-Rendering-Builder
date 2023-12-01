/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder class INotifyPropertyChangedImplmented
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 1.0
 * -----------------------------------------------------------------------------------------------------------
 * This file simply contains everything needed to implement the INotifyPropertyChanged interface for any UI
 * elements that need to mirrored to a model.  To use it you simply inherit from this class in the desired
 * model and then you can use the OnPropertyChanged() function in that class.
 * -----------------------------------------------------------------------------------------------------------
 */

using System.ComponentModel;

namespace Blender_Script_Rendering_Builder.Classes.Helpers
{
    public class INotifyPropertyChangedImplmented : INotifyPropertyChanged
    {
        /// <summary>
        /// This is the contract we have to make with the compiler because we are implementing the interface "INotifyPropertyChanged".  So we must have this event defined.  We will raise this event anytime one of our properties changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// A reusable set of code so that we can attach the PropertyChangedEventHandler, without having to type out this code multiple times
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        /*protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }*/

        // Or we can use this shorthand method to achieve the same thing as the above code. Source for this code https://youtu.be/HYVXO_uV68w?t=441
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
