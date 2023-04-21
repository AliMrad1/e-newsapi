﻿using Entities;
using DALC;

namespace BLC
{
    public class BLC
    {
        private DALC_SQL DALC_SQL;

        public BLC()
        {
            DALC_SQL = new DALC_SQL();       
        }

        public List<Category> GetCategories()
        {
            return DALC_SQL.getAllCategory();
        }
    }
}