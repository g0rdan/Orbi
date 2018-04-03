
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Droid.Support.V7.AppCompat;
using Orbi.ViewModels;
using static Android.Widget.AbsListView;
using static Android.Widget.AdapterView;

namespace Orbi.Droid.Activities
{
    [Activity]
    public class SelectVideosActivity : MvxAppCompatActivity<SelectVideosViewModel>
    {
        MvxListView _listView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SelectVideosView);
            _listView = FindViewById<MvxListView>(Resource.Id.select_listview);
            _listView.ChoiceMode = ChoiceMode.Multiple;
            var listener = new ListViewListener();
            listener.ItemSelected = ItemSelected;
            listener.ItemDeselected = ItemDeselected;
            _listView.OnItemClickListener = listener;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.right_menu, menu);
            menu.RemoveItem(Resource.Id.add);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            base.OnOptionsItemSelected(item);
            if (item.ItemId == Resource.Id.done)
                ViewModel.AddVideosCommand?.Execute();

            return false;
        }

        void ItemSelected(int index)
        {
            ViewModel.AddSelectedItem(index);
        }

        void ItemDeselected(int index)
        {
            ViewModel.RemoveSelectedItem(index);
        }
    }

    class ListViewListener : Java.Lang.Object, IOnItemClickListener
    {
        List<int?> _selectedIndexes = new List<int?>();

        public Action<int> ItemSelected { get; set; }
        public Action<int> ItemDeselected { get; set; }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            var index = _selectedIndexes.FirstOrDefault(x => x == position);
            if (index == null)
            {
                _selectedIndexes.Add(position);
                ItemSelected?.Invoke(position);
            }
            else
            {
                _selectedIndexes.Remove(position);
                ItemDeselected?.Invoke(position);
            }
        }
    }
}
