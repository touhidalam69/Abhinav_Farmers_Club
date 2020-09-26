using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Widget;
using System;

namespace Abhinav_Farmers_Club
{
    [Activity(Theme = "@style/MyTheme.Login", NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class LoginActivity : Activity
    {
        EditText email;
        EditText password;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);

            //Get email & password values from edit text  
            email = FindViewById<EditText>(Resource.Id.txtEmail);
            password = FindViewById<EditText>(Resource.Id.txtPassword);
            //Trigger click event of Login Button  
            var button = FindViewById<FloatingActionButton>(Resource.Id.btnLogin);
            button.Click += DoLogin;
        }
        public void DoLogin(object sender, EventArgs e)
        {
            //NOTE: WE ARE CURRRENTLY GOING TO ADD STATIC CODE  
            //IN MY PHASE () TWO I WILL CHANGE THIS CODE TO DYNAMIC   
            //BECAUSE FOR DYNAMIC CODE WE NEED TO CRAETE WEB API FOR SAME  
            //STATIC CODE FOR PHASE ONE ONLY. WE WILL CHANGE IT TO DYNAMIX IN PHASE TWO  
            if (email.Text == "abc@gmail.com" && password.Text == "12345")
            {
                Toast.MakeText(this, "Login successfully done!", ToastLength.Long).Show();
                StartActivity(typeof(MainActivity));
            }
            else
            {
                //Toast.makeText(getActivity(), "Wrong credentials found!", Toast.LENGTH_LONG).show();  
                Toast.MakeText(this, "Wrong credentials found!", ToastLength.Long).Show();
            }
        }
    }
}