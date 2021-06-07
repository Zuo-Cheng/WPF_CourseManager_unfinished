using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CourseManager.Converter
{
    /// <summary>
    /// 性别的单选按钮转换器
    /// </summary>
    public class GenderConverter : IValueConverter
    {
        /// <summary>
        /// 这个是model向view传递的时候的转换,比如页面加载的时候model会向页面传递数据，这个时候调用这个方法
        /// </summary>
        /// <param name="value">model的值</param>
        /// <param name="targetType">model值得类型</param>
        /// <param name="parameter">view视图通过ConverterParameter属性传过来的参数</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;
            return value.ToString() == parameter.ToString();
        }

        /// <summary>
        /// 这个是view向model传递参数的时候的转换，当用户的行为修改了指定的数据之后，则触发这个方法,
        /// 这个方法会返回parameter这个参数，然后会再次调用Converter()这个方法，并且将返回的参数传递到Converter()方法中
        /// </summary>
        /// <param name="value">model的值</param>
        /// <param name="targetType"></param>
        /// <param name="parameter">view视图通过ConverterParameter属性传过来的参数</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter;
        }
    }
}
