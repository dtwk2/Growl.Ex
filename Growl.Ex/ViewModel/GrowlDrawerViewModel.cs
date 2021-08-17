
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows.Media;
using DynamicData;
using Growl.Ex.View.Infrastructure;
using Growl.Ex.ViewModel.Infrastructure;
using HandyControl.Data;
using ReactiveUI;
using G = HandyControl.Controls.Growl;

namespace Growl.Ex.ViewModel
{
    /// <summary>
    /// Interaction logic for GrowlDrawerView.xaml
    /// </summary>
    public partial class GrowlDrawerViewModel : ReactiveObject, IObserver<(AddRemove, G)>, IObserver<bool>
    {

        private readonly ISubject<G> removesSubject = new Subject<G>();
        private readonly ISubject<bool> checkedSubject = new Subject<bool>();
        private readonly ReadOnlyObservableCollection<InfoGroup> groupedItems;
        private readonly ObservableAsPropertyHelper<bool> state;

        public GrowlDrawerViewModel(IObservable<GrowlInfo> growlInfos)
        {
            var mainEvents = growlInfos.Where(a => a.Token == MessageToken.GrowlMainPanel);

            state = mainEvents
                 .Select(a => true)
                 .Merge(checkedSubject)
                 .ToProperty(this, a => a.State);

            _ = mainEvents
                .ToObservableChangeSet()
                .Bind(out var items)
                .GroupOn(a => a.Type)
                .Transform(Transform)
                .AutoRefresh()
                .Filter(a => a.Count > 0)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out groupedItems)
                .Subscribe();

            _ = items
                .SelectAddedAndResetItems()
                .Select(GrowlHelper.ToGrowlAction)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(a => a.Invoke());


            InfoGroup Transform(IGroup<GrowlInfo, InfoType> group)
            {
                var removes = removesSubject.Where(a => a.Type == group.GroupKey)
                    .Scan(0, (a, _) => ++a).StartWith(0);
                var (g, b) = group.GroupKey.GetGrowlInfo();
                var brush = HandyControl.Tools.ResourceHelper.GetResource<Brush>(b);
                var geometry = HandyControl.Tools.ResourceHelper.GetResource<Geometry>(g);
                return new InfoGroup(group.GroupKey, brush, geometry,
                    group.List
                        .CountChanged
                        .CombineLatest(removes, (a, bc) => a - bc));
            }
        }

        /// <summary>
        /// The state of awareness of the user relative to the growl icons:
        /// <list type="bullet">
        /// <item>
        /// <term>True</term>
        /// <description>User has been apprised of new growls</description>
        /// </item>
        /// <item>
        /// <term>False</term>
        /// <description>User has yet to be apprised of new growls.</description>
        /// </item>
        /// </list>
        /// </summary>
        public bool State => state.Value;

        public ReadOnlyObservableCollection<InfoGroup> GroupedItems => groupedItems;


        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(bool value)
        {
            checkedSubject.OnNext(value);
        }

        public void OnNext((AddRemove, G) value)
        {
            if (value.Item1 == AddRemove.Remove)
                removesSubject.OnNext(value.Item2);
        }
    }
}
