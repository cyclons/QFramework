using System.Collections.Generic;

namespace QFramework
{
    public class PanelCodeData
    {
        public          string                     PanelName;
        public          Dictionary<string, string> DicNameToFullName = new Dictionary<string, string>();
        public readonly List<MarkedObjInfo>        MarkedObjInfos    = new List<MarkedObjInfo>();
        public readonly List<ElementCodeData>      ElementCodeDatas  = new List<ElementCodeData>();
    }
}