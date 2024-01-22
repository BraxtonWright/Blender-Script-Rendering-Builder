/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder UserControl BulletedItem
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 1.0
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the required properties and constructor for the UserControl BulletedItem.
 * Original source for this code with some modifications
 * https://stackoverflow.com/a/19138626
 * -----------------------------------------------------------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Blender_Script_Rendering_Builder.UserControls.Bulleted_Item
{
    /// <summary>
    /// Interaction logic for BulletedItem.xaml
    /// </summary>
    public partial class BulletedItem : UserControl
    {
        #region .Net property wrappers
        private static readonly SolidColorBrush _defaultBrush = new SolidColorBrush(Colors.Black);

        // The new UIPropertyMetadata() applies a default to the below properties if they are not defined or configured when this user control is created https://stackoverflow.com/a/72543547
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("BulletText", typeof(string), typeof(BulletedItem));
        public static readonly DependencyProperty BulletFillProperty = DependencyProperty.Register("BulletFill", typeof(SolidColorBrush), typeof(BulletedItem), new UIPropertyMetadata(_defaultBrush));
        public static readonly DependencyProperty BulletOutlineProperty = DependencyProperty.Register("BulletOutline", typeof(SolidColorBrush), typeof(BulletedItem), new UIPropertyMetadata(_defaultBrush));

        #region Getter and Setters for the above properties
        /// <summary>
        /// The text to be displayed after the bullet point
        /// </summary>
        public string BulletText
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// The fill color of the bullet 
        /// </summary>
        public SolidColorBrush BulletFill
        {
            get { return (SolidColorBrush)GetValue(BulletFillProperty); }
            set { SetValue(BulletFillProperty, value); }
        }

        /// <summary>
        /// The outline color of the bullet
        /// </summary>
        public SolidColorBrush BulletOutline
        {
            get { return (SolidColorBrush)GetValue(BulletOutlineProperty); }
            set { SetValue(BulletOutlineProperty, value); }
        }
        #endregion
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public BulletedItem()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        #endregion
    }
}
