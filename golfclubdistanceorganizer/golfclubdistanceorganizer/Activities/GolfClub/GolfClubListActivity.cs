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
using golfclubdistanceorganizer.Repositories;
using golfclubdistanceorganizer.Adapters;

namespace golfclubdistanceorganizer.Activities.GolfClub
{
    [Activity(Label = "Golf Clubs")]
    public class GolfClubListActivity : Activity
    {
        private GolfClubRepository repository;
        private List<Models.GolfClub> gcList;

        ListView listViewGolfClubs;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.GolfClubList);

            repository = new GolfClubRepository();
            gcList = new List<Models.GolfClub>();

            GetGolfClubs();
            GetViews();
            SetAdapter();
            SetHandlers();
        }

        private void GetGolfClubs()
        {
            gcList = repository.GetAll().OrderBy(gc => gc.Type).ThenBy(gc => gc.Name).ToList();
        }

        private void GetViews()
        {
            listViewGolfClubs = FindViewById<ListView>(Resource.Id.listViewGolfClubs);
        }

        private void SetAdapter()
        {
            var adapter = new GolfClubListAdapter(this, gcList);
            listViewGolfClubs.Adapter = adapter;
        }

        private void SetHandlers()
        {
            listViewGolfClubs.ItemClick += ListViewGolfClubs_ItemClick;
        }

        private void ListViewGolfClubs_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var gc = gcList[e.Position];
            var intent = new Intent(this, typeof(EditGolfClubActivity));
            intent.PutExtra("GcId", gc.Id);
            StartActivity(intent);
        }
    }
}