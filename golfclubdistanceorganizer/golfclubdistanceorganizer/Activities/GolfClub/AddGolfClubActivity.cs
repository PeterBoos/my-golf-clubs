using System;
using System.Linq;

using Android.App;
using Android.OS;
using Android.Widget;
using golfclubdistanceorganizer.Repositories;
using golfclubdistanceorganizer.Models;

namespace golfclubdistanceorganizer.Activities.GolfClub
{
    [Activity(Label = "Add a new Golf Club")]
    public class AddGolfClubActivity : Activity
    {
        private GolfClubRepository repository;

        EditText editTextGcName;
        Spinner spinnerClubType;
        EditText editTextGcBrand;
        Button btnSubmit;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            repository = new GolfClubRepository();

            SetContentView(Resource.Layout.AddGolfClub);

            SetViews();
            SetHandlers();
        }

        private void SetViews()
        {
            editTextGcName = FindViewById<EditText>(Resource.Id.editTextGcName);

            spinnerClubType = FindViewById<Spinner>(Resource.Id.spinnerGcType);
            var enumValues = Enum.GetValues(typeof(ModelEnums.ClubType));
            var arrayForAdapter = enumValues.Cast<ModelEnums.ClubType>().Select(e => e.ToString()).ToArray();
            var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, arrayForAdapter);
            spinnerClubType.Adapter = adapter;

            editTextGcBrand = FindViewById<EditText>(Resource.Id.editTextGcBrand);
            btnSubmit = FindViewById<Button>(Resource.Id.btnGcSubmit);
        }

        private void SetHandlers()
        {
            btnSubmit.Click += BtnSubmit_Click;
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(editTextGcName.Text))
            {
                Toast.MakeText(this, $"You must enter a Name of the Golf club, ex. 'Iron 7'.", ToastLength.Long).Show();
                return;
            }

            var gc = new Models.GolfClub();
            gc.Name = editTextGcName.Text;

            var selectedItem = (int)spinnerClubType.SelectedItemId;
            gc.Type = selectedItem;
            gc.Brand = editTextGcBrand.Text;
            gc.CreatedDate = DateTime.Now;
            gc.ModifiedDate = null;

            repository.Add(gc);

            Toast.MakeText(this, $"New Golf Club created! {gc.Name}", ToastLength.Long).Show();

            editTextGcName.Text = "";
            editTextGcBrand.Text = "";
        }
    }
}