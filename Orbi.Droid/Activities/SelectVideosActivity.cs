using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using Orbi.Messages;
using Orbi.ViewModels;
using static Android.Widget.AdapterView;

namespace Orbi.Droid.Activities
{
    [Activity]
    public class SelectVideosActivity : MvxAppCompatActivity<SelectVideosViewModel>
    {
        IMenuItem _doneBtn;
        MvxListView _listView;
        MvxSubscriptionToken _donBtnSubsToken;

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

		protected override void OnResume()
		{
            base.OnResume();
            _donBtnSubsToken = Mvx.Resolve<IMvxMessenger>().SubscribeOnMainThread<DoneBtnMessage>(ReceivedMessage);
		}

		protected override void OnPause()
		{
            base.OnPause();
            Mvx.Resolve<IMvxMessenger>().Unsubscribe<DoneBtnMessage>(_donBtnSubsToken);
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.right_menu, menu);
            _doneBtn = menu.GetItem(1);
            DisableDoneBtn();
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

        void ReceivedMessage(DoneBtnMessage message)
        {
            if (message.Enabled)
            {
                EnableDoneBtn();
            }
            else
            {
                DisableDoneBtn();
            }
        }

        void EnableDoneBtn()
        {
            _doneBtn.SetEnabled(true);
            _doneBtn.Icon.SetAlpha(255);
        }

        void DisableDoneBtn()
        {
            _doneBtn.SetEnabled(false);
            _doneBtn.Icon.SetAlpha(130);
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
