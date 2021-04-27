﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ecomerce.Models;

namespace Ecomerce.Clases
{
    public class DbHelper
    {
        internal static int GetState(string description, EcomerceContext db)
        {
            var state = db.States.Where(s => s.Description == description).FirstOrDefault();
            if (state == null)
            {
                state = new State { Description = description, };
                db.States.Add(state);
                db.SaveChanges();
            }
            return state.StateId;
        }
    }
}