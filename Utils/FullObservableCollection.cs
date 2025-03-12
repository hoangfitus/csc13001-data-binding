using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace csc13001_data_binding.Utils;

public sealed class FullObservableCollection<T> : ObservableCollection<T>
    where T : INotifyPropertyChanged
{
    public FullObservableCollection()
    {
        CollectionChanged += FullObservableCollection_CollectionChanged;
    }

    public FullObservableCollection(IEnumerable<T> collection) : this()
    {
        foreach (var item in collection)
        {
            Add(item);
        }
    }
    private void FullObservableCollection_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (Object? item in e.NewItems)
            {
                ((INotifyPropertyChanged)item).PropertyChanged += Item_PropertyChanged;
            }
        }
        if (e.OldItems != null)
        {
            foreach (Object? item in e.OldItems)
            {
                ((INotifyPropertyChanged)item).PropertyChanged -= Item_PropertyChanged;
            }
        }
    }

    private void Item_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        var args = new NotifyCollectionChangedEventArgs(
            NotifyCollectionChangedAction.Replace, sender, sender, IndexOf((T)sender!)
        );
        OnCollectionChanged(args);
    }
}
