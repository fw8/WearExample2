using System;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using static Android.Resource;

namespace WearExample2
{
    //----------------------------------------------------------------------
    // ADAPTER
    // Adapter to connect the data set (book) to the RecyclerView:

    public class BookAdapter : RecyclerView.Adapter
    {
        // Underlying data set (a book of questions):
        private readonly MainActivity mainActivity;
        public Book mBook;

        //----------------------------------------------------------------------
        // VIEW HOLDER

        // Implement the ViewHolder pattern: each ViewHolder holds references
        // to the UI components (TextView, Buttons) within the CardView
        // that is displayed in a row of the RecyclerView:

        public class PageViewHolder : RecyclerView.ViewHolder
        {
            public TextView QueryText { get; set; }
            public Button yesBtn, noBtn;

            // Get references to the views defined in the CardView layout.
            public PageViewHolder(View itemView)
                : base(itemView)
            {
                // Locate and cache view references:
                QueryText = itemView.FindViewById<TextView>(Resource.Id.listItemTextView);
                yesBtn = itemView.FindViewById<Button>(Resource.Id.listItemYesButton);
                noBtn = itemView.FindViewById<Button>(Resource.Id.listItemNoButton);
            
            }
        }

        // Load the adapter with the data set (book) at construction time:
        public BookAdapter(MainActivity a)
        {
            mainActivity = a;
            mBook = mainActivity.book;
        }

        // Create a new CardView (invoked by the layout manager):
        public override RecyclerView.ViewHolder
            OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the CardView:
            View itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.list_item, parent, false);

            // Create a ViewHolder to find and hold these view references, and
            // register OnClick with the view holder:
            PageViewHolder vh = new PageViewHolder(itemView);

            vh.yesBtn.Click += mainActivity.YesBtnClick;
            vh.noBtn.Click += mainActivity.NoBtnClick;

            return vh;
        }

        // Fill in the contents of the card (invoked by the layout manager):
        public override void
            OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            PageViewHolder vh = holder as PageViewHolder;

            // Set the TextView in this ViewHolder's CardView
            // from this position in the books history:
            vh.QueryText.Text = mBook.GetPageAt(position).text;

            // Button tag to row position...
            vh.yesBtn.SetTag(Resource.Id.listItemYesButton, position);
            vh.noBtn.SetTag(Resource.Id.listItemNoButton, position);

            if (position+1 < mainActivity.book.NumPages()) // nicht die letzte position
            {
                if (mBook.GetAnswerAt(position) == YesNoEnum.Yes)
                {
                    vh.yesBtn.SetBackgroundColor(Android.Graphics.Color.Green);
                    vh.noBtn.SetBackgroundColor(Android.Graphics.Color.Gray);
                }
                else
                {
                    vh.yesBtn.SetBackgroundColor(Android.Graphics.Color.Gray);
                    vh.noBtn.SetBackgroundColor(Android.Graphics.Color.Red);
                }
            }
            else // letzte Position... also normales rot/grün Schema
            {
                vh.yesBtn.SetBackgroundColor(Android.Graphics.Color.Green);
                vh.noBtn.SetBackgroundColor(Android.Graphics.Color.Red);
            }
        }

        // Return the number items in history
        public override int ItemCount => mainActivity.book.NumPages();
    }
}
