﻿using System.Collections.Generic;
using System.Xml;
using AgGateway.ADAPT.ApplicationDataModel.Products;
using AgGateway.ADAPT.ISOv4Plugin.Extensions;

namespace AgGateway.ADAPT.ISOv4Plugin.Loaders
{
    public static class CropVarietyLoader
    {
        public static Dictionary<string, CropVariety> Load(XmlNodeList inputNodes)
        {
            var varieties = new Dictionary<string, CropVariety>();
            foreach (XmlNode inputNode in inputNodes)
            {
                string varietyId;
                var variety = LoadVariety(inputNode, out varietyId);
                if (variety != null)
                    varieties.Add(varietyId, variety);
            }

            return varieties;
        }

        private static CropVariety LoadVariety(XmlNode inputNode, out string varietyId)
        {
            varietyId = inputNode.GetXmlNodeValue("@A");
            var description = inputNode.GetXmlNodeValue("@B");
            if (string.IsNullOrEmpty(varietyId) || string.IsNullOrEmpty(description))
                return null;

            var variety = new CropVariety
            {
                ProductType = ProductTypeEnum.Variety, 
                Description = description
            };
            variety.Id.UniqueIds.Add(ImportHelper.CreateUniqueId(varietyId));

            return variety;
        }
    }
}
