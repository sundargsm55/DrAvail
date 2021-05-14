using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrAvail.Services
{
    public class Utilities
    {
        public static DateTimeRelation CompareDateTime(DateTime dt1, DateTime dt2)
        {
            int result = DateTime.Compare(dt1, dt2);
            if (result == 0)
            {
                return DateTimeRelation.IsSame;
            }
            else if (result > 0)
            {
                return DateTimeRelation.IsLater;
            }
            return DateTimeRelation.IsEarlier;
        }

        public enum DateTimeRelation
        {
            IsEarlier,
            IsSame,
            IsLater
        }
    }
}
