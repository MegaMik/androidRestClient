using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Xml;
using System.Web;
using Android.Views;
using Android;
using Android.Content;
using Xamarin.Android.Net;
using Xamarin.Android;
using Android.Support.V7.App;
using ActionBar = Android.Support.V7.App.ActionBar;
using com.refractored.fab;
using Android.Support.V7.Widget;
using RecyclerView = Android.Support.V7.Widget.RecyclerView;

using Android.OS;
using Android.Runtime;
using Android.Widget;


namespace androidRestClient
{
    [Activity(Label = "MW Rest Web service", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Spinner spnData;
        Button btGetData;
        String txtIPNum;
        String txtAppName;
        String txtIPPort;
        Int32 ButtonClicked = 0;
        ArrayAdapter mwAdapter;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            btGetData = FindViewById<Button>(Resource.Id.btGetWebData);

            spnData = FindViewById<Spinner>(Resource.Id.spnData);
            spnData.SetGravity(GravityFlags.Center);
            //spnData.;
            mwAdapter = new ArrayAdapter<string>(this, Resource.Layout.spinner_item);
            mwAdapter.Add("Load data");

            // mwAdapter.SetDropDownViewResource(Resource.Layout.spinner_item);
            spnData.Adapter = mwAdapter;

            spnData.ItemSelected += spnData_ItemSelected;

            btGetData.Click += OnButtonClicked;

            retrieveset();
            var list = FindViewById<AbsListView >(Resource.Id.list);

            var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.AttachToListView(list, this, this);
            fab.Click += (sender, args) =>
            {
                Toast.MakeText(this, "FAB Clicked!", ToastLength.Short).Show();
            };

            if (txtIPNum == null)
            {

                StartActivity(typeof(mwSettings));
            };

        }
        //protected void spnData_ItemSelected()
        //{

        //}
        private void InitActionBar()
        {
            if (SupportActionBar == null)
                return;

            var actionBar = SupportActionBar;
            actionBar.NavigationMode = (int)ActionBarNavigationMode.Tabs;

            var tab1 = actionBar.NewTab();
            tab1.SetTabListener(this);
            tab1.SetText("ListView");
            actionBar.AddTab(tab1);

            var tab2 = actionBar.NewTab();
            tab2.SetTabListener(this);
            tab2.SetText("RecyclerView");
            actionBar.AddTab(tab2);

            var tab3 = actionBar.NewTab();
            tab3.SetTabListener(this);
            tab3.SetText("ScrollView");
            actionBar.AddTab(tab3);
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            //string mwDataList;

            //Retrieve Settingsvalues
            retrieveset();

            if (txtIPNum != null)
            {
                string wcfURL = "http://" + txtIPNum + ":" + txtIPPort + "/" + txtAppName + "/Xml/0";
                Uri mwUri = new Uri(wcfURL);
                //var request = WebRequest.Create(string.Format(@"http://192.168.0.101/wcfRestHostedLibrary/xml/0", ""));
                var request = WebRequest.Create(mwUri);
                //var request = WebRequest.Create(string.Format(@IPNO, ""));
                request.ContentType = "application/xml";
                request.Method = "GET";
                string content;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        content = ("Error fetching data. Server returned status code" + response.StatusDescription);

                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        content = reader.ReadToEnd();
                        if (string.IsNullOrWhiteSpace(content))
                        {
                            content = "Response contained empty body...";
                        }
                    }
                }

                XmlDocument xmlDoc = new XmlDocument();
                //decode the received results from HTML format. The result in "content" contains HTML tags instead of the visual character representation, like &lt; is the char <
                content = HttpUtility.HtmlDecode(content);
                if (ButtonClicked == 0)
                {
                    ButtonClicked += 1;
                }
                else
                {
                    ButtonClicked = 0;
                };

                xmlDoc.LoadXml(content);
                //List to put values received from webservice to populate the adapter for the spinner
                List<string> mwXMLList = new List<string>();

                XmlNodeList parentNode = xmlDoc.GetElementsByTagName("ProductID");

                foreach (XmlNode childrenNode in parentNode)
                {
                    mwXMLList.Add(childrenNode.InnerText);
                }

                ArrayAdapter mwAdapter = new ArrayAdapter<string>(this, Resource.Layout.spinner_item);
                //mwAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
                mwAdapter.AddAll(mwXMLList);

                spnData.Adapter = mwAdapter;

            }
        }

        private void spnData_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (ButtonClicked == 1)
            {
                spnData = (Spinner)sender;
                string strSelected = string.Format("{0}", spnData.GetItemAtPosition(e.Position));
                string wcfURL = "http://" + txtIPNum + ":" + txtIPPort + "/" + txtAppName + "/Xml/0";
                Uri mwUri = new Uri(wcfURL);
                //var request = WebRequest.Create(string.Format(@"http://192.168.0.101/wcfRestHostedLibrary/xml/0", ""));
                var request = WebRequest.Create(mwUri);
                //var request = WebRequest.Create(string.Format(@IPNO, ""));
                request.ContentType = "application/xml";
                request.Method = "GET";
                string content;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        content = ("Error fetching data. Server returned status code" + response.StatusDescription);

                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        content = reader.ReadToEnd();
                        if (string.IsNullOrWhiteSpace(content))
                        {
                            content = "Response contained empty body...";
                        }
                    }
                }

                XmlDocument xmlDoc = new XmlDocument();
                //decode the received results from HTML format. The result in "content" contains HTML tags instead of the visual character representation, like &lt; is the char <
                content = HttpUtility.HtmlDecode(content);
                if (ButtonClicked == 0)
                {
                    ButtonClicked += 1;
                }
                else
                {
                    ButtonClicked = 0;
                };

                xmlDoc.LoadXml(content);
                //List to put values received from webservice to populate the adapter for the spinner
                List<string> mwXMLList = new List<string>();

                XmlNodeList parentNode = xmlDoc.GetElementsByTagName("ProductID");

                foreach (XmlNode childrenNode in parentNode)
                {
                    mwXMLList.Add(childrenNode.InnerText);
                }

                ArrayAdapter mwAdapter = new ArrayAdapter<string>(this, Resource.Layout.spinner_item);
                //mwAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
                mwAdapter.AddAll(mwXMLList);

                spnData.Adapter = mwAdapter;
            }



            //string toast = string.Format("{0}", spnData.GetItemAtPosition(e.Position));
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
        }



        // Function called from OnCreate
        protected void retrieveset()
        {
            //retreive 
            var prefs = Application.Context.GetSharedPreferences("mwSettings", FileCreationMode.Private);
            txtIPNum = prefs.GetString("txtIPNum", null);
            txtAppName = prefs.GetString("txtAppName", null);
            txtIPPort = prefs.GetString("txtIPPort", null);
        }
    }
}



    

