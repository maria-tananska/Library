﻿namespace Library.Data.Models
{
    using Library.Data.Common.Models;

    public class Setting : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public string example { get; set; }
    }
}
