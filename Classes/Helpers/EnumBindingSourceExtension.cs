/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder class EnumBindingSourceExtension
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the required logic to extract the required data from an enum to be bound to something
 * like a combo box.  https://www.youtube.com/watch?v=Bp5LFXjwtQ0&ab_channel=BrianLagunas
 * Need to make it so it uses the description of the enum and not the name of the enum.
 * -----------------------------------------------------------------------------------------------------------
 */
using System;
using System.Windows.Markup;

namespace Blender_Script_Rendering_Builder.Classes.Helpers
{
    class EnumBindingSourceExtension : MarkupExtension
    {
        public Type EnumType { get; private set; }

        public EnumBindingSourceExtension(Type enumType)
        {
            if (enumType is null || !enumType.IsEnum)
                throw new Exception("EnumType must not be null and has to be of type Enum");

            EnumType = enumType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.GetValues(EnumType);
        }
    }
}
