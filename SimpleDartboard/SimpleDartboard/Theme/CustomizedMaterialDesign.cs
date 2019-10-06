using System.Windows.Media;
using System.Windows.Media.Media3D;
using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.Wpf;

namespace SimpleDartboard.Theme
{
    public class CustomizedMaterialDesign : MaterialDesignDarkTheme, ITheme
    {
        private static readonly Color BaseColor = Colors.Yellow;

        public CustomizedMaterialDesign()
        {
            ValidationError = ValidationErrorColor;
            Background = CardBackground = BaseColor;
            Paper = ToolBarBackground = BaseColor.Darken(1);
            Body = Colors.DarkBlue;
            BodyLight = MaterialDesignBodyLight;
            ColumnHeader = MaterialDesignColumnHeader;
            CheckBoxOff = MaterialDesignCheckBoxOff;
            CheckBoxDisabled = MaterialDesignCheckBoxDisabled;
            Divider = MaterialDesignDivider;
            Selection = MaterialDesignSelection;
            FlatButtonClick = MaterialDesignFlatButtonClick;
            FlatButtonRipple = MaterialDesignFlatButtonRipple;
            ToolTipBackground = MaterialDesignToolTipBackground;
            ChipBackground = MaterialDesignChipBackground;
            SnackbarBackground = MaterialDesignSnackbarBackground;
            SnackbarMouseOver = MaterialDesignSnackbarMouseOver;
            SnackbarRipple = MaterialDesignSnackbarRipple;
            TextBoxBorder = MaterialDesignTextBoxBorder;
            TextFieldBoxBackground = MaterialDesignTextFieldBoxBackground;
            TextFieldBoxHoverBackground = MaterialDesignTextFieldBoxHoverBackground;
            TextFieldBoxDisabledBackground = MaterialDesignTextFieldBoxDisabledBackground;
            TextAreaBorder = MaterialDesignTextAreaBorder;
            TextAreaInactiveBorder = MaterialDesignTextAreaInactiveBorder;
        }

        public ColorPair PrimaryLight { get; set; }
        public ColorPair PrimaryMid { get; set; }
        public ColorPair PrimaryDark { get; set; }
        public ColorPair SecondaryLight { get; set; }
        public ColorPair SecondaryMid { get; set; }
        public ColorPair SecondaryDark { get; set; }
        public Color ValidationError { get; set; }
        public Color Background { get; set; }
        public Color Paper { get; set; }
        public Color CardBackground { get; set; }
        public Color ToolBarBackground { get; set; }
        public Color Body { get; set; }
        public Color BodyLight { get; set; }
        public Color ColumnHeader { get; set; }
        public Color CheckBoxOff { get; set; }
        public Color CheckBoxDisabled { get; set; }
        public Color Divider { get; set; }
        public Color Selection { get; set; }
        public Color FlatButtonClick { get; set; }
        public Color FlatButtonRipple { get; set; }
        public Color ToolTipBackground { get; set; }
        public Color ChipBackground { get; set; }
        public Color SnackbarBackground { get; set; }
        public Color SnackbarMouseOver { get; set; }
        public Color SnackbarRipple { get; set; }
        public Color TextBoxBorder { get; set; }
        public Color TextFieldBoxBackground { get; set; }
        public Color TextFieldBoxHoverBackground { get; set; }
        public Color TextFieldBoxDisabledBackground { get; set; }
        public Color TextAreaBorder { get; set; }
        public Color TextAreaInactiveBorder { get; set; }
    }
}