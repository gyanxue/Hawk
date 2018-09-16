﻿using System.Collections.Generic;
using Hawk.Base.Managements;
using Hawk.Base.Utils;
using Hawk.Base.Utils.Plugins;

namespace Hawk.Base.Plugins.Transformers
{
    public enum RepeatType
    {
        OneRepeat,
        ListRepeat,
    }
    [XFrmWork("RepeatTF", "RepeatTF_desc")]
    public class RepeatTF : TransformerBase
    {
        public RepeatTF()
        {
            RepeatCount = "1";
        }

        [LocalizedDisplayName("key_522")]
        public RepeatType RepeatType { get; set; }

        [LocalizedDisplayName("key_523")]
        public string RepeatCount { get; set; }

        public override bool Init(IEnumerable<IFreeDocument> docus)
        {
            IsMultiYield = true;
            return base.Init(docus);
        }

        public override IEnumerable<IFreeDocument> TransformManyData(IEnumerable<IFreeDocument> datas, AnalyzeItem analyzer)
        {
            switch (RepeatType)
            {
                    case RepeatType.ListRepeat:
                    var count = int.Parse(RepeatCount);
                    while (count>0)
                    {
                        foreach (var data in datas)
                        {
                            yield return data.Clone();
                        }
                        count--;
                    }
                    break;
                    case RepeatType.OneRepeat:
                    foreach (var data in datas)
                    {
                        var c = data.Query(RepeatCount);
                        var c2 = int.Parse(c);
                        while (c2 > 0)
                        {
                            yield return data;
                            c2--;
                        }
                    }

                    break;
            } 
        }
    }
}