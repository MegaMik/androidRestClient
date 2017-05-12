using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android;
using androidRestClient;

namespace androidRestClient
{
    //[Activity(Label = "Settings")]

    [Activity(NoHistory = true, Label = "Settings")]
    public class mwSettings : Activity
    {
        EditText txtIPNum;
        EditText txtIPPort;
        EditText txtAppName;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.mwSettings);

            txtIPNum = FindViewById<EditText>(Resource.Id.txtIPNum);
            txtAppName = FindViewById<EditText>(Resource.Id.txtAppName);
            txtIPPort = FindViewById<EditText>(Resource.Id.txtIPPort);

            retrieveset();

            Button btSaveS = FindViewById<Button>(Resource.Id.btSave);
            btSaveS.Click += btSaveSClick;
            
        }

        //private void SetContentView(object mwSettings)
        //{
        //    throw new NotImplementedException();
        //}

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
            StartActivity(typeof(mwSettings));
            Finish();
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