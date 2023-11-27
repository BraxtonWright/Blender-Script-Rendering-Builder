using Blender_Script_Rendering_Builder.Classes.Helpers;
using Blender_Script_Rendering_Builder.Classes.Modules;
using Blender_Script_Rendering_Builder.UserControls.Bulleted_Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Blender_Script_Rendering_Builder.Windows.Error_List
{
    public class clsErrorListLogic
    {
        #region Variables
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public clsErrorListLogic()
        {
            // Do nothing
        }
        #endregion

        #region Functions
        /// <summary>
        /// Creates a instance of the user control BulletedItem and configures it as you defined
        /// </summary>
        /// <param name="bulletText">The text to be displayed on the bullet point</param>
        /// <param name="tabOffset">An optional tab offset for the bullet point</param>
        /// <returns>A instance of the usercontrol BulletedItem for the item</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public BulletedItem GenerateBulletPoint(string bulletText, int tabOffset = 0)
        {
            try
            {
                BulletedItem bulletedItem = new BulletedItem();
                bulletedItem.BulletText = bulletText;

                // Change the margin of the bullet point so they look like they are nested https://stackoverflow.com/a/1003808
                Thickness margin = bulletedItem.Margin;
                margin.Left += 15 * tabOffset;
                bulletedItem.Margin = margin;

                // Change the style of the bullet depending on what the tabOffset is.  The code for the SetResourceReference() was found here https://joshsmithonwpf.wordpress.com/2007/06/29/how-to-set-a-property-to-a-dynamicresource-reference-in-code/
                switch (tabOffset)
                {
                    case 0:
                        bulletedItem.SetResourceReference(BulletedItem.BulletFillProperty, "PrimaryBullitFillBrush");
                        break;
                    case 1:
                        bulletedItem.SetResourceReference(BulletedItem.BulletFillProperty, "SecondaryBullitFillBrush");
                        //bulletedItem.BulletFill = new SolidColorBrush(Colors.Gray);
                        break;
                    case 2:
                        bulletedItem.SetResourceReference(BulletedItem.BulletFillProperty, "TertiaryBullitFillBrush");
                        break;
                    default:
                        throw new Exception($"There is no switch condition for the tab offset of \"{tabOffset}\".");
                }
                return bulletedItem;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
    }
}
