using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Data;

namespace Desktop.Extensions.Helpers
{
  internal class EnumArrayDescriptionConverter : IValueConverter
  {
    #region Public Methods

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      List<string> descriptions = new List<string>();

      if (value is IEnumerable enums)
      {
        foreach (object element in enums)
        {
          Enum enumVal = element as Enum;

          descriptions.Add(GetEnumDescription(enumVal));
        }

        if (descriptions.Count > 0)
        {
          return descriptions;
        }
      }

      return value;
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