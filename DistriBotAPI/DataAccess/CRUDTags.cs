using DistriBotAPI.Contexts;
using DistriBotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.DataAccess
{
    public class CRUDTags
    {
        private Context db = new Context();

        public void CreateTag(Tag tag)
        {
            db.Tags.Add(tag);
            db.SaveChanges();
        }

        public CRUDTags()
        {

        }
    }
}