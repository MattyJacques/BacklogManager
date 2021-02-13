using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Data;

namespace Desktop.Extensions.Helpers
{
  public class EnumDescriptionConverter : IValueConverter
  {
    #region Public Methods

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      Enum myEnum = (Enum)value;
      string description = GetEnumDescription(myEnum);
      return description;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return string.Empty;
    }

    #endregion Public Methods

    #region Private Methods

    private string GetEnumDescription(Enum enumObject)
    {
      FieldInfo fieldInfo = enumObject.GetType().GetField(enumObject.ToString());

      object[] attribArray = fieldInfo.GetCustomAttributes(false);
      DescriptionAttribute attrib = attribArray.OfType<DescriptionAttribute>().FirstOrDefault();

      return attrib == null ? enumObject.ToString() : attrib.Description;
    }

    #endregion Private Methods
  }
}