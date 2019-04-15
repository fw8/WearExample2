using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace WearExample2
{
    //----------------------------------------------------------------------
    // ADAPTER
    // Adapter to connect the data set (book) to the RecyclerView:

    public class BookAdapter : RecyclerView.Adapter
    {
        // Underlying data set (a photo album):
        public Book mBook;

        // Load the adapter with the data set (book) at construction time:
        public BookAdapter(Book book)
        {
            mBook = book;
        }

        // Create a new photo CardView (invoked by the layout manager):
        public override RecyclerView.ViewHolder
            OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the CardView for the photo:
            View itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.list_item, parent, false);

            // Create a ViewHolder to find and hold these view references, and
            // register OnClick with the view holder:
            PageViewHolder vh = new PageViewHolder(itemView);
            return vh;
        }

        // Fill in the contents of the photo card (invoked by the layout manager):
        public override void
            OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            PageViewHolder vh = holder as PageViewHolder;

            // Set the ImageView and TextView in this ViewHolder's CardView
            // from this position in the photo album:
            vh.QueryText.Text = mBook.GetPageAt(position).text;
        }

        // Return the number of photos available in the photo album:
        public override int ItemCount
        {
            get { return mBook.NumPages(); }
        }
    }
}
