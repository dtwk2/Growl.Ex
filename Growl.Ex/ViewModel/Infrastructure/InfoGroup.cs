using System;
using System.Windows.Media;
using HandyControl.Data;
using ReactiveUI;

namespace Growl.Ex.ViewModel.Infrastructure
{
    public class InfoGroup : ReactiveUI.ReactiveObject
    {
        private InfoType groupKey;
        private ObservableAsPropertyHelper<int> countChanged;

        public InfoGroup(InfoType groupKey, Brush brush, Geometry iconData, IObservable<int> countChanged)
        {
            //var blankImage = BitmapLoader.Current.Create(512.0f, 512.0f);
            this.groupKey = groupKey;
            Brush = brush;
            Data = iconData;
            this.countChanged = countChanged.ToProperty(this, a => a.Count);
        }

        public int Count => countChanged.Value;

        //public InfoType Type { get; }

        public Brush Brush { get; }

        public Geometry Data { get; }


    }

}
