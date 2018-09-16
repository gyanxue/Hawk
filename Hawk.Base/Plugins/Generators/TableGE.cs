﻿using System.Collections.Generic;
using Hawk.Base.Interfaces;
using Hawk.Base.Utils;
using Hawk.Base.Utils.Plugins;

namespace Hawk.Base.Plugins.Generators
{
    [XFrmWork("TableGE","TableGE_desc" )]
    public class TableGE : GeneratorBase
    {
        private readonly IDataManager dataManager;

        public TableGE()
        {
            dataManager = MainDescription.MainFrm.PluginDictionary["DataManager"] as IDataManager;
            TableSelector = new ExtendSelector<string>();
            TableSelector.GetItems = () => dataManager.DataCollections.Select(d=>d.Name).ToList();
            TableSelector.SelectChanged +=(s,e)=> this.InformPropertyChanged("TableSelector");
        }

        [Browsable(false)]
        public override string KeyConfig => TableSelector?.SelectItem; 
        [LocalizedDisplayName("key_461")]
        [LocalizedDescription("key_462")]
        [PropertyOrder(1)]
        public ExtendSelector<string> TableSelector { get; set; }

        //public override FreeDocument DictSerialize(Scenario scenario = Scenario.Database)
        //{
        //    var dict = base.DictSerialize(scenario);
        //    if(TableSelector.SelectItem!=null)
        //      dict.Set("Table", TableSelector.SelectItem);
             
        //    return dict;
        //}
        //public override void DictDeserialize(IDictionary<string, object> docu, Scenario scenario = Scenario.Database)
        //{
        //    base.DictDeserialize(docu);
        //    TableSelector.SelectItem =
        //        dataManager.DataCollections.FirstOrDefault(d => d.Name == docu["Table"].ToString());
        //    TableSelector.InformPropertyChanged("");
        //}
        public override IEnumerable<IFreeDocument> Generate(IFreeDocument document = null)
        {
            DataCollection table = dataManager.DataCollections.FirstOrDefault(d=>d.Name== TableSelector.SelectItem);
            if(table==null)
                yield break;
            var me = table.ComputeData;
            foreach (IDictionarySerializable  item in me)
            {
                yield return item.Clone() as FreeDocument;
            }
        }



    }
}