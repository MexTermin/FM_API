﻿#nullable disable

namespace FMAPI.DTOS
{
    public class EstimateIncomeDTO
    {
        public long Id { get; set; }

        public long Id_Estimate { get; set; }

        public long Id_Income { get; set; }

        public Income Income { get; set; }
    }
}