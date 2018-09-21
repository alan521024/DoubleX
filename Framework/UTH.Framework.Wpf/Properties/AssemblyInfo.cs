using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;

[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]

[assembly: XmlnsPrefix("http://uth.com/uthui", "uthui")]
[assembly: XmlnsDefinition("http://uth.com/uthui", "UTH.Framework.Wpf")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("WpfAnalyzers.DependencyProperty", "WPF0005:Name of PropertyChangedCallback should match registered name.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("WpfAnalyzers.DependencyProperty", "WPF0006:Name of CoerceValueCallback should match registered name.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("WpfAnalyzers.DependencyProperty", "WPF0007:Name of ValidateValueCallback should match registered name.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("WpfAnalyzers.DependencyProperty", "WPF0036:Avoid side effects in CLR accessors.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("WpfAnalyzers.DependencyProperty", "WPF0041:Set mutable dependency properties using SetCurrentValue.")]