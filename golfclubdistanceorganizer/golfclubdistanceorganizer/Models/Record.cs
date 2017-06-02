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

namespace golfclubdistanceorganizer.Models
{
    public class Record
    {
        public int Id { get; set; }
        public int GolfClubId { get; set; }
        public int CarryDistance { get; set; }
        public int TotalDistance { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}