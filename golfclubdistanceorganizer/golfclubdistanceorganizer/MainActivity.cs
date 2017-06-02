using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using golfclubdistanceorganizer.Activities.GolfClub;

namespace golfclubdistanceorganizer
{
    [Activity(Label = "golfclubdistanceorganizer", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button btnAddGolfClub;
        Button btnGolfClubs;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
                        
            SetContentView (Resource.Layout.Main);
            SetViews();
            SetHandlers();
        }

        private void SetViews()
        {
            btnAddGolfClub = FindViewById<Button>(Resource.Id.btnAddGolfClub);
            btnGolfClubs = FindViewById<Button>(Resource.Id.btnListGolfClubs);
        }

        private void SetHandlers()
        {
            btnAddGolfClub.Click += BtnAddGolfClub_Click;
            btnGolfClubs.Click += BtnGolfClubs_Click;
        }

        private void BtnGolfClubs_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(GolfClubListActivity));
            StartActivity(intent);
        }

        private void BtnAddGolfClub_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(AddGolfClubActivity));
            StartActivity(intent);
        }
    }
}

