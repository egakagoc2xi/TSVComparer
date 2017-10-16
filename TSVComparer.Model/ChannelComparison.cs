using System;
using System.Collections.Generic;

namespace TSVComparer.Model
{ 
    public class ChannelComparison : IEqualityComparer<ITSVGenericModel>
    {
        public bool Equals(ITSVGenericModel x, ITSVGenericModel y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }
            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
            {
                return false;
            }
            return (x.ChannelName.Equals(y.ChannelName) && x.ChannelNumber.Equals(y.ChannelNumber) && x.LongChannelName.Equals(y.LongChannelName) && x.ShortChannelName.Equals(y.ShortChannelName));
        }

        public int GetHashCode(ITSVGenericModel obj)
        {
            return (obj.ChannelName.GetHashCode() + obj.ChannelNumber.GetHashCode() + obj.LongChannelName.GetHashCode() + obj.ShortChannelName.GetHashCode());
        }
    }
}