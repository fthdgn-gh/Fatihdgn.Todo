using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fatihdgn.Todo.App.Extensions;

public class TagExtension
{
    public static readonly BindableProperty TagProperty =
            BindableProperty.CreateAttached("Tag", typeof(object), typeof(TagExtension), null);


    public static object GetTag(BindableObject view)
    {
        return view.GetValue(TagProperty);
    }

    public static void SetTag(BindableObject view, object value)
    {
        view.SetValue(TagProperty, value);
    }
}
