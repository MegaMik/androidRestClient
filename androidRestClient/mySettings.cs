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

namespace androidRestClient.Resources.layout
{
    [Activity(Label = "mySettings")]
    public class mySettings : Activity
    {
        EditText txtIPNum;
        EditText txtIPPort;
        EditText txtAppName;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(global::Android.Resource.Layout.mySettings);

            txtIPNum = FindViewById<EditText>(Resource.Id.txtIPNum);
            txtAppName = FindViewById<EditText>(Resource.Id.txtAppName);
            txtIPPort = FindViewById<EditText>(Resource.Id.txtIPPort);

            retrieveset();

            Button btSaveS = FindViewById<Button>(Resource.Id.btSave);
            btSaveS.Click += btSaveSClick;

        }

        private void SetContentView(object mwSettings)
        {
            throw new NotImplementedException();
        }

        public void btSaveSClick(object o, EventArgs e)
        {
            saveset();
        }
        protected void saveset()
        {
            //store

            var prefs = Application.Context.GetSharedPreferences("mwSettings", FileCreationMode.Private);
            var prefEditor = prefs.Edit();
            prefEditor.PutString("txtIPNum", txtIPNum.Text);
            prefEditor.PutString("txtAppName", txtAppName.Text);
            prefEditor.PutString("txtIPPort", txtIPPort.Text);
            prefEditor.Commit();

        }

        // Function called from OnCreate
        protected void retrieveset()
        {
            //retreive 
            var prefs = Application.Context.GetSharedPreferences("mwSettings", FileCreationMode.Private);
            if (prefs.GetString("txtIPNum", null) != null)
            {
                txtIPNum.Text = prefs.GetString("txtIPNum", null);
                txtAppName.Text = prefs.GetString("txtAppName", null);
                txtIPPort.Text = prefs.GetString("txtIPPort", null);
                //Show a toast
                RunOnUiThread(() => Toast.MakeText(this, txtAppName.Text, ToastLength.Long).Show());
            }
        }
    }
}