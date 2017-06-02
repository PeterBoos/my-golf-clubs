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
using golfclubdistanceorganizer.Models;
using golfclubdistanceorganizer.Repositories;

namespace golfclubdistanceorganizer.Adapters
{
    class GolfClubListAdapter : BaseAdapter<GolfClub>
    {
        //GolfClubRepository gcRepository;
        RecordRepository rRepository;
        List<GolfClub> gcList;
        Activity context;

        public GolfClubListAdapter(Activity context, List<GolfClub> gcList) : base()
        {
            this.context = context;
            this.gcList = gcList;
            rRepository = new RecordRepository();
        }


        public override GolfClub this[int position]
        {
            get { return gcList[position]; }
        }

        public override int Count
        {
            get { return gcList.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = gcList[position];

            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.GolfClubListRow, null);
            }
            convertView.FindViewById<TextView>(Resource.Id.txtRowName).Text = item.Name;
            convertView.FindViewById<TextView>(Resource.Id.txtRowBrand).Text = item.Brand;
            convertView.FindViewById<TextView>(Resource.Id.txtRowCreatedDate).Text = item.CreatedDate.ToString("yyyy-MM-dd hh:mm");

            var latestRecord = rRepository.GetLatestByGolfClubId(item.Id);
            if (latestRecord != null)
            {
                convertView.FindViewById<TextView>(Resource.Id.txtRowCarry).Text = latestRecord.CarryDistance.ToString();
                convertView.FindViewById<TextView>(Resource.Id.txtRowTotal).Text = latestRecord.TotalDistance.ToString();
                var allRecords = rRepository.GetAllByGolfClubId(item.Id);
                var totalDistanceSum = 0;
                foreach (var r in allRecords)
                {
                    totalDistanceSum += r.TotalDistance;
                }
                var avarageDistance = totalDistanceSum / allRecords.Count();
                convertView.FindViewById<TextView>(Resource.Id.txtRowAvarage).Text = avarageDistance.ToString();
            }

            convertView.FindViewById<TextView>(Resource.Id.txtRowLastUpdated).Text = $"Last updated: {item.ModifiedDate}";

            return convertView;
        }

        public void UpdateList(List<GolfClub> items)
        {
            this.gcList = items;
            NotifyDataSetChanged();
        }
    }
}