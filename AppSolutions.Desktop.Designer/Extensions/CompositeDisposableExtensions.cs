using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;

namespace AppSolutions.Desktop.Designer.Extensions
{
    public static class CompositeDisposableExtensions
    {
        public static T DisposeWith<T>(this T instance, CompositeDisposable disposable) where T : IDisposable
        {
            disposable.Add(instance);

            return instance;
        }
    }
}
