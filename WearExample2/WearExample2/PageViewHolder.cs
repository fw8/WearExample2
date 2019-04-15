using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace WearExample2
{
    //----------------------------------------------------------------------
    // VIEW HOLDER

    // Implement the ViewHolder pattern: each ViewHolder holds references
    // to the UI components (TextView, Button) within the CardView
    // that is displayed in a row of the RecyclerView:

    public class PageViewHolder : RecyclerView.ViewHolder
    {
        public TextView QueryText { get;  set; }

        // Get references to the views defined in the CardView layout.
        public PageViewHolder(View itemView)
            : base(itemView)
        {
            // Locate and cache view references:
            QueryText = itemView.FindViewById<TextView>(Resource.Id.listItemTextView);
        }
    }
}