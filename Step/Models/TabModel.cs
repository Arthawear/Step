using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Step.Models
{
    public class TabModel : BaseModel
    {
        //private string name="Valaki";
        internal static string[] tabNames = new string[18] { "Barni", "Szabi", "Laci", "Csaba", "P. Eszter", "D-K. Eszter", "Lori", "Konya", "Donat", "Betty", "Isti", "Cili", "Boti", "Tita", "Balint", "Csabi", "Timi", "Hajna" };
        //private string[] outPlaces = new string[18] { "A", "D", "G", "J", "M", "P", "S", "V", "Y", "AB", "AE", "AH", "AK", "AN", "AQ", "AT", "AW", "AZ"};
        //private string[] inPlaces = new string[18] { "B", "E", "H", "K", "N", "Q", "T", "W", "Z", "AC", "AF", "AI", "AL", "AO", "AR", "AU", "AX", "BA" };

        //public string Name
        //{
        //    get
        //    {
        //        return this.name;
        //    }
        //    set
        //    {
        //        if (this.name != value)
        //        {
        //            this.name = value;
        //            this.OnPropertyChanged("Name");
        //        }
        //    }
        //}
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
