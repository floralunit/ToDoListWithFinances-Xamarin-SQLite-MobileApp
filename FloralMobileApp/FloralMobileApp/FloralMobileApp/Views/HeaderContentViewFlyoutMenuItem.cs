using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloralMobileApp.Views
{
    public class HeaderContentViewFlyoutMenuItem
    {
        public HeaderContentViewFlyoutMenuItem()
        {
            TargetType = typeof(HeaderContentViewFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}