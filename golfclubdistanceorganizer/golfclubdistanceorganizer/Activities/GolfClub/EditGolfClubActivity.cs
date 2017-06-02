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
using golfclubdistanceorganizer.Models;

namespace golfclubdistanceorganizer.Activities.GolfClub
{
    [Activity(Label = "EditGolfClubActivity")]
    public class EditGolfClubActivity : Activity
    {
        private GolfClubRepository repository;
        int gcId;
        Models.GolfClub gc;

        EditText editTextName;
        Spinner spinnerType;
        EditText editTextBrand;
        Button btnSubmit;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EditGolfClub);
            gcId = Intent.GetIntExtra("GcId", 0);
            if (gcId == 0) { /*Error here*/ }
            gc = repository.Get(gcId);

            SetViews();
            SetViewValues();
            SetHandlers();
        }

        private void SetViews()
        {
            editTextName = FindViewById<EditText>(Resource.Id.editTextEditGcName);

            spinnerType = FindViewById<Spinner>(Resource.Id.spinnerEditGcType);
            var enumValues = Enum.GetValues(typeof(ModelEnums.ClubType));
            var arrayForAdapter = enumValues.Cast<ModelEnums.ClubType>().Select(e => e.ToString()).ToArray();
            var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, arrayForAdapter);
            spinnerType.Adapter = adapter;

            editTextBrand = FindViewById<EditText>(Resource.Id.editTextEditGcBrand);
            btnSubmit = FindViewById<Button>(Resource.Id.btnEditGcSubmit);
        }

        private void SetViewValues()
        {
            editTextName.Text = gc.Name;
            spinnerType.SetSelection(gc.Type);
            editTextBrand.Text = gc.Brand;
        }

        private void SetHandlers()
        {
            btnSubmit.Click += BtnSubmit_Click;
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            gc.Name = editTextName.Text;
            var selectedItem = (int)spinnerType.SelectedItemId;
            gc.Type = selectedItem;
            gc.Brand = editTextBrand.Text;
            gc.ModifiedDate = DateTime.Now;

            repository.Update(gc);

            Toast.MakeText(this, $"Golf club {gc.Name} updated!", ToastLength.Long).Show();
        }
    }
}