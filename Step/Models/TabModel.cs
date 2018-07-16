using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Step.Models
{
    public class TabModel : BaseModel
    {
        internal static string[] tabNames = new string[18] { "Name", "Name", "Name", "Name", "Name", "Name", "Name", "Name", "Name", "Name", "Name", "Name", "Name", "Name", "Name", "Name", "Name", "Name" };
        
        public string[] TabNames
        {
            get
            {
                return tabNames;
            }
            set
            {
                if (tabNames != value)
                {
                    tabNames = value;
                    this.OnPropertyChanged("TabNames");
                }
            }
        }
    }
}
