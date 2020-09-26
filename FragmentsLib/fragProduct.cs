using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Java.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Socket = Java.Net.Socket;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace Abhinav_Farmers_Club.FragmentsLib
{
    public class fragProduct : SupportFragment
    {
        List<Product> lstProduct;
        View view;
        string type = "Bangla";
        public fragProduct() { }
        public fragProduct(string type)
        {
            this.type = type;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.DefaultList, container, false);
            ListView myListView = view.FindViewById<ListView>(Resource.Id.DefaultListView);
            myListView.Adapter = GetAdapter();
            myListView.ItemClick += MyListView_ItemClick;
            return view;
        }
        public void MyListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (isOnline())
            {
                //try
                //{
                //    Context context = view.Context;
                //    Intent intent = new Intent(context, typeof(NewsPaperActivity));
                //    intent.PutExtra(NewsPaperActivity.URL, NewsPaperlist[e.Position].Url);
                //    context.StartActivity(intent);
                //}
                //catch (IOException ex)
                //{
                //    Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this.Activity);
                //    alert.SetTitle(ex.Message);
                //    alert.SetPositiveButton("OK ", (senderAlert, args) => { });
                //    alert.SetNegativeButton("Cancel", (senderAlert, args) => { });
                //    this.Activity.RunOnUiThread(() => { alert.Show(); });
                //}
            }
            else
            {
                //Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this.Activity);
                //alert.SetTitle("Check your Internet Connection");
                //alert.SetPositiveButton("OK ", (senderAlert, args) => { });
                //alert.SetNegativeButton("Cancel", (senderAlert, args) => { });
                //this.Activity.RunOnUiThread(() => { alert.Show(); });
            }
        }
        public ListViewNewsPaperAdapter GetAdapter()
        {
            lstProduct =new ProductDataAccess().GetProducts(type);
            ListViewNewsPaperAdapter adapter = new ListViewNewsPaperAdapter(this.Activity, lstProduct);
            return adapter;
        }
        public static bool isOnline()
        {
            try
            {
                int timeoutMs = 3000;
                Socket sock = new Socket();
                Java.Net.SocketAddress sockaddr = new InetSocketAddress("8.8.8.8", 53);
                sock.Connect(sockaddr, timeoutMs);
                sock.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    public class ListViewNewsPaperAdapter : BaseAdapter<Product>
    {
        private List<Product> lst;
        private Context LstContext;
        public ListViewNewsPaperAdapter(Context cntext, List<Product> items)
        {
            lst = items;
            LstContext = cntext;
        }
        public override int Count
        {
            get
            {
                return lst.Count;
            }
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override Product this[int position]
        {
            get
            {
                return lst[position];
            }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                row = LayoutInflater.From(LstContext).Inflate(Resource.Layout.ProductAdapter, null, false);
            }
                      

            ImageView ImgProduct = row.FindViewById<ImageView>(Resource.Id.ImgProduct);
            ImgProduct.SetImageResource(lst[position].Icon);

            TextView ProductName = row.FindViewById<TextView>(Resource.Id.ProductName);
            ProductName.Text = lst[position].ProductName;

            TextView plist_price_text = row.FindViewById<TextView>(Resource.Id.plist_price_text);
            plist_price_text.Text = lst[position].ProductPrice.ToString();

            TextView plist_weight_text = row.FindViewById<TextView>(Resource.Id.plist_weight_text);
            plist_weight_text.Text = lst[position].PrdSize;

            //ImageView Imgbutton = row.FindViewById<ImageView>(Resource.Id.Imgbutton);
            //Imgbutton.Tag = lst[position].ProductId;
            //Imgbutton.SetOnClickListener(new ButtonClickListener(LstContext));
            return row;
        }
        private class ButtonClickListener : Java.Lang.Object, View.IOnClickListener
        {
            private Context activity;
            public ButtonClickListener(Context activity)
            {
                this.activity = activity;
            }
            public void OnClick(View v)
            {
                //string name = (string)v.Tag;
                //List<NewsPaper> NewsPaperLst;
                //var ExistingData = Application.Context.GetSharedPreferences("ExistingData", FileCreationMode.Private);
                //var EditExistingData = ExistingData.Edit();
                //string ExistingNewsList = ExistingData.GetString("NewsList", null);

                //if (ExistingNewsList != null)
                //{
                //    NewsPaperLst = JsonConvert.DeserializeObject<List<NewsPaper>>(ExistingNewsList);
                //    if (NewsPaperLst.FirstOrDefault(c => c.NewsPaperTitle == name).bookmarks == true)
                //    {
                //        NewsPaperLst.FirstOrDefault(c => c.NewsPaperTitle == name).bookmarks = false;
                //        string text = string.Format("{0} Removed from Bookmark.", name);
                //        Toast.MakeText(this.activity, text, ToastLength.Short).Show();
                //    }
                //    else
                //    {
                //        NewsPaperLst.FirstOrDefault(c => c.NewsPaperTitle == name).bookmarks = true;
                //        string text = string.Format("{0} Bookmarked Successfully.", name);
                //        Toast.MakeText(this.activity, text, ToastLength.Short).Show();
                //    }
                //    EditExistingData.PutString("NewsList", JsonConvert.SerializeObject(NewsPaperLst));
                //    EditExistingData.Commit();
                //}
            }
        }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string Category { get; set; }
        public double ProductPrice { get; set; }
        public string ProductName { get; set; }
        public string PrdDescription { get; set; }
        public string PrdSize { get; set; }
        public double Qty { get; set; }
        public int Icon { get; internal set; }
    }
    public class ProductDataAccess
    {
        public List<Product> GetProducts(string category)
        {
            List<Product> lst = new List<Product>()
                {
                #region Bangla
                new Product { ProductId = 1, ProductPrice=100, Category = "Vegitable", ProductName = "Potato", PrdSize ="50gm-$70", Qty=0, Icon = Resource.Drawable.IconPotatoes},
                new Product { ProductId = 2, ProductPrice=120, Category = "Vegitable", ProductName = "Broccoli", PrdSize ="50gm-$70", Qty=0, Icon = Resource.Drawable.IconPotatoes},
                new Product { ProductId = 3, ProductPrice=300, Category = "Vegitable", ProductName = "artichoke", PrdSize ="50gm-$70", Qty=0, Icon = Resource.Drawable.IconPotatoes},
                new Product { ProductId = 4, ProductPrice=500, Category = "Vegitable", ProductName = "asparagus", PrdSize ="50gm-$70", Qty=0, Icon = Resource.Drawable.IconPotatoes},
                new Product { ProductId = 5, ProductPrice=130, Category = "Vegitable", ProductName = "cabbage", PrdSize ="50gm-$70", Qty=0, Icon = Resource.Drawable.IconPotatoes},
                new Product { ProductId = 6, ProductPrice=109, Category = "Fruit", ProductName = "Apples", PrdSize ="50gm-$70", Qty=0, Icon = Resource.Drawable.IconPotatoes},
                new Product { ProductId = 7, ProductPrice=150, Category = "Fruit", ProductName = "Citrus", PrdSize ="50gm-$70", Qty=0, Icon = Resource.Drawable.IconPotatoes},
                new Product { ProductId = 8, ProductPrice=190, Category = "Fruit", ProductName = "Berries", PrdSize ="50gm-$70", Qty=0, Icon = Resource.Drawable.IconPotatoes},
                new Product { ProductId = 9, ProductPrice=107, Category = "Fruit", ProductName = "Melons", PrdSize ="50gm-$70", Qty=0, Icon = Resource.Drawable.IconPotatoes}                
                #endregion
            };

            lst = lst.FindAll(n => n.Category == category).OrderBy(x => x.ProductName).ToList();
            return lst;
        }
    }
}