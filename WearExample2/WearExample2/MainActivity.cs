using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.Wearable.Views;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.Wearable.Activity;
using Java.Interop;
using Android.Views.Animations;
using Android.Support.V7.Widget;
using Android.Util;

namespace WearExample2
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : WearableActivity
    {
        RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        public Book book = new Book();
        private BookAdapter mAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.activityMainRecyclerView);

            // Plug in the linear layout manager:
            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);

            // Plug in my adapter:
            mAdapter = new BookAdapter(this);
            mRecyclerView.SetAdapter(mAdapter);
        }

        public void YesBtnClick(object sender, EventArgs e)
        {
            Button b = sender as Button;
            int position = (int)b.GetTag(Resource.Id.listItemYesButton);

            Log.Debug("Button", "Yes at position: " + position);

            book.RemoveBehind(position);

            book.GetNextPage(YesNoEnum.Yes);

            mAdapter.NotifyDataSetChanged();
            mRecyclerView.SmoothScrollToPosition(position + 1);
        }

        public void NoBtnClick(object sender, EventArgs e)
        {
            Button b = sender as Button;
            int position = (int)b.GetTag(Resource.Id.listItemNoButton);

            Log.Debug("Button", "No at position: " + position);

            book.RemoveBehind(position);

            book.GetNextPage(YesNoEnum.No);

            mAdapter.NotifyDataSetChanged();
            mRecyclerView.SmoothScrollToPosition(position + 1);
        }
    }
}