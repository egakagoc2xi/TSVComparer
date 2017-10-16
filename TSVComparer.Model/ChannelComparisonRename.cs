using System;
using System.Collections.Generic;

namespace TSVComparer.Model
{
    public class ChannelComparisonRename : IEqualityComparer<ITSVGenericModel>
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
            return ((((!x.ChannelName.Equals(y.ChannelName) && x.ChannelNumber.Equals(y.ChannelNumber)) && (x.Network.Equals(y.Network) && x.Tid.Equals(y.Tid)))
                && x.Transponder.Equals(y.Transponder)) && x.VPid.Equals(y.VPid));
        }

        public int GetHashCode(ITSVGenericModel obj)
        {
            return ((((obj.ChannelNumber.GetHashCode() + obj.Network.GetHashCode()) + obj.Tid.GetHashCode()) + obj.Transponder.GetHashCode()) + obj.VPid.GetHashCode());
        }
    }
}
