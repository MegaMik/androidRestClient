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
using Java.Lang;

namespace androidRestClient
{
    public class ViewHolder:Java.Lang.Object
    {
        public TextView textView { get; set; }

    }
    class ListViewAdapter : BaseAdapter
    {
        private Context context;
        private string[] data;
        public ListViewAdapter(Context context)
        {
            this.context = context;
        }
        public override int Count
        {
            get
            {
                return data.Length;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return data[position];
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ViewHolder viewHolder;
            if (viewHolder == null)
            {
                convertView = LayoutInflater.From(context).Inflate(Resource.Layout.spinner_item); 
            }

        }
    }
}