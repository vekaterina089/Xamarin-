﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2
{
    public class AddMedicinePageFlyoutMenuItem
    {
        public AddMedicinePageFlyoutMenuItem()
        {
            TargetType = typeof(AddMedicinePageFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}